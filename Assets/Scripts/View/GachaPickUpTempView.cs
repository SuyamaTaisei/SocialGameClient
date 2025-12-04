using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaPickUpTempView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;

    public Image CharacterImage => characterImage;
    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI RarityText => rarityText;
}
