using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaLogTemplateView : MonoBehaviour
{
    [SerializeField] Image gachaLogImage;
    [SerializeField] TextMeshProUGUI gachaLogNameText;
    [SerializeField] TextMeshProUGUI gachaLogRarityText;
    [SerializeField] TextMeshProUGUI gachaLogDateTimeText;
    [SerializeField] TextMeshProUGUI gachaLogPeriodText;

    public void Set(GachaLogTemplateView view, CharacterDataModel data1, CharacterRaritiesModel data2, GachaLogsModel data3, GachaPeriodsModel data4, string imagePath)
    {
        view.gachaLogImage.sprite = Resources.Load<Sprite>(imagePath);
        view.gachaLogNameText.text = data1.name;
        view.gachaLogRarityText.text = data2.name;
        view.gachaLogDateTimeText.text = data3.created_at;
        view.gachaLogPeriodText.text = data4.name;
    }
}
