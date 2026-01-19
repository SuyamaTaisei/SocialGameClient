using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClientInstance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemInstanceNothingText;
    [SerializeField] TextMeshProUGUI enhanceItemNothingText;
    [SerializeField] TextMeshProUGUI charaInstanceNothingText;

    [SerializeField] Button itemInstanceOpenButton;
    [SerializeField] Button itemInstanceCloseButton;
    [SerializeField] Button charaInstanceOpenButton;
    [SerializeField] Button charaInstanceCloseButton;

    [SerializeField] GameObject itemInstanceView;
    [SerializeField] GameObject charaInstanceView;

    [SerializeField] EnhanceItemList enhanceItemList;
    [SerializeField] InstanceCharacterList charaInstanceList;
    [SerializeField] InstanceCharacterDetailFixedView charaDetailFixedView;
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
        charaInstanceView.SetActive(false);

        itemInstanceOpenButton.onClick.AddListener(() => ItemInstance(true));
        itemInstanceCloseButton.onClick.AddListener(() => ItemInstance(false));
        charaInstanceOpenButton.onClick.AddListener(() => CharaInstance(true));
        charaInstanceCloseButton.onClick.AddListener(() => CharaInstance(false));
    }

    //キャラ強化画面を開いた時の選択キャラのキャラクターIDを取得
    public void GetCharacterId(int characterId)
    {
        selectEnhanceCharacterId = characterId;
    }

    //強化アイテム一覧で選択されたアイテムIDとアイテム数量を保持
    public void SaveEnhanceItems(int itemId, int amount)
    {
        selectEnhanceItems[itemId] = amount;
    }

    //保持された紐づけをリセット
    public void ClearEnhanceItems()
    {
        selectEnhanceItems.Clear();
    }

    //強化リクエストの送信処理
    public void RequestEnhance()
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
            CharacterInstancesModel characterInstancesModel = CharacterInstancesTable.SelectCharacterId(selectEnhanceCharacterId);
            charaDetailFixedView.SetLatestLevel(characterInstancesModel.level); //最新レベルを反映
            enhanceItemList.Refresh();                                          //アイテム更新
            charaInstanceList.Refresh();                                        //キャラクター更新
            charaDetailFixedView.SetEnhanceConnectingCover(false);              //画面カバー非表示
            charaDetailFixedView.SetEnhanceCompleteView(true);                  //強化完了画面開く
            ClearEnhanceItems();
        }));
    }

    //アイテム一覧開閉
    public void ItemInstance(bool enabled)
    {
        itemInstanceView.SetActive(enabled);
    }

    //キャラ一覧開閉
    public void CharaInstance(bool enabled)
    {
        charaInstanceView.SetActive(enabled);
    }

    //メッセージ
    public void ItemInstanceMessage(string text)
    {
        itemInstanceNothingText.text = text;
    }

    public void EnhanceItemMessage(string text)
    {
        enhanceItemNothingText.text = text;
    }

    public void CharaInstanceMessage(string text)
    {
        charaInstanceNothingText.text = text;
    }
}
