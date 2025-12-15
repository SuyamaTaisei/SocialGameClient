using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaLogTemplateView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI dateTimeText;
    [SerializeField] TextMeshProUGUI periodText;

    public void Set(GachaLogTemplateView view, CharacterDataModel data1, CharacterRaritiesModel data2, GachaLogsModel data3, GachaPeriodsModel data4, string imagePath)
    {
        view.characterImage.sprite = Resources.Load<Sprite>(imagePath);
        view.nameText.text = data1.name;
        view.rarityText.text = data2.name;
        view.dateTimeText.text = data3.created_at;
        view.periodText.text = data4.name;
    }
}
