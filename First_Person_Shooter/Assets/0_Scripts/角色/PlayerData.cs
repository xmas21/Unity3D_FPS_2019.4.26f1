using UnityEngine;

[CreateAssetMenu(fileName = "控制器參數", menuName = "Lobo/玩家資料")]
[System.Serializable]
public class PlayerData : ScriptableObject
{
    [Header("X 軸靈敏度"), Range(0, 50)]
    public float ViewX_Sensitivity;
    [Header("Y 軸靈敏度"), Range(0, 50)]
    public float ViewY_Sensitivity;

    [Header("X 軸反轉")]
    public bool ViewX_inverted;
    [Header("Y 軸反轉")]
    public bool ViewY_inverted;

    [Header("角色前進移動速度")]
    public float walkForwardSpeed;
    [Header("角色後退移動速度")]
    public float walkBackwardSpeed;
    [Header("角色左右移動速度")]
    public float walkRLSpeed;
}
