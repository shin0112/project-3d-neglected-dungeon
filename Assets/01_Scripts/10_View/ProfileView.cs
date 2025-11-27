using TMPro;
using UnityEngine;

public class ProfileView : UIView, IProfileView
{
    [Header("이름")]
    [SerializeField] private TextMeshProUGUI _nameText;

    [Header("스탯")]
    [SerializeField] private TextMeshProUGUI _attakPowerText;
    [SerializeField] private TextMeshProUGUI _defensePowerText;
    [SerializeField] private TextMeshProUGUI _maxHealthText;

    // todo: 장비창

    public void UpdateAttackPowerText(int atk)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateDefensePowerText(int def)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMaxHealthText(int health)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateNameText(string name)
    {
        throw new System.NotImplementedException();
    }
}
