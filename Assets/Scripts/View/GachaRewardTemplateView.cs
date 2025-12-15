using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaRewardTemplateView : MonoBehaviour
{
    [SerializeField] Image rewardImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI amountText;

    public Image RewardImage => rewardImage;
    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI RarityText => rarityText;
    public TextMeshProUGUI AmountText => amountText;
}
