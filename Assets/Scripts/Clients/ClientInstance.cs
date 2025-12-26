using TMPro;
using UnityEngine;

public class ClientInstance : MonoBehaviour
{
    [SerializeField] GameObject itemInstanceView;
    [SerializeField] GameObject characterInstanceView;
    [SerializeField] TextMeshProUGUI itemNothingText;
    [SerializeField] TextMeshProUGUI CharacterNothingText;

    private void Start()
    {
        itemInstanceView.SetActive(false);
        characterInstanceView.SetActive(false);
    }

    //アイテム一覧開くボタン
    public void ItemInstanceButton(bool enabled)
    {
        itemInstanceView.SetActive(enabled);
    }

    //キャラクター一覧開くボタン
    public void CharacterInstanceButton(bool enabled)
    {
        characterInstanceView.SetActive(enabled);
    }

    public void NothingItemMessage(string text)
    {
        itemNothingText.text = text;
    }

    public void NothingCharacterMessage(string text)
    {
        CharacterNothingText.text = text;
    }
}
