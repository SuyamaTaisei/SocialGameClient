using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryTemplateView : MonoBehaviour
{
    [SerializeField] Image shopCategoryIcon;
    [SerializeField] TextMeshProUGUI shopCategoryText;

    public Image ShopCategoryIcon => shopCategoryIcon;
    public TextMeshProUGUI ShopCategoryText => shopCategoryText;
}
