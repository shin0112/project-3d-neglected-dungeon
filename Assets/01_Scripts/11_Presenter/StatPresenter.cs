public class StatPresenter
{
    private readonly IStatView _view;

    public StatPresenter(IStatView view)
    {
        _view = view;
        Managers.Instance.Player.Condition.OnStaminaChanged += OnStaminaChanged;
    }

    #region 스테미나
    public void OnStaminaChanged(float stamina)
    {
        _view.UpdateStaminaProgress(stamina / Define.MaxStamina);
    }
    #endregion
}
