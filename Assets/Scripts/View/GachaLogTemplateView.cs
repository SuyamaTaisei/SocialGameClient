using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaLogTemplateView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI dateTimeText;
    [SerializeField] TextMeshProUGUI gachaPeriodText;

    public Image CharacterImage => characterImage;
    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI RarityText => rarityText;
    public TextMeshProUGUI DateTimeText => dateTimeText;
    public TextMeshProUGUI GachaPeriodText => gachaPeriodText;
}
