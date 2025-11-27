public class StatPresenter
{
    private readonly IStatView _view;
    // stat

    public StatPresenter(IStatView view)
    {
        _view = view;
    }

    #region 스테미나
    public void OnStaminaChanged()
    {
        float stamina = 100f;       // todo: stat model의 stamina 가져오기
        _view.UpdateStaminaProgress(stamina / Define.MaxStamina);
    }
    #endregion
}
