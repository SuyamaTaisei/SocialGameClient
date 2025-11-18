using TMPro;
using UnityEngine;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI productNameText;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Set(ShopDataModel data)
    {
        if (productNameText) productNameText.text = data.name;
        if (priceText) priceText.text = data.price.ToString() + "円";
    }
}