using UnityEngine;

[System.Serializable]
public class data_WeaponData : ScriptableObject
{
    [System.Serializable]
    public class WeaponSetting
    {
        [Header("武器搖擺值")]
        public float swayValue = 4;
        [Header("搖擺滑順值")]
        public float swaySmooth = 0.1f;
        [Header("搖擺回歸原點滑順值")]
        public float swayResetSmooth = 0.1f;
        [Header("X 視角限制")]
        public float swayClampX = 6;
        [Header("Y 視角限制")]
        public float swayClampY = 6;

        [Header("X 軸反轉")]
        public bool swayX_inverted;
        [Header("Y 軸反轉")]
        public bool swayY_inverted;


    }
}
