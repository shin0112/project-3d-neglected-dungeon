using UnityEngine;
using UnityEngine.UI;

public class StatView : UIView
{
    [Header("스테미나")]
    [SerializeField] private Image _staminaProgress;

    protected override void Reset()
    {
        base.Reset();

        _staminaProgress = transform.FindChild<Image>("Image - Stamina Progress");
    }

    // todo: 코루틴 애니메이션
    public void UpdateStaminaProgress(float progress)
    {
        _staminaProgress.fillAmount = progress;
    }
}
