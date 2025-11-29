using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 던전 진행 시 보이는 현재 스텟(스테미나, 체력 등) 정보 View
/// todo: 현재 체력 정보 추가
/// </summary>
public class StatView : UIView, IStatView
{
    [Header("스테미나")]
    [SerializeField] private Image _staminaProgress;

    private StatPresenter _presenter;

    #region 초기화
    protected override void Reset()
    {
        base.Reset();

        _staminaProgress = transform.FindChild<Image>("Image - Stamina Progress");
    }

    private void Start()
    {
        _presenter = new(this);
    }
    #endregion

    #region 스테미나
    // todo: 코루틴 애니메이션
    public void UpdateStaminaProgress(float progress)
    {
        _staminaProgress.fillAmount = progress;
    }
    #endregion
}
