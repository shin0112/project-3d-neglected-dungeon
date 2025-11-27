/// <summary>
/// ProfileView와 관련 데이터를 연결하는 Presenter
/// </summary>
public class ProfilePresenter
{
    IProfileView _view;

    public ProfilePresenter(IProfileView view)
    {
        _view = view;

        PlayerCondition condition = Managers.Instance.Player.Condition;

        condition[StatType.Attack].OnMaxValueChanged += OnTotalAttackChanged;
        condition[StatType.Defense].OnMaxValueChanged += OnTotalDefenseChanged;
        condition[StatType.Health].OnMaxValueChanged += OnTotalHealthChanged;

        condition.InitProfileView();
        UpdateNameText(condition.Name);
    }

    public void UpdateNameText(string name)
    {
        _view.UpdateNameText(name);
    }

    public void OnTotalAttackChanged(float value)
    {
        _view.UpdateAttackText((int)value);
    }

    public void OnTotalDefenseChanged(float value)
    {
        _view.UpdateDefenseText((int)value);
    }

    public void OnTotalHealthChanged(float value)
    {
        _view.UpdateTotalHealthText((int)value);
    }
}
