using UnityEngine;
using static data_WeaponData;

public class scr_WeaponController : MonoBehaviour
{

    [Header("武器設定值")]
    public WeaponSetting weaponSetting;

    private bool isInitialised;  // 是否初始化

    private Vector3 newWeaponRotation;              // 武器座標
    private Vector3 newWeaponRotationVelocity;      // 武器座標變換速度 (程式自定義)

    private Vector3 targetWeaponRotation;           // 計算座標
    private Vector3 targetWeaponRotationVelocity;   // 計算座標變換速度 (程式自定義)

    private scr_CharacterController characterController;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialise(scr_CharacterController CharacterController)
    {
        characterController = CharacterController;
        isInitialised = true;
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

        targetWeaponRotation.x += weaponSetting.swayValue * (weaponSetting.swayY_inverted ? characterController.input_View.y : -characterController.input_View.y) * Time.deltaTime;
        targetWeaponRotation.y += weaponSetting.swayValue * (weaponSetting.swayX_inverted ? -characterController.input_View.x : characterController.input_View.x) * Time.deltaTime;

        targetWeaponRotation.x = Mathf.Clamp(targetWeaponRotation.x, -weaponSetting.swayClampX, weaponSetting.swayClampX);
        targetWeaponRotation.y = Mathf.Clamp(targetWeaponRotation.y, -weaponSetting.swayClampY, weaponSetting.swayClampY);

        targetWeaponRotation = Vector3.SmoothDamp(targetWeaponRotation, Vector3.zero, ref targetWeaponRotationVelocity, weaponSetting.swayResetSmooth);
        newWeaponRotation = Vector3.SmoothDamp(newWeaponRotation, targetWeaponRotation, ref newWeaponRotationVelocity, weaponSetting.swaySmooth);

        transform.localRotation = Quaternion.Euler(newWeaponRotation);
    }
}
