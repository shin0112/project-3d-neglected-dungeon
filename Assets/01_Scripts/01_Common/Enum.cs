#region 스텟
public enum StatType
{
    Health,
    Stamina,
    AttackDistance,
    DetectDistance,
}
#endregion

#region 던전: 맵
public enum CorridorType
{
    Straight,
    Corner,
}
#endregion

#region 던전: 몬스터
public enum MonsterType
{
    Skeleton,
}

public enum MonsterVariant
{
    Mage,
    Minion,
    Rogue,
    Warrior
}

public enum BossType
{
    Middle,
    Final
}
#endregion

#region 아이템
// 아이템 타입
public enum ItemType
{
    Equipment,
    Consumable,
}

// 아이템 등급
public enum ItemClass
{
    Normal,
    Rare,
    Elite,
    Unique
}

// 장비 아이템 타입
public enum EquipmentType
{
    Weapon,
    Armor,
}

// 장비 아이템 착용 시 적용 스텟
public enum EquipmentStats
{
    Attack,
    SkillAttack,
    Health
}

// 소비 아이템 타입
public enum ConsumableType
{
    Portion,
    Buff,
}

// 소비 아이템 사용 시 적용 스텟
public enum ConsumableStats
{
    Health,
    Stamina,
}
#endregion
