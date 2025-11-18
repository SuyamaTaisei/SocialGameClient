using TMPro;
using UnityEngine;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI productNameText;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Set(ShopDataModel data)
    {
        productNameText.text = data.name;
        priceText.text = data.price.ToString() + "円";
    }
}