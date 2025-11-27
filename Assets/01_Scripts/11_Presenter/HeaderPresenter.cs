/// <summary>
/// UI 상단 재화 뷰(HeaderView)와 관련된 데이터를 연결하는 Presenter
/// </summary>
public class HeaderPresenter
{
    private IHeaderView _view;

    /// <summary>
    /// Header View Start 시점에 생성
    /// </summary>
    /// <param name="view"></param>
    public HeaderPresenter(IHeaderView view)
    {
        _view = view;

        // 이벤트 구독
        PlayerCondition condition = Managers.Instance.Player.Condition;

        condition.OnLevelChanged += OnLevelChanged;
        condition.OnExpChanged += OnExpChanged;

        condition.InitHeaderView();
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
