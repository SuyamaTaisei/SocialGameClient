using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ClientInstance : MonoBehaviour
{
    [SerializeField] GameObject itemInstanceView;
    [SerializeField] GameObject characterInstanceView;
    [SerializeField] GameObject characterInstanceDetailView;
    [SerializeField] TextMeshProUGUI itemNothingText;
    [SerializeField] TextMeshProUGUI enhanceItemNothingText;
    [SerializeField] TextMeshProUGUI CharacterNothingText;

    [SerializeField] EnhanceItemList enhanceItemList;
    [SerializeField] InstanceCharacterList instanceCharacterList;
    [SerializeField] InstanceCharacterDetailFixedView instanceCharacterDetailView;

    private ApiConnect apiConnect;

    //キャラクターID取得
    private int selectEnhanceCharacterId;

    //強化アイテム一覧で選択したアイテムIDとアイテム数量の紐づけ
    private readonly Dictionary<int, int> selectEnhanceItems = new();

    private const string column_id = "id";
    private const string column_character_id = "character_id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        itemInstanceView.SetActive(false);
        characterInstanceView.SetActive(false);
        characterInstanceDetailView.SetActive(false);
    }

    //キャラ強化画面を開いた時の選択キャラのキャラクターIDを取得
    public void SetEnhanceCharacterId(int characterId)
    {
        selectEnhanceCharacterId = characterId;
    }

    //強化アイテム一覧で選択されたアイテムIDとアイテム数量を保持
    public void SetEnhanceItems(int itemId, int amount)
    {
        selectEnhanceItems[itemId] = amount;
    }

    //保持された紐づけをリセット
    public void ClearSelectEnhanceItems()
    {
        selectEnhanceItems.Clear();
    }

    //強化リクエストの送信処理
    public void ExecuteEnhance()
    {
        var usersModel = UsersTable.Select();

        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_character_id, selectEnhanceCharacterId.ToString()),
        };

        //保持したアイテムIDと数量でペアを生成
        var items = new List<KeyValuePair<int, int>>(selectEnhanceItems);

        //強化アイテムのIDと数量をペアで送信して、URLの末尾に追加
        for (int i = 0; i < items.Count; i++)
        {
            form.Add(new MultipartFormDataSection($"items[{i}][item_id]", items[i].Key.ToString()));
            form.Add(new MultipartFormDataSection($"items[{i}][amount]", items[i].Value.ToString()));
        }

        //リクエスト送信後の成功時レスポンス受け取りコールバック
        StartCoroutine(apiConnect.Send(GameUtility.Const.CHARACTER_ENHANCE_URL, form, (action) =>
        {
            CharacterInstancesModel characterInstancesModel = CharacterInstancesTable.SelectId(selectEnhanceCharacterId);
            instanceCharacterDetailView.SetLatestLevel(characterInstancesModel.level); //最新レベルを反映
            enhanceItemList.Refresh();                                //アイテム更新
            instanceCharacterList.Refresh();                          //キャラクター更新
            instanceCharacterDetailView.SetEnhanceConfirmView(false); //確認画面閉じる
            instanceCharacterDetailView.SetEnhanceCompleteView(true); //強化完了画面開く
            ClearSelectEnhanceItems();
        }));
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

    public void NothingEnhanceItemMessage(string text)
    {
        enhanceItemNothingText.text = text;
    }

    public void NothingCharacterMessage(string text)
    {
        CharacterNothingText.text = text;
    }
}
