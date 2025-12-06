using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GachaResultTempView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI newText;

    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemRarityText;
    [SerializeField] TextMeshProUGUI itemAmountText;
    [SerializeField] GameObject itemOtherObject;

    public Image CharacterImage => characterImage;
    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI RarityText => rarityText;
    public TextMeshProUGUI NewText => newText;

    public Image ItemImage => itemImage;
    public TextMeshProUGUI ItemNameText => itemNameText;
    public TextMeshProUGUI ItemRarityText => itemRarityText;
    public TextMeshProUGUI ItemAmountText => itemAmountText;
    public GameObject ItemOtherObject => itemOtherObject;
}
