using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaRewardTempView : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI amountText;

    public Image ItemImage => itemImage;
    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI RarityText => rarityText;
    public TextMeshProUGUI AmountText => amountText;
}
