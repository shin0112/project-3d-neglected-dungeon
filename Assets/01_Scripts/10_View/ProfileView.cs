using TMPro;
using UnityEngine;

/// <summary>
/// 플레이어 정보 (이름, 전체 스텟) 표시
/// </summary>
public class ProfileView : UIView, IProfileView
{
    private ProfilePresenter _presenter;

    [Header("이름")]
    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("스탯")]
    [SerializeField] private TextMeshProUGUI _attakPowerText;
    [SerializeField] private TextMeshProUGUI _defensePowerText;
    [SerializeField] private TextMeshProUGUI _totalHealthText;

    // todo: 장비창

    protected override void Reset()
    {
        base.Reset();

        _nameText = transform.FindChild<TextMeshProUGUI>("Text - Name");
        _attakPowerText = transform.FindChild<TextMeshProUGUI>("Text - Attak Power");
        _defensePowerText = transform.FindChild<TextMeshProUGUI>("Text - Defense Power");
        _totalHealthText = transform.FindChild<TextMeshProUGUI>("Text - Heath");
    }

    private void Start()
    {
        _presenter = new(this);
    }

    public void UpdateNameText(string name)
    {
        _nameText.text = name;
    }

    #region 프로필 스텟 텍스트 관리
    public void UpdateAttackText(int attack)
    {
        _attakPowerText.text = attack.ToString();
    }

    public void UpdateDefenseText(int defense)
    {
        _defensePowerText.text = defense.ToString();
    }

    public void UpdateTotalHealthText(int health)
    {
        _totalHealthText.text = health.ToString();
    }
    #endregion
}
