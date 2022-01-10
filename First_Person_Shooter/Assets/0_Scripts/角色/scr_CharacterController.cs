using UnityEngine;

public class scr_CharacterController : MonoBehaviour
{
    #region -- 欄位 -- 
    [HideInInspector]
    [Header("Y視角下限")]
    public float viewClamp_YMin = -70;
    [HideInInspector]
    [Header("Y視角上限")]
    public float viewClamp_YMax = 80;
    [HideInInspector]
    [Header("重力值")]
    public float gravityValue = 0.05f;
    [HideInInspector]
    [Header("最小重力值")]
    public float gravity_Min = -3;
    [HideInInspector]
    [Header("攝影機換位時間")]
    public float cameraSmoothTime;
    [HideInInspector]
    [Header("攝影機高度 - 站立")]
    public float cameraStandHeight = 0.7f;
    [HideInInspector]
    [Header("攝影機高度 - 蹲下")]
    public float cameraCrouchHeight = 0;
    [HideInInspector]
    [Header("攝影機高度 - 趴下")]
    public float cameraProneHeight = -0.6f;

    [Header("角色攝影機座標系統")]
    public Transform cameraTransform;

    [Header("角色控制器參數")]
    public PlayerData playerdata;

    private float playerGravity;           // 玩家重力值
    private float cameraHeight;            // 攝影機高度
    private float cameraHeightVelocity;    // 攝影機變換速度

    private Vector2 input_Movement;        // 鍵盤輸入值
    private Vector2 input_View;            // 滑鼠視角值
    private Vector3 newCameraRotation;     // 攝影機的角度
    private Vector3 newCharacterRotation;  // 角色的角度
    private Vector3 jumpForce;             // 跳躍力道
    private Vector3 jumpFallVelocity;      // 下墜加速度

    private InputSystem inputSystem;
    private CharacterController characterController;

    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputSystem = new InputSystem();

        inputSystem.Character.Movement.performed += p => input_Movement = p.ReadValue<Vector2>();
        inputSystem.Character.View.performed += p => input_View = p.ReadValue<Vector2>();
        inputSystem.Character.Jump.performed += p => Jump();

        inputSystem.Enable();

        newCameraRotation = cameraTransform.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        cameraHeight = cameraTransform.localPosition.y;
    }

    private void Update()
    {
        Move();
        View();
        CalculateJump();
        CalculateCameraHeight();
    }

    #region -- 方法 --

    /// <summary>
    /// 角色移動
    /// </summary>
    private void Move()
    {
        var verticalSpeed = playerdata.walkForwardSpeed * input_Movement.y * Time.deltaTime;
        var horizontalSpeed = playerdata.walkRLSpeed * input_Movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);  // 調整面向

        if (playerGravity > gravity_Min)
        {
            playerGravity -= gravityValue * Time.deltaTime;
        }

        if (playerGravity < -0.1f && characterController.isGrounded)
        {
            playerGravity = -0.1f;
        }

        newMovementSpeed.y += playerGravity;
        newMovementSpeed += jumpForce * Time.deltaTime;

        characterController.Move(newMovementSpeed);
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
        jumpForce = Vector3.SmoothDamp(jumpForce, Vector3.zero, ref jumpFallVelocity, playerdata.jumpSmoothTime);
    }

    /// <summary>
    /// 跳躍功能
    /// </summary>
    private void Jump()
    {
        if (!characterController.isGrounded) // 假如不再地上 > 跳回
        {
            return;
        }

        jumpForce = Vector3.up * playerdata.jumpHeight;
        playerGravity = 0;
    }

    /// <summary>
    /// 計算攝影機高度
    /// </summary>
    private void CalculateCameraHeight()
    {
        var cameraCurrentHeight = cameraStandHeight;

        if (playerdata.playerStates == PlayerData.PlayerState.Crouch)
        {
            cameraCurrentHeight = cameraCrouchHeight;
        }
        else if (playerdata.playerStates == PlayerData.PlayerState.Prone)
        {
            cameraCurrentHeight = cameraProneHeight;
        }

        cameraHeight = Mathf.SmoothDamp(cameraTransform.localPosition.y, cameraCurrentHeight, ref cameraHeightVelocity, cameraSmoothTime);

        cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraHeight, cameraTransform.localPosition.z);
    }

    #endregion
}
