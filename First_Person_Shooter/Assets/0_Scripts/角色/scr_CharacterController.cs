using UnityEngine;
using static PlayerData;

public class scr_CharacterController : MonoBehaviour
{
    #region -- 欄位 -- 
    [HideInInspector]
    [Header("Y視角下限")]
    public float viewClamp_YMin = -70;
    [HideInInspector]
    [Header("Y視角上限")]
    public float viewClamp_YMax = 80;
    [Header("重力值")]
    public float gravityValue = 0.03f;
    [Header("最小重力值")]
    public float gravity_Min = -3;
    [Header("攝影機換位時間")]
    public float cameraSmoothTime;

    [Header("角色攝影機座標系統")]
    public Transform cameraTransform;
    [Header("角色位置的地板點")]
    public Transform feetTransform;
    [HideInInspector]
    [Header("偵測地圖圖層")]
    public LayerMask environmentMask;

    [Header("角色自由性控制器參數")]
    public PlayerData playerdata;
    [Header("角色非自由性控制器參數")]
    public PlayerUnfreeSetting playerUnfreeSetting;
    [Header("角色參數 - 站立")]
    public ModelSetting playerStandState;
    [Header("角色參數 - 蹲下")]
    public ModelSetting playerCrouchState;
    [Header("角色參數 - 趴下")]
    public ModelSetting playerProneState;

    private bool isSprint;

    private float playerGravity;                       // 玩家重力值
    private float cameraHeight;                        // 攝影機高度
    private float cameraHeightVelocity;                // 攝影機變換速度 (程式自定義)
    private float stateCapsuleHeightVelocity;          // 碰撞器高度改變速度 (程式自定義)
    private float stateCheckErrorMargin = 0.05f;       // 確認高度額外加的包容值

    private Vector2 input_Movement;                    // 鍵盤輸入值
    private Vector2 input_View;                        // 滑鼠視角值
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

        inputSystem.Enable();

        newCameraRotation = cameraTransform.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;
        cameraHeight = cameraTransform.localPosition.y;

    }

    private void Update()
    {
        CalculateJump();
        CalculateState();
        Move();
        View();
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

        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime), ref newMovementSpeedVelocity, playerUnfreeSetting.movementSmooth);
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
        if (playerGravity > gravity_Min)
        {
            playerGravity -= gravityValue * Time.deltaTime;
        }

        if (playerGravity < -0.1f && characterController.isGrounded)
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
        if (!characterController.isGrounded || playerUnfreeSetting.playerStates == PlayerState.Prone) // 假如不再地上 > 不執行回傳
        {
            return;
        }

        if (playerUnfreeSetting.playerStates == PlayerState.Crouch) // 蹲下 & 趴下狀態的話 > 站立 
        {
            playerUnfreeSetting.playerStates = PlayerState.Stand;
            return;
        }

        jumpForce = Vector3.up * playerUnfreeSetting.jumpHeight;
        playerGravity = 0;
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

    #endregion
}
