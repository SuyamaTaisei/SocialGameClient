using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GachaResultTemplateView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI characterRarityText;
    [SerializeField] TextMeshProUGUI characterNewText;

    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemRarityText;
    [SerializeField] TextMeshProUGUI itemAmountText;
    [SerializeField] GameObject itemOtherObject;

    public Image CharacterImage => characterImage;
    public TextMeshProUGUI CharacterNameText => characterNameText;
    public TextMeshProUGUI CharacterRarityText => characterRarityText;
    public TextMeshProUGUI CharacterNewText => characterNewText;

    public Image ItemImage => itemImage;
    public TextMeshProUGUI ItemNameText => itemNameText;
    public TextMeshProUGUI ItemRarityText => itemRarityText;
    public TextMeshProUGUI ItemAmountText => itemAmountText;
    public GameObject ItemOtherObject => itemOtherObject;
}
