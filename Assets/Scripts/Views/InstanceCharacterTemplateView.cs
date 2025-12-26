using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterTemplateView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI characterRarityText;
    [SerializeField] TextMeshProUGUI characterLevelText;

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, CharacterInstancesModel data3, string imagePath)
    {
        if (characterImage) characterImage.sprite = Resources.Load<Sprite>(imagePath);
        if (characterNameText) characterNameText.text = data1.name;
        if (characterRarityText) characterRarityText.text = data2.name;
        if (characterLevelText) characterLevelText.text = data3.level.ToString() + "/" + GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX;
    }
}
