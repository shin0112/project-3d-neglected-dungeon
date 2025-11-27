using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon : UIView
{
    [Header("던전 정보")]
    [SerializeField] private DungeonData _data;
    [SerializeField] private TextMeshProUGUI _dungeonName;

    [SerializeField] private Button _button;

    protected override void Reset()
    {
        base.Reset();

        _dungeonName = transform.FindChild<TextMeshProUGUI>("Text (TMP) - Name");
        _button = GetComponent<Button>();
    }

    private void Awake()
    {
        _dungeonName.text = _data.dungeonName;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClickButton()
    {
        Managers.Instance.Dungeon.StartDungeon(_data);
    }
}
