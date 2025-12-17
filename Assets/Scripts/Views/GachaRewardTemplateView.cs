using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaRewardTemplateView : MonoBehaviour
{
    [SerializeField] Image rewardImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI amountText;

    public void Set(GachaRewardTemplateView view, ItemDataModel data1, ItemRaritiesModel data2, GachaResultsModel data3, string imagePath)
    {
        view.nameText.text = data1.name;
        view.rarityText.text = data2.name;
        view.amountText.text = data3.amount.ToString();
        view.rewardImage.sprite = Resources.Load<Sprite>(imagePath);
        view.rewardImage.preserveAspect = true;
    }
}
