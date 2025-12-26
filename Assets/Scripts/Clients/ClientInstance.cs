using TMPro;
using UnityEngine;

public class ClientInstance : MonoBehaviour
{
    [SerializeField] GameObject characterInstanceView;
    [SerializeField] TextMeshProUGUI messageText;

    private void Start()
    {
        characterInstanceView.SetActive(false);
    }

    //キャラクター一覧開くボタン
    public void CharacterInstanceButton(bool enabled)
    {
        characterInstanceView.SetActive(enabled);
    }

    public void NothingMessage(string text)
    {
        messageText.text = text;
    }
}
