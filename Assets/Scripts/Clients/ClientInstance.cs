using TMPro;
using UnityEngine;

public class ClientInstance : MonoBehaviour
{
    [SerializeField] GameObject itemInstanceView;
    [SerializeField] GameObject characterInstanceView;
    [SerializeField] GameObject characterInstanceDetailView;
    [SerializeField] TextMeshProUGUI itemNothingText;
    [SerializeField] TextMeshProUGUI CharacterNothingText;

    private void Start()
    {
        itemInstanceView.SetActive(false);
        characterInstanceView.SetActive(false);
        characterInstanceDetailView.SetActive(false);
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

    //キャラクター詳細画面開閉ボタン
    public void CharacterDetailButton(bool enabled)
    {
        characterInstanceDetailView.SetActive(enabled);
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
