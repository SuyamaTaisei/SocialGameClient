using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GachaResultTempView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI newText;

    public Image CharacterImage => characterImage;
    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI RarityText => rarityText;
    public TextMeshProUGUI NewText => newText;
}
