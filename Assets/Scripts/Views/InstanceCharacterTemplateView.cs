using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterTemplateView : MonoBehaviour
{
    [SerializeField] Image charaImage;
    [SerializeField] TextMeshProUGUI charaInstanceNameText;
    [SerializeField] TextMeshProUGUI charaInstanceRarityText;
    [SerializeField] TextMeshProUGUI charaInstanceLevelText;

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, CharacterInstancesModel data3, string imagePath)
    {
        if (charaImage) charaImage.sprite = Resources.Load<Sprite>(imagePath);
        if (charaInstanceNameText) charaInstanceNameText.text = data1.name;
        if (charaInstanceRarityText) charaInstanceRarityText.text = data2.name;
        if (charaInstanceLevelText) charaInstanceLevelText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + data3.level.ToString() + "/" + GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX;
    }
}
