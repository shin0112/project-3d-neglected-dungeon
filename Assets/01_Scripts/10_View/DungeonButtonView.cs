using UnityEngine;
using UnityEngine.UI;

public class DungeonButtonView : UIView
{
    [Header("스킬")]
    [SerializeField] private Button[] _skillButtons = new Button[Define.MaxDisplaySkills];

    [Header("버튼")]
    [SerializeField] private Button _inventoryButton;

    protected override void Reset()
    {
        base.Reset();

        _skillButtons[0] = transform.FindChild<Button>("Button - SkillSlot_0");
        _skillButtons[1] = transform.FindChild<Button>("Button - SkillSlot_1");
        _skillButtons[2] = transform.FindChild<Button>("Button - SkillSlot_2");

        _inventoryButton = transform.FindChild<Button>("Button - Inventory");
    }

    // todo: 버튼과 스킬 정보 연동

    // todo: 인벤토리 화면으로 이동
}
