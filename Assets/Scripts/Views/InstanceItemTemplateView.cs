using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceItemTemplateView : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemRarityText;
    [SerializeField] TextMeshProUGUI itemAmountText;

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (itemImage) itemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (itemNameText) itemNameText.text = data1.name;
        if (itemRarityText) itemRarityText.text = data2.name;
        if (itemAmountText) itemAmountText.text = data3.amount + "/" + GameUtility.Const.SHOW_INSTANCE_AMOUNT_MAX;
    }
}
