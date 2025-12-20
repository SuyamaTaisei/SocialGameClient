using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplateView : MonoBehaviour
{
    [SerializeField] Image productImage;
    [SerializeField] TextMeshProUGUI productNameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI productRarityText;

    public void Set(ShopDataModel data1, ItemRaritiesModel data2, string imagePath)
    {
        if (productImage) productImage.sprite = Resources.Load<Sprite>(imagePath);
        if (productNameText) productNameText.text = data1.name;
        if (priceText) priceText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        if (productRarityText) productRarityText.text = data2.name;
    }
}