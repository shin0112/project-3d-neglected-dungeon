using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderView : UIView, IHeaderView
{
    private HeaderPresenter _presenter;

    [Header("레벨")]
    [SerializeField] private Image _levelProgress;
    [SerializeField] private TextMeshProUGUI _levelText;

    [Header("열쇠")]
    [SerializeField] private TextMeshProUGUI _keyText;

    [Header("재화")]
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _gemText;

    private void Start()
    {
        _presenter = new HeaderPresenter(this);
    }

    protected override void Reset()
    {
        base.Reset();

        _levelProgress = transform.FindChild<Image>("Image - LevelProgress");
        _levelText = transform.FindChild<TextMeshProUGUI>("Text (TMP) - Level");

        _keyText = transform.FindChild<TextMeshProUGUI>("Text (TMP) - Key");

        _goldText = transform.FindChild<TextMeshProUGUI>("Text (TMP) - Gold");
        _gemText = transform.FindChild<TextMeshProUGUI>("Text (TMP) - Gem");
    }

    // todo: 레벨업 시 경험치 비율 확인해서 progress바 코루틴으로 애니메이션 연출 적용
    public void UpdateLevelProgress(float progress)
    {
        _levelProgress.fillAmount = progress;
    }

    #region 헤더 텍스트 관리
    public void UpdateLevelText(int level)
    {
        _levelText.text = level.ToString();
    }

    public void UpdateKeyText(int key)
    {
        _keyText.text = $"{key}/{Define.MaxKeyCount}";
    }

    public void UpdateGoldText(int gold)
    {
        _goldText.text = gold.ToString();
    }

    public void UpdateGemText(int gem)
    {
        _gemText.text = gem.ToString();
    }
    #endregion
}
