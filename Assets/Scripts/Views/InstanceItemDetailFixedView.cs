using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceItemDetailFixedView : MonoBehaviour
{
    [SerializeField] Image detailItemImage;
    [SerializeField] TextMeshProUGUI detailItemNameText;
    [SerializeField] TextMeshProUGUI detailItemRarityText;
    [SerializeField] TextMeshProUGUI detailItemDescriptionText;
    [SerializeField] TextMeshProUGUI detailItemAmountText;

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (detailItemImage) detailItemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (detailItemNameText) detailItemNameText.text = data1.name;
        if (detailItemRarityText) detailItemRarityText.text = data2.name;
        if (detailItemDescriptionText) detailItemDescriptionText.text = data1.description;
        if (detailItemAmountText) detailItemAmountText.text = data3.amount + GameUtility.Const.SHOW_AMOUNT + GameUtility.Const.SHOW_POSSESSION;
    }
}
