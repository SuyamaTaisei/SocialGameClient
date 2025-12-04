using TMPro;
using UnityEngine;

public class ShopTempView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI productNameText;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Set(ShopDataModel data)
    {
        if (productNameText) productNameText.text = data.name;
        if (priceText) priceText.text = data.price.ToString() + GameUtility.Const.SHOW_YEN;
    }
}