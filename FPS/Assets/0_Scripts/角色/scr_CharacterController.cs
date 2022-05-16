using UnityEngine;
using static PlayerData;

public class scr_CharacterController : MonoBehaviour
{
    #region -- 欄位 -- 
    [HideInInspector]
    public float gravityValue = 0.03f;                // 環境重力
    [HideInInspector]
    public float gravity_Min = -3;                    // 最小重力值
    [HideInInspector]
    public float cameraSmoothTime = 0.2f;             // 攝影機換位時間
    [HideInInspector]
    public float viewClamp_YMin = -70;                // Y視角下限
    [HideInInspector]
    public float viewClamp_YMax = 80;                 // Y視角上限
    [HideInInspector]
    public float weaponAnimationSpeed;                // 武器動畫速度
    [HideInInspector]
    public bool isSprint;                             // 是否跑步中
    [HideInInspector]
    public bool isGround;                             // 是否在地上
    [HideInInspector]
    public bool isFalling;                            // 是否下墜中

    public bool isAiming;                             // 是否瞄準中

    [HideInInspector]
    public Vector2 input_View;                        // 滑鼠視角值
    [HideInInspector]
    public Vector2 input_Movement;                    // 鍵盤輸入值
    [HideInInspector]
    public LayerMask environmentMask;                 // 偵測地圖圖層
    [HideInInspector]
    public LayerMask groundMask;                      // 偵測地版圖層

    [Header("角色攝影機座標系統")]
    public Transform cameraTransform;
    [Header("角色位置的地板點")]
    public Transform feetTransform;

    [Header("角色自由性控制器參數")]
    public PlayerData playerdata;
    [Header("武器")]
    public scr_WeaponController currentWeapon;
    [Header("角色非自由性控制器參數")]
    public PlayerUnfreeSetting playerUnfreeSetting;
    [Header("角色參數 - 站立")]
    public ModelSetting playerStandState;
    [Header("角色參數 - 蹲下")]
    public ModelSetting playerCrouchState;
    [Header("角色參數 - 趴下")]
    public ModelSetting playerProneState;
    [Header("設定畫面")]
    public GameObject settingPage;

    public bool isSetting;

    private float playerGravity;                       // 玩家重力值
    private float cameraHeight;                        // 攝影機高度
    private float cameraHeightVelocity;                // 攝影機變換速度 (程式自定義)
    private float stateCapsuleHeightVelocity;          // 碰撞器高度改變速度 (程式自定義)
    private float stateCheckErrorMargin = 0.05f;       // 確認高度額外加的包容值

    private Vector3 newCameraRotation;                 // 攝影機的角度
    private Vector3 newCharacterRotation;              // 角色的角度
    private Vector3 jumpForce;                         // 跳躍力道
    private Vector3 jumpFallVelocity;                  // 下墜速度 (程式自定義)
    private Vector3 stateCapsuleCenterVelocity;        // 碰撞器座標改變速度 (程式自定義)
    private Vector3 newMovementSpeed;
    private Vector3 newMovementSpeedVelocity;

    private InputSystem inputSystem;                   // 輸入系統
    private CharacterController characterController;   // 角色內建控制器

    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputSystem = new InputSystem();

        inputSystem.Character.Movement.performed += p => input_Movement = p.ReadValue<Vector2>();
        inputSystem.Character.View.performed += p => input_View = p.ReadValue<Vector2>();
        inputSystem.Character.Jump.performed += p => Jump();
        inputSystem.Character.Crouch.performed += p => Crouch();
        inputSystem.Character.CrouchRelease.performed += p => StopCrouch();
        inputSystem.Character.Prone.performed += p => Prone();
        inputSystem.Character.Sprint.performed += p => ToggleSprint();
        inputSystem.Character.SprintRelease.performed += p => StopSprint();
        inputSystem.Character.Setting.performed += p => SettingPage();

        inputSystem.Weapon.Fire2Press.performed += p => AimingPressed();
        inputSystem.Weapon.Fire2Release.performed += p => AimingReleased();

        inputSystem.Enable();

        newCameraRotation = cameraTransform.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;
        cameraHeight = cameraTransform.localPosition.y;

        isSetting = false;

        if (currentWeapon)
        {
            currentWeapon.Initialise(this);
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

     void Update()
    {
        CalculateJump();
        CalculateState();
        Move();
        View();
        SetIsFalling();
        SetIsGrounded();
        CalculateAiming();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetTransform.position, playerUnfreeSetting.groundDetectRadius);
    }

    #region -- 方法 --
    /// <summary>
    /// 判斷是否可以站起來
    /// </summary>
    /// <param name="stateCheckHeight">針對不同狀態的高度</param>
    /// <returns></returns>
    private bool StateCheck(float stateCheckHeight)
    {
        var start = new Vector3(feetTransform.position.x, feetTransform.position.y + characterController.radius + stateCheckErrorMargin, feetTransform.position.z);                  // 碰撞器下緣 (0.35)
        var end = new Vector3(feetTransform.position.x, feetTransform.position.y - characterController.radius - stateCheckErrorMargin + stateCheckHeight, feetTransform.position.z); // 碰撞器上緣 (確認的高度-0.35)

        return Physics.CheckCapsule(start, end, characterController.radius, environmentMask);  // 假如 碰撞器下緣 ~ 碰撞器上緣 中間有碰觸到 環境圖層的話 回傳 True
    }

    /// <summary>
    /// 角色移動
    /// </summary>
    private void Move()
    {
        if (isSetting) return;

        if (input_Movement.y <= 0.2f)
        {
            isSprint = false;
        }

        var verticalSpeed = playerUnfreeSetting.walkForwardSpeed;
        var horizontalSpeed = playerUnfreeSetting.walkRLSpeed;

        if (isSprint)                                                       // 判斷是不是跑步中 => 影響 水平/垂直 速度
        {
            verticalSpeed = playerUnfreeSetting.runForwardSpeed;
            horizontalSpeed = playerUnfreeSetting.runRLSpeed;
        }

        // * Effectors
        if (!isGround)
        {
            playerUnfreeSetting.speedEffector = playerUnfreeSetting.fallSpeedEffector;
        }
        else if (playerUnfreeSetting.playerStates == PlayerState.Crouch)
        {
            playerUnfreeSetting.speedEffector = playerUnfreeSetting.crouchSpeedEffector;
        }
        else if (playerUnfreeSetting.playerStates == PlayerState.Prone)
        {
            playerUnfreeSetting.speedEffector = playerUnfreeSetting.proneSpeedEffector;
        }
        else
        {
            playerUnfreeSetting.speedEffector = 1;
        }

        weaponAnimationSpeed = characterController.velocity.magnitude / (playerUnfreeSetting.walkForwardSpeed * playerUnfreeSetting.speedEffector);

        if (weaponAnimationSpeed >= 1)
        {
            weaponAnimationSpeed = 1;
        }

        verticalSpeed *= playerUnfreeSetting.speedEffector;
        horizontalSpeed *= playerUnfreeSetting.speedEffector;

        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime), ref newMovementSpeedVelocity, isGround ? playerUnfreeSetting.movementSmooth : playerUnfreeSetting.fallingSmooth);
        var movementSpeed = transform.TransformDirection(newMovementSpeed);  // 調整面向

        movementSpeed.y += playerGravity;
        movementSpeed += jumpForce * Time.deltaTime;

        characterController.Move(movementSpeed);
    }

    /// <summary>
    /// 視角移動 / 角色旋轉
    /// </summary>
    private void View()
    {
        if (isSetting) return;

        newCharacterRotation.y += playerdata.ViewX_Sensitivity * (playerdata.ViewX_inverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        newCameraRotation.x += playerdata.ViewY_Sensitivity * (playerdata.ViewY_inverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClamp_YMin, viewClamp_YMax);

        cameraTransform.localRotation = Quaternion.Euler(newCameraRotation);
    }

    /// <summary>
    /// 計算跳躍
    /// </summary>
    private void CalculateJump()
    {
        if (isSetting) return;

        if (playerGravity > gravity_Min)
        {
            playerGravity -= gravityValue * Time.deltaTime;
        }

        if (playerGravity < -0.1f && isGround)
        {
            playerGravity = -0.1f;
        }

        jumpForce = Vector3.SmoothDamp(jumpForce, Vector3.zero, ref jumpFallVelocity, playerUnfreeSetting.jumpSmoothTime);
    }

    /// <summary>
    /// 跳躍功能
    /// </summary>
    private void Jump()
    {
        if (!isGround || playerUnfreeSetting.playerStates == PlayerState.Prone) // 假如不再地上 > 不執行回傳
        {
            return;
        }

        if (playerUnfreeSetting.playerStates == PlayerState.Crouch)                                   // 蹲下 & 趴下狀態的話 > 站立 
        {
            if (StateCheck(playerStandState.stateCollider.height))
            {
                return;
            }

            playerUnfreeSetting.playerStates = PlayerState.Stand;
            return;
        }

        jumpForce = Vector3.up * playerUnfreeSetting.jumpHeight;
        playerGravity = 0;

        currentWeapon.TriggerJump();
    }

    /// <summary>
    /// 計算攝影機高度
    /// </summary>
    private void CalculateState()
    {
        var currentState = playerStandState;

        if (playerUnfreeSetting.playerStates == PlayerData.PlayerState.Crouch)
        {
            currentState = playerCrouchState;
        }
        else if (playerUnfreeSetting.playerStates == PlayerData.PlayerState.Prone)
        {
            currentState = playerProneState;
        }

        cameraHeight = Mathf.SmoothDamp(cameraTransform.localPosition.y, currentState.CameraHeight, ref cameraHeightVelocity, cameraSmoothTime);
        cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraHeight, cameraTransform.localPosition.z);

        characterController.height = Mathf.SmoothDamp(characterController.height, currentState.stateCollider.height, ref stateCapsuleHeightVelocity, cameraSmoothTime);
        characterController.center = Vector3.SmoothDamp(characterController.center, currentState.stateCollider.center, ref stateCapsuleCenterVelocity, cameraSmoothTime);
    }

    /// <summary>
    /// 蹲下
    /// </summary>
    private void Crouch()
    {
        if (playerUnfreeSetting.playerStates == PlayerState.Crouch)
        {
            if (StateCheck(playerStandState.stateCollider.height))
            {
                return;
            }
            playerUnfreeSetting.playerStates = PlayerState.Stand;
            return;
        }

        if (StateCheck(playerCrouchState.stateCollider.height))
        {
            return;
        }

        playerUnfreeSetting.playerStates = PlayerState.Crouch;
    }

    /// <summary>
    /// 放開蹲下
    /// </summary>
    private void StopCrouch()
    {
        if (StateCheck(playerStandState.stateCollider.height))
        {
            return;
        }
        else
        {
            playerUnfreeSetting.playerStates = PlayerState.Stand;
        }
    }

    /// <summary>
    /// 趴下
    /// </summary>
    private void Prone()
    {
        if (playerUnfreeSetting.playerStates == PlayerState.Prone) // 假如狀態為趴下
        {
            if (StateCheck(playerStandState.stateCollider.height)) // 0.35 ~ 1.65 之間有其他物件
            {
                return;
            }
            playerUnfreeSetting.playerStates = PlayerState.Stand;
            return;
        }

        if (StateCheck(playerCrouchState.stateCollider.height))  // 0.35 ~ 0.85 之間有其他物件的話
        {
            return;
        }

        playerUnfreeSetting.playerStates = PlayerState.Prone;
    }

    /// <summary>
    /// 切換跑步
    /// </summary>
    private void ToggleSprint()
    {
        if (input_Movement.y <= 0.2f)
        {
            isSprint = false;
            return;
        }

        isSprint = !isSprint;
    }

    /// <summary>
    /// 暫停奔跑
    /// </summary>
    private void StopSprint()
    {
        isSprint = false;
    }

    /// <summary>
    /// 偵測是否在地上
    /// </summary>
    private void SetIsGrounded()
    {
        isGround = Physics.CheckSphere(feetTransform.position, playerUnfreeSetting.groundDetectRadius, groundMask);
    }

    /// <summary>
    /// 偵測是否在下墜
    /// </summary>
    private void SetIsFalling()
    {
        if (!isGround && characterController.velocity.magnitude > playerUnfreeSetting.fallingSpeed)
        {
            isFalling = true;
        }
    }

    /// <summary>
    /// 按下滑鼠左鍵
    /// </summary>
    private void AimingPressed()
    {
        isAiming = true;
    }

    /// <summary>
    /// 放開滑鼠左鍵
    /// </summary>
    private void AimingReleased()
    {
        isAiming = false;
    }

    /// <summary>
    /// 計算瞄準
    /// </summary>
    private void CalculateAiming()
    {
        if (!currentWeapon)
        {
            return;
        }

        currentWeapon.isAiming = isAiming;

    }
    #endregion

    private void SettingPage()
    {
        isSetting = !isSetting;

        settingPage.SetActive(isSetting);
        Cursor.visible = isSetting;

        if (isSetting) Cursor.lockState = CursorLockMode.None;
        else if (!isSetting) Cursor.lockState = CursorLockMode.Locked;
    }
}
