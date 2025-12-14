using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplateView : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI productNameText;
    [SerializeField] TextMeshProUGUI priceText;

    public void Set(ShopDataModel data, string imagePath)
    {
        if (itemImage) itemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (productNameText) productNameText.text = data.name;
        if (priceText) priceText.text = data.price.ToString() + GameUtility.Const.SHOW_YEN;
    }
}