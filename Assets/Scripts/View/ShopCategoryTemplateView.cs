using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryTemplateView : MonoBehaviour
{
    [SerializeField] Image categoryIcon;
    [SerializeField] TextMeshProUGUI categoryText;

    public Image CategoryIcon => categoryIcon;
    public TextMeshProUGUI CategoryText => categoryText;
}
