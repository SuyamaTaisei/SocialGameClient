using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterDetailFixedView : MonoBehaviour
{
    [SerializeField] Image characterDetailImage;
    [SerializeField] TextMeshProUGUI characterDetailNameText;
    [SerializeField] TextMeshProUGUI characterDetailRarityText;
    [SerializeField] TextMeshProUGUI characterDetailLevelBeforeText;
    [SerializeField] TextMeshProUGUI characterDetailLevelAfterText;
    [SerializeField] Button enhanceButton;

    private int beforeLevel;
    private int afterLevel;

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, CharacterInstancesModel data3, string imagePath)
    {
        if (characterDetailImage) characterDetailImage.sprite = Resources.Load<Sprite>(imagePath);
        if (characterDetailNameText) characterDetailNameText.text = data1.name;
        if (characterDetailRarityText) characterDetailRarityText.text = data2.name;

        beforeLevel = data3.level;
        if (characterDetailLevelBeforeText) characterDetailLevelBeforeText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + data3.level.ToString();

        afterLevel = beforeLevel;
        if (characterDetailLevelAfterText) characterDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + afterLevel;

        SetCtrlEnhanceButton();
    }

    //レベルアップ表記のプレビュー更新処理
    public void SetAddAfterLevel(int value)
    {
        afterLevel += value;
        characterDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + afterLevel;

        SetCtrlEnhanceButton();
    }

    //強化ボタン押下制御
    private void SetCtrlEnhanceButton()
    {
        enhanceButton.interactable = (afterLevel != beforeLevel) && (afterLevel <= int.Parse(GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX));
    }

    //強化ボタン押下時の処理を書く
}
