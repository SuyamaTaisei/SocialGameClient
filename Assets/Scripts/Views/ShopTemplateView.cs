using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplateView : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI rarityText;

    public void Set(ShopDataModel data1, ItemRaritiesModel data2, string imagePath)
    {
        if (image) image.sprite = Resources.Load<Sprite>(imagePath);
        if (nameText) nameText.text = data1.name;
        if (priceText) priceText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        if (rarityText) rarityText.text = data2.name;
    }
}