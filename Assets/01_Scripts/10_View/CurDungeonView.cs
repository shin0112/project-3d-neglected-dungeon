using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 현재 던전 진행 정보 표시
/// </summary>
public class CurDungeonView : UIView, ICurDungeonView
{
    CurDungeonPresenter _presenter;

    [Header("현재 던전 정보")]
    [SerializeField] private TextMeshProUGUI _curDungeon;
    [SerializeField] private Image _progress;
    [SerializeField] private Button _battleBossButton;

    protected override void Reset()
    {
        base.Reset();

        _progress = transform.FindChild<Image>("Image - CurDungeon Progress");
        _curDungeon = transform.FindChild<TextMeshProUGUI>("Text (TMP) - CurDungeon");
        _battleBossButton = transform.FindChild<Button>("Button - BattleBoss");
    }

    private void Start()
    {
        _presenter = new(this);
        _battleBossButton.onClick.AddListener(OnClickBattleBossButton);
    }

    private void OnDestroy()
    {
        _battleBossButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// [public] 던전을 새로 입장할 때 진행 상황과 보스 소환 정보 초기화
    /// </summary>
    public void ResetDungeon()
    {
        UpdateProgress(0f);
        _battleBossButton.gameObject.SetActive(false);
    }

    public void UpdateCurDungeonText(string name)
    {
        _curDungeon.text = $"{name} | 진행 상황";
    }

    public void UpdateProgress(float progress)
    {
        _progress.fillAmount = progress;

        if (progress > 1)
        {
            _battleBossButton.gameObject.SetActive(true);
        }
    }

    public void OnClickBattleBossButton()
    {
        _presenter.OnClickBattleBossButton();
    }
}
