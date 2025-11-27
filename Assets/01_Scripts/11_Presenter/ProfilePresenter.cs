public class ProfilePresenter
{
    IProfileView _view;

    public ProfilePresenter(IProfileView view)
    {
        _view = view;

        PlayerCondition condition = Managers.Instance.Player.Condition;
        condition.OnTotalAttackChanged += OnTotalAttackPowerChanged;
        condition.OnTotalDefenseChanged += OnTotalDefensePowerChanged;

        condition.InitProfileView();
    }

    public void OnTotalAttackPowerChanged(float value)
    {
        _view.UpdateAttackPowerText((int)value);
    }

    public void OnTotalDefensePowerChanged(float value)
    {
        _view.UpdateDefensePowerText((int)value);
    }
}
