using TMPro;
using UnityEngine;

public class ProfileView : UIView, IProfileView
{
    private ProfilePresenter _presenter;

    [Header("이름")]
    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("스탯")]
    [SerializeField] private TextMeshProUGUI _attakPowerText;
    [SerializeField] private TextMeshProUGUI _defensePowerText;
    [SerializeField] private TextMeshProUGUI _maxHealthText;

    // todo: 장비창

    protected override void Reset()
    {
        base.Reset();

        _nameText = transform.FindChild<TextMeshProUGUI>("Text - Name");
        _attakPowerText = transform.FindChild<TextMeshProUGUI>("Text - Attak Power");
        _defensePowerText = transform.FindChild<TextMeshProUGUI>("Text - Defense Power");
        _maxHealthText = transform.FindChild<TextMeshProUGUI>("Text - Heath");
    }

    private void Start()
    {
        _presenter = new(this);
    }

    public void UpdateNameText(string name)
    {
        throw new System.NotImplementedException();
    }

    #region 프로필 스텟 텍스트 관리
    public void UpdateAttackPowerText(int atk)
    {
        _attakPowerText.text = atk.ToString();
    }

    public void UpdateDefensePowerText(int def)
    {
        _defensePowerText.text = def.ToString();
    }

    public void UpdateMaxHealthText(int health)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
