using System;
using System.Collections.Generic;

/// <summary>
/// 플레이어 보유 통화(재산) 정보를 관리하는 컨테이너 
/// </summary>
[System.Serializable]
public class PlayerWallet
{
    #region 필드
    private readonly Dictionary<WalletType, Wallet> _wallets;
    public Wallet this[WalletType type] => _wallets[type];      // 인덱서 문법 사용
    #endregion

    #region 초기화 & 파괴
    public PlayerWallet()
    {
        // todo: 플레이어 초기화 시 저장된 값 불러오기
        _wallets = new()
        {
            { WalletType.DungeonKey, new(5, WalletType.DungeonKey) },
            { WalletType.Gold, new(10, WalletType.Gold) },
            { WalletType.Gem, new(0, WalletType.Gem) }
        };
    }

    /// <summary>
    /// [public] Player가 파괴될 때 사용
    /// </summary>
    public void OnDestroy()
    {
        foreach (Wallet wallet in _wallets.Values)
        {
            wallet.OnDestroy();
        }
    }
    #endregion

    #region [public] 초기화 - View 용
    public void InitHeaderView()
    {
        foreach (Wallet wallet in _wallets.Values)
        {
            wallet.SyncView();
        }
    }
    #endregion

    #region [public] 통화 사용
    /// <summary>
    /// WallType을 입력해서 값 사용
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryUse(WalletType type, int amount)
    {
        return _wallets[type].TryUse(amount);
    }
    #endregion
}

#region 재화 관리용 클래스
/// <summary>
/// 재화 관리용 클래스
/// </summary>
public class Wallet
{
    // 필드
    public int Value { get; private set; }
    private WalletType _type;

    // 이벤트
    public event Action<int> OnValueChanged;

    public Wallet(int value, WalletType type)
    {
        Value = value;
        _type = type;
    }

    /// <summary>
    /// [public] View 초기화용 함수
    /// </summary>
    public void SyncView() => OnValueChanged?.Invoke(Value);

    /// <summary>
    /// [public] 초기화 함수
    /// </summary>
    public void OnDestroy()
    {
        OnValueChanged = null;
    }

    /// <summary>
    /// [public] 특정 값 만큼 재화 사용 시도. 변경 다음 OnValueChanged 호출
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryUse(int amount)
    {
        if (Value < amount)
        {
            Logger.Log($"{_type} 부족");
            return false;
        }

        Value -= amount;
        OnValueChanged?.Invoke(Value);
        return true;
    }

    /// <summary>
    /// [public] 특정 값 만큼 재화 획득
    /// </summary>
    /// <param name="amount"></param>
    public void Add(int amount)
    {
        Value += amount;
        OnValueChanged?.Invoke(Value);
    }
}
#endregion
