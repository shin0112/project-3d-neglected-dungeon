using UnityEngine;

/// <summary>
/// 게임에 필요한 상수를 정의합니다.
/// </summary>
public static class Define
{
    #region 던전
    // 몬스터
    public static int KillCountForMidBossSpawn = 100;
    public static float RespawnDelay = 3f;

    // 던전 맵
    public static float TileSize = 1f;
    public static LayerMask FloorLayer = 1 << 6;

    public static int MaxKeyCount = 5;
    #endregion

    #region 캐릭터 정보
    // 스텟
    public static float DefaultHealth = 100f;
    public static float DefaultStamina = 100f;
    public static float DefaultAttackPower = 5f;
    public static float DefaultDefensePower = 5f;

    public static float MaxStamina = 100f;

    // 스킬
    public static int MaxDisplaySkills = 3;
    #endregion

    #region 아이템 정보
    // 아이템 클래스 색상
    public static Color ColorNone = Color.gray;
    public static Color ColorNormal = Color.green;
    public static Color ColorRare = Color.blue;
    public static Color ColorElite = Color.magenta;
    public static Color ColorUnique = Color.yellow;

    // 강화
    public static int UpgradeDefaultGold = 20;
    public static float UpgradeProbability = 0.5f;
    #endregion
}
