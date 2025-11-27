using UnityEngine;

/// <summary>
/// 게임에 필요한 상수를 정의합니다.
/// </summary>
public static class Define
{
    #region 던전
    // 몬스터
    public static int KillCountForMidBossSpawn = 100;
    public static float RespawnDelay = 10f;

    // 던전 맵
    public static LayerMask FloorLayer = 1 << 6;

    public static int MaxKeyCount = 5;
    #endregion

    #region 스킬
    public static int MaxDisplaySkills = 3;
    #endregion
}
