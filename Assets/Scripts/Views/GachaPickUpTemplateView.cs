using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaPickUpTemplateView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, string imagePath)
    {
        nameText.text = data1.name;
        rarityText.text = data2.name;
        characterImage.sprite = Resources.Load<Sprite>(imagePath);
        characterImage.preserveAspect = true;
    }
}
