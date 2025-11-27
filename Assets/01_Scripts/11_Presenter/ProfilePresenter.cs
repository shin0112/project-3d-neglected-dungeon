public class ProfilePresenter
{
    IProfileView _view;

    public ProfilePresenter(IProfileView view)
    {
        _view = view;

        PlayerCondition condition = Managers.Instance.Player.Condition;
        condition.OnTotalAttackChanged += OnTotalAttackChanged;
        condition.OnTotalDefenseChanged += OnTotalDefenseChanged;

        condition.InitProfileView();
    }

    public void OnTotalAttackChanged(float value)
    {
        _view.UpdateAttackText((int)value);
    }

    public void OnTotalDefenseChanged(float value)
    {
        _view.UpdateDefenseText((int)value);
    }
}
