using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        _battleBossButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _battleBossButton.onClick.RemoveAllListeners();
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
