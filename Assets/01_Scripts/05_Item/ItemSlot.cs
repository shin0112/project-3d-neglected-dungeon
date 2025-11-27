using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 슬롯 추상 클래스
/// </summary>
[RequireComponent(typeof(Button))]
public abstract class ItemSlot : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] protected Button button;

    [Header("자식 컴포넌트")]
    [SerializeField] protected Image itemClass;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI text;

    [Header("데이터")]
    [SerializeField] protected ItemData data;


    protected void Reset()
    {
        button = GetComponent<Button>();

        itemClass = transform.FindChild<Image>("Image - Class");
        icon = transform.FindChild<Image>("Image - Icon");
        text = transform.FindChild<TextMeshProUGUI>("Text");

        itemClass.color = Define.ColorNone;
        icon.sprite = null;
    }

    protected void OnEnable()
    {
        button.onClick.AddListener(OnClickButton);
    }

    protected void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    protected abstract void OnClickButton();
}
