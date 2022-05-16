using UnityEngine;

[System.Serializable]
public class data_WeaponData : ScriptableObject
{
    [System.Serializable]
    public class WeaponSetting
    {
        [Header("滑鼠 - 武器搖擺值")]
        public float swayValue = 4;
        [Header("滑鼠 - 搖擺滑順值")]
        public float swaySmooth = 0.1f;
        [Header("滑鼠 - 搖擺回歸原點滑順值")]
        public float swayResetSmooth = 0.1f;
        [Header("滑鼠 - X軸反轉")]
        public bool swayX_inverted;
        [Header("滑鼠 - Y軸反轉")]
        public bool swayY_inverted;
        [Header("滑鼠 - X視角限制")]
        public float swayClampX = 6;
        [Header("滑鼠 - Y視角限制")]
        public float swayClampY = 6;

        [Header("鍵盤 - 武器 Z軸搖擺值")]
        public float movementSwayX = 1500;
        [Header("鍵盤 - 武器 X軸搖擺值")]
        public float movementSwayY = 600;
        [Header("鍵盤 - 搖擺滑順值")]
        public float movemenSwaySmooth = 0.1f;
        [Header("鍵盤 - Z軸反轉")]
        public bool movementSwayX_inverted;
        [Header("鍵盤 - X軸反轉")]
        public bool movementSwayY_inverted;

    }
}
