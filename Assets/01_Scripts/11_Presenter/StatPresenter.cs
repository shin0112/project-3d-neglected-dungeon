/// <summary>
/// StatView와 스탯 관련 데이터를 연결하는 Presenter
/// </summary>
public class StatPresenter
{
    private readonly IStatView _view;

    /// <summary>
    /// Stat View Start 시점에 생성
    /// </summary>
    /// <param name="view"></param>
    public StatPresenter(IStatView view)
    {
        _view = view;

        PlayerCondition condition = Managers.Instance.Player.Condition;
        condition.OnStaminaChanged += OnStaminaChanged;

        condition.InitStatView();
    }

    #region 스테미나
    public void OnStaminaChanged(float stamina)
    {
        _view.UpdateStaminaProgress(stamina / Define.MaxStamina);
    }
    #endregion
}
