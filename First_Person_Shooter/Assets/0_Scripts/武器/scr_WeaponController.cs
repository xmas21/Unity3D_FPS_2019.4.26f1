﻿using UnityEngine;
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

    private Vector3 newWeaponMovementRotation;              // 武器座標
    private Vector3 newWeaponMovementRotationVelocity;      // 武器座標變換速度 (程式自定義)

    private Vector3 targetWeaponMovementRotation;           // 計算座標
    private Vector3 targetWeaponMovementRotationVelocity;   // 計算座標變換速度 (程式自定義)

    private scr_CharacterController characterController;

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

        GunSway();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialise(scr_CharacterController CharacterController)
    {
        characterController = CharacterController;
        isInitialised = true;
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

}
