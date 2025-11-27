using System;

/// <summary>
/// 플레이어 보유 통화(재산) 관리
/// </summary>
[System.Serializable]
public class PlayerWallet
{
    #region 필드
    // 던전
    public Wallet Key { get; private set; }

    // 재화
    public Wallet Gold { get; private set; }
    public Wallet Gem { get; private set; }

    // 이벤트
    public event Action<int> OnKeyChanged;

    public event Action<int> OnGoldChanged;
    public event Action<int> OnGemChanged;
    #endregion

    #region 초기화 & 파괴
    public PlayerWallet()
    {
        Key = new(5, WalletType.DungeonKey);
        Gold = new(0, WalletType.Gold);
        Gem = new(0, WalletType.Gem);

        EnrollEvents();
    }

    private void EnrollEvents()
    {
        Key.OnValueChanged += OnKeyChanged;
        Gold.OnValueChanged += OnGoldChanged;
        Gem.OnValueChanged += OnGemChanged;
    }

    /// <summary>
    /// Player가 파괴될 때 사용. 너무 값이 많아지면 list로 관리
    /// </summary>
    public void OnDestroy()
    {
        Key.OnDestroy();
        Gold.OnDestroy();
        Gem.OnDestroy();
    }
    #endregion

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
        /// 특정 수 만큼 재화 사용 시도. 변경 다음 OnValueChanged 호출
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
        /// 초기화 함수
        /// </summary>
        public void OnDestroy()
        {
            OnValueChanged = null;
        }
    }

    #region [public] 초기화 - View 용
    public void InitHeaderView()
    {
        OnKeyChanged?.Invoke(Key.Value);
        OnGoldChanged?.Invoke(Gold.Value);
        OnGemChanged?.Invoke(Gem.Value);
    }
    #endregion

    #region [public] 통화 사용
    public bool TryUseKey(int amount) => Key.TryUse(amount);

    public bool TryUseGold(int amount) => Gold.TryUse(amount);

    public bool TryUseGem(int amount) => Gem.TryUse(amount);
    #endregion
}
