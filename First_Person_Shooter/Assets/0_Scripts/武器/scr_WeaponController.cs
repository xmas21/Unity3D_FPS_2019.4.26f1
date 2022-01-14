using UnityEngine;
using static data_WeaponData;

public class scr_WeaponController : MonoBehaviour
{
    [Header("武器設定值")]
    public WeaponSetting weaponSetting;

    [Header("呼吸 - 武器搖擺物件")]
    public Vector3 swayPosition;
    [Header("移動 - 武器搖擺物件")]
    public Transform weaponSwayObject;
    [Header("瞄準視角")]
    public Transform sightTarget;

    [Header("曲線參數分母 > 影響擺幅")]
    public float breathSwayScale = 300;

    [HideInInspector]
    public bool isAiming;                       // 是否瞄準中
    [HideInInspector]
    public float sightOffset;                   // 瞄準視角位移
    [HideInInspector]
    public float aimingTime;                    // 瞄準視角時間

    private bool isInitialised;                 // 是否初始化
    private bool isGroundedTrigger;             // 著地觸發器
    private bool isFallingTrigger;              // 下降觸發器

    private float breathSwayValue_A = 1;        // 利薩茹曲線參數 A
    private float breathSwayValue_B = 2;        // 利薩茹曲線參數 B
    private float fallingDelay;                 // 下墜判斷延遲
    private float breathSwayLerpSpeed = 20;     // Lerp 參數
    private float breathSwayTime;               // 曲線時間參數 > 不設值僅宣告

    private Vector3 weaponSwayPosition;                     // 武器搖擺座標
    private Vector3 weaponSwayPositionVelocity;             // 武器搖擺座標
    private Vector3 newWeaponRotation;                      // 武器座標
    private Vector3 newWeaponRotationVelocity;              // 武器座標變換速度 (程式自定義)
    private Vector3 targetWeaponRotation;                   // 計算座標
    private Vector3 targetWeaponRotationVelocity;           // 計算座標變換速度 (程式自定義)
    private Vector3 newWeaponMovementRotation;              // 武器座標
    private Vector3 newWeaponMovementRotationVelocity;      // 武器座標變換速度 (程式自定義)
    private Vector3 targetWeaponMovementRotation;           // 計算座標
    private Vector3 targetWeaponMovementRotationVelocity;   // 計算座標變換速度 (程式自定義)

    private scr_CharacterController characterController;
    private Animator ani;

    private void Awake()
    {
        ani = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        newWeaponRotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        if (!isInitialised)
        {
            return;
        }

        SetWeaponAnimation();
        GunSway();
        CalculateWeaponbreathSway();
        CalculateAiming();
    }

    /// <summary>
    /// 利薩茹曲線
    /// </summary>
    /// <param name="Time"> 參數 : 不設定數值 </param>
    /// <param name="A"> 曲線參數 A </param>
    /// <param name="B"> 曲線參數 B </param>
    /// <returns></returns>
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }

    /// <summary>
    /// 初始化 => 幫忙抓取腳本
    /// </summary>
    /// <param name="CharacterController">角色控制器的腳本</param>
    public void Initialise(scr_CharacterController CharacterController)
    {
        characterController = CharacterController;
        isInitialised = true;
    }

    /// <summary>
    /// 跳躍觸發器
    /// </summary>
    public void TriggerJump()
    {
        isGroundedTrigger = false;
        isFallingTrigger = true;
        ani.SetTrigger("Jump");
    }

    /// <summary>
    /// 槍搖擺
    /// </summary>
    private void GunSway()
    {
        // View Sway
        targetWeaponRotation.x += weaponSetting.swayValue * (weaponSetting.swayY_inverted ? characterController.input_View.y : -characterController.input_View.y) * Time.deltaTime;
        targetWeaponRotation.y += weaponSetting.swayValue * (weaponSetting.swayX_inverted ? -characterController.input_View.x : characterController.input_View.x) * Time.deltaTime;

        targetWeaponRotation.x = Mathf.Clamp(targetWeaponRotation.x, -weaponSetting.swayClampX, weaponSetting.swayClampX);
        targetWeaponRotation.y = Mathf.Clamp(targetWeaponRotation.y, -weaponSetting.swayClampY, weaponSetting.swayClampY);
        targetWeaponRotation.z = targetWeaponRotation.y;

        targetWeaponRotation = Vector3.SmoothDamp(targetWeaponRotation, Vector3.zero, ref targetWeaponRotationVelocity, weaponSetting.swayResetSmooth);
        newWeaponRotation = Vector3.SmoothDamp(newWeaponRotation, targetWeaponRotation, ref newWeaponRotationVelocity, weaponSetting.swaySmooth);

        // Movement Sway
        targetWeaponMovementRotation.z = weaponSetting.movementSwayX * (weaponSetting.movementSwayX_inverted ? characterController.input_Movement.x : -characterController.input_Movement.x) * Time.deltaTime;
        targetWeaponMovementRotation.x = weaponSetting.movementSwayY * (weaponSetting.movementSwayY_inverted ? -characterController.input_Movement.y : characterController.input_Movement.y) * Time.deltaTime;

        targetWeaponMovementRotation = Vector3.SmoothDamp(targetWeaponMovementRotation, Vector3.zero, ref targetWeaponMovementRotationVelocity, weaponSetting.movemenSwaySmooth);
        newWeaponMovementRotation = Vector3.SmoothDamp(newWeaponMovementRotation, targetWeaponMovementRotation, ref newWeaponMovementRotationVelocity, weaponSetting.movemenSwaySmooth);

        transform.localRotation = Quaternion.Euler(newWeaponRotation + newWeaponMovementRotation);
    }

    /// <summary>
    /// 設定動畫切換
    /// </summary>
    private void SetWeaponAnimation()
    {
        if (isGroundedTrigger)
        {
            fallingDelay = 0;
        }
        else
        {
            fallingDelay += Time.deltaTime;
        }

        if (characterController.isGround && !isGroundedTrigger && fallingDelay > 0.1f)
        {
            ani.SetTrigger("Land");
            isGroundedTrigger = true;
        }

        if (!characterController.isGround && isGroundedTrigger || !characterController.isGround && isFallingTrigger)
        {
            ani.SetTrigger("Fall");
            isGroundedTrigger = false;
            isFallingTrigger = false;
        }

        ani.SetBool("isSpirint", characterController.isSprint);
        ani.SetFloat("weaponAnimationSpeed", characterController.weaponAnimationSpeed);
    }

    /// <summary>
    /// 呼吸武器搖擺
    /// </summary>
    private void CalculateWeaponbreathSway()
    {
        var targetPosition = LissajousCurve(breathSwayTime, breathSwayValue_A, breathSwayValue_B) / breathSwayScale;

        swayPosition = Vector3.Lerp(swayPosition, targetPosition, Time.smoothDeltaTime * breathSwayLerpSpeed);
        breathSwayTime += Time.deltaTime;

        if (breathSwayTime > 6.3f)
        {
            breathSwayTime = 0;
        }

        // weaponSwayObject.localPosition = swayPosition;
    }

    /// <summary>
    /// 計算瞄準
    /// </summary>
    private void CalculateAiming()
    {
        var targetPosition = transform.position;

        if (isAiming)
        {
            targetPosition = characterController.cameraTransform.transform.position + (weaponSwayObject.transform.position - sightTarget.transform.position) + (characterController.cameraTransform.transform.forward * sightOffset);
        }

        weaponSwayPosition = weaponSwayObject.transform.position;
        weaponSwayPosition = Vector3.SmoothDamp(weaponSwayPosition, targetPosition, ref weaponSwayPositionVelocity, aimingTime);
        weaponSwayObject.transform.position = weaponSwayPosition;
    }
}
