using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaPickUpTemplateView : MonoBehaviour
{
    [SerializeField] Image gachaPickUpImage;
    [SerializeField] TextMeshProUGUI gachaPickUpNameText;
    [SerializeField] TextMeshProUGUI gachaPickUpRarityText;

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, string imagePath)
    {
        gachaPickUpNameText.text = data1.name;
        gachaPickUpRarityText.text = data2.name;
        gachaPickUpImage.sprite = Resources.Load<Sprite>(imagePath);
        gachaPickUpImage.preserveAspect = true;
    }
}
