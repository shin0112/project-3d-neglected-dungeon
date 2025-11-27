using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemView : UIView, IEquipmentItemView
{
    [Header("아이템 정보")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _class;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _statNValue;
    private ItemData _data;

    [Header("아이템 강화")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TextMeshProUGUI _upgrade;
    private int _upgradeGold;

    protected override void Reset()
    {
        base.Reset();

        _name = transform.FindChild<TextMeshProUGUI>("Text - Name");
        _class = transform.FindChild<TextMeshProUGUI>("Text - Class");
        _description = transform.FindChild<TextMeshProUGUI>("Text -Description");
        _statNValue = transform.FindChild<TextMeshProUGUI>("Text -Stat N Value");

        _upgradeButton = transform.FindChild<Button>("Button - Upgrade");
        _upgrade = transform.FindChild<TextMeshProUGUI>("Text - UpgradeText");
    }

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnClickUpgradeButton);
    }

    private void OnDisable()
    {
        _data = null;
        _upgradeButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// [public] 아이템 정보 View에 전달
    /// </summary>
    /// <param name="data"></param>
    public void UpdateEquipmentItemText(ItemData data)
    {
        CacheItemData(data);

        _name.text = $"{data.Name} {(data.UpgradeLevel > 0 ? "+" + data.UpgradeLevel : "")}";
        _class.text = data.ItemClass.ToString();
        _description.text = data.Description;

        string statNValue = "";
        foreach (EquipmentItemData equipment in data.Equipments)
        {
            statNValue += $"{equipment.Stat} | {equipment.Value}";
        }
        _statNValue.text = statNValue;

        _upgrade.text = $"강화\n(비용: {CalcUpgradeGold()})";
    }

    /// <summary>
    /// 아이템 정보 캐싱하기
    /// </summary>
    /// <param name="data"></param>
    private void CacheItemData(ItemData data)
    {
        _data = data;
    }

    public void OnClickUpgradeButton()
    {
        bool canUse = Managers.Instance.Player.Wallet[WalletType.Gold].TryUse(_upgradeGold);

        if (canUse)
        {
            _data.Upgrade();

            int nextUpgradeGold = CalcUpgradeGold();

            _upgradeGold = nextUpgradeGold;
            _upgrade.text = $"강화\n(비용: {nextUpgradeGold})";
        }
    }

    /// <summary>
    /// 장비 강화에 사용할 골드 계산
    /// </summary>
    /// <returns></returns>
    private int CalcUpgradeGold()
    {
        return (int)(Define.UpgradeDefaultGold * Mathf.Pow(1.3f, _data.UpgradeLevel));
    }
}
