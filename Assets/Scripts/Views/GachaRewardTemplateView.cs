using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaRewardTemplateView : MonoBehaviour
{
    [SerializeField] Image gachaRewardImage;
    [SerializeField] TextMeshProUGUI gachaRewardNameText;
    [SerializeField] TextMeshProUGUI gachaRewardRarityText;
    [SerializeField] TextMeshProUGUI gachaRewardAmountText;

    public void Set(GachaRewardTemplateView view, ItemDataModel data1, ItemRaritiesModel data2, GachaResultsModel data3, string imagePath)
    {
        view.gachaRewardNameText.text = data1.name;
        view.gachaRewardRarityText.text = data2.name;
        view.gachaRewardAmountText.text = data3.amount.ToString();
        view.gachaRewardImage.sprite = Resources.Load<Sprite>(imagePath);
        view.gachaRewardImage.preserveAspect = true;
    }
}
