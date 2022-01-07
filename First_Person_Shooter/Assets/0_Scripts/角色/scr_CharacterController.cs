using UnityEngine;

public class scr_CharacterController : MonoBehaviour
{

    [Header("Y視角下限")]
    public float viewClamp_YMin = -70;
    [Header("Y視角上限")]
    public float viewClamp_YMax = 80;

    [Header("角色控制器參數")]
    public PlayerData playerdata;

    [Header("角色攝影機座標系統")]
    public Transform cameraTransform;

    private Vector2 input_Movement;       // 鍵盤輸入值
    private Vector2 input_View;           // 滑鼠視角值
    private Vector3 newCameraRotation;    // 攝影機的角度
    private Vector3 newCharacterRotation; // 角色的角度

    private InputSystem inputSystem;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputSystem = new InputSystem();

        inputSystem.Character.Movement.performed += p => input_Movement = p.ReadValue<Vector2>();
        inputSystem.Character.View.performed += p => input_View = p.ReadValue<Vector2>();

        inputSystem.Enable();

        newCameraRotation = cameraTransform.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        CalculateMovement();
        CalculateView();
    }

    /// <summary>
    /// 角色移動
    /// </summary>
    private void CalculateMovement()
    {
        var verticalSpeed = playerdata.walkForwardSpeed * input_Movement.y * Time.deltaTime;
        var horizontalSpeed = playerdata.walkRLSpeed * input_Movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);  // 調整面向

        characterController.Move(newMovementSpeed);
    }

    /// <summary>
    /// 視角移動 / 角色旋轉
    /// </summary>
    private void CalculateView()
    {
        newCharacterRotation.y += playerdata.ViewX_Sensitivity * (playerdata.ViewX_inverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        newCameraRotation.x += playerdata.ViewY_Sensitivity * (playerdata.ViewY_inverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClamp_YMin, viewClamp_YMax);

        cameraTransform.localRotation = Quaternion.Euler(newCameraRotation);
    }

}
