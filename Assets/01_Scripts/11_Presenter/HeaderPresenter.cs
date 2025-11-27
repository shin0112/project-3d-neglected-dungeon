public class HeaderPresenter
{
    private IHeaderView _view;

    public HeaderPresenter(IHeaderView view)
    {
        _view = view;

        // 이벤트 구독
        PlayerCondition condition = Managers.Instance.Player.Condition;

        condition.OnLevelChanged += OnLevelChanged;
        condition.OnExpChanged += OnExpChanged;
    }

    private void OnLevelChanged(int value)
    {
        _view.UpdateLevelText(value);
    }

    private void OnExpChanged(float progress)
    {
        _view.UpdateLevelProgress(progress);
    }

    private void OnKeyCountChaged(int value)
    {
        _view.UpdateKeyText(value);
    }

    private void OnGoldChanged(int value)
    {
        _view.UpdateGoldText(value);
    }

    private void OnGemChanged(int value)
    {
        _view.UpdateGemText(value);
    }
}
