using UnityEngine;

[CreateAssetMenu(fileName = "控制器參數", menuName = "Lobo/玩家參數")]
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

    /// <summary>
    /// 角色非自由性控制器參數
    /// </summary>
    [System.Serializable]
    public class PlayerUnfreeSetting
    {
        [Header("角色狀態"), Tooltip("站立 / 蹲下 / 趴下")]
        public PlayerState playerStates;

        [Header("走路 - 前進速度")]
        public float walkForwardSpeed = 4;
        [Header("走路 - 左右速度")]
        public float walkRLSpeed = 3;
        [Header("走路 - 後退速度")]
        public float walkBackwardSpeed = 2;

        [Header("跑步 - 前進速度")]
        public float runForwardSpeed;
        [Header("跑步 - 左右速度")]
        public float runRLSpeed;
        [Header("跑步 - 切換滑順時間")]
        public float movementSmooth;

        [Header("跳躍高度")]
        public float jumpHeight = 6;
        [Header("下墜至地面所需時間")]
        public float jumpSmoothTime = 1;

    }

    /// <summary>
    /// 角色在不同狀態下的參數
    /// </summary>
    [System.Serializable]
    public class ModelSetting
    {
        [Header("攝影機高度")]
        public float CameraHeight;
        [Header("角色碰撞器")]
        public CapsuleCollider stateCollider;
    }

    /// <summary>
    /// 角色狀態
    /// </summary>
    public enum PlayerState
    {
        Stand,
        Crouch,
        Prone
    }

}

