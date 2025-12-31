using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterDetailView : MonoBehaviour
{
    [SerializeField] Image characterDetailImage;
    [SerializeField] TextMeshProUGUI characterDetailNameText;
    [SerializeField] TextMeshProUGUI characterDetailRarityText;
    [SerializeField] TextMeshProUGUI characterDetailLevelBeforeText;
    [SerializeField] TextMeshProUGUI characterDetailLevelAfterText;

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, CharacterInstancesModel data3, string imagePath)
    {
        if (characterDetailImage) characterDetailImage.sprite = Resources.Load<Sprite>(imagePath);
        if (characterDetailNameText) characterDetailNameText.text = data1.name;
        if (characterDetailRarityText) characterDetailRarityText.text = data2.name;
        if (characterDetailLevelBeforeText) characterDetailLevelBeforeText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + data3.level.ToString();
        if (characterDetailLevelAfterText) characterDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + data3.level.ToString();
    }
}
