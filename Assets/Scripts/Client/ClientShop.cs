using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientShop : MonoBehaviour
{
    //各ビューの表示
    [SerializeField] GameObject shopView;
    [SerializeField] GameObject itemListView;
    [SerializeField] GameObject gemListView;
    [SerializeField] GameObject shopConfirmCover;

    //現在所持ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //商品名表示
    [SerializeField] TextMeshProUGUI productName;

    //価格表示
    [SerializeField] TextMeshProUGUI priceMoneyText;
    [SerializeField] TextMeshProUGUI priceCoinText;
    [SerializeField] TextMeshProUGUI priceGemText;

    //購入ボタン
    [SerializeField] Button buyMoneyButton;
    [SerializeField] Button buyItemButtonCoin;
    [SerializeField] Button buyItemButtonGem;

    //購入警告
    [SerializeField] TextMeshProUGUI warningText;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_product_id = "product_id";

    //DBモデル
    private UsersModel usersModel;
    private WalletsModel walletsModel;

    private void Start()
    {
        usersModel = UsersTable.Select();
        apiConnect = FindAnyObjectByType<ApiConnect>();
        shopView.SetActive(false);
        shopConfirmCover.SetActive(false);
        warningText.text = "";
        OpenGemListButton();
    }

    //表記のリアルタイム更新
    private void Update()
    {
        if (!string.IsNullOrEmpty(usersModel.id))
        {
            walletsModel = WalletsTable.Select();
            coinText.text = walletsModel.coin_amount.ToString();
            gemFreeText.text = walletsModel.gem_free_amount.ToString();
            gemPaidText.text = walletsModel.gem_paid_amount.ToString();
        }
    }

    //購入処理
    public void PaymentButton(int index)
    {
        usersModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_product_id, index.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.PAYMENT_URL, form));
    }

    //購入確認画面
    public void OpenConfirmButton(int index1, int index2)
    {
        //必ず購入状態をリセット
        buyMoneyButton.onClick.RemoveAllListeners();
        buyItemButtonCoin.onClick.RemoveAllListeners();
        buyItemButtonGem.onClick.RemoveAllListeners();

        //product_idが一致するレコードを取得
        ShopDataModel data1 = ShopDataTable.SelectProductId(index1);
        ShopDataModel data2 = ShopDataTable.SelectProductId(index2);

        //表記
        productName.text = data1.name;
        priceMoneyText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        priceCoinText.text = data1.price.ToString();
        priceGemText.text = data2.price.ToString();

        //購入ボタン処理
        buyMoneyButton.onClick.AddListener(() => PaymentButton(index1));
        buyItemButtonCoin.onClick.AddListener(() => PaymentButton(index1));
        buyItemButtonGem.onClick.AddListener(() => PaymentButton(index2));

        shopConfirmCover.SetActive(true);
    }

    //購入確認画面閉じる
    public void CloseConfirmButton()
    {
        shopConfirmCover.SetActive(false);
        warningText.text = "";
    }

    //販売アイテム一覧表示
    public void OpenItemListButton()
    {
        itemListView.SetActive(true);
        buyItemButtonCoin.gameObject.SetActive(true);
        buyItemButtonGem.gameObject.SetActive(true);
        buyMoneyButton.gameObject.SetActive(false);
        gemListView.SetActive(false);
    }

    //販売ジェム一覧表示
    public void OpenGemListButton()
    {
        gemListView.SetActive(true);
        buyMoneyButton.gameObject.SetActive(true);
        buyItemButtonCoin.gameObject.SetActive(false);
        buyItemButtonGem.gameObject.SetActive(false);
        itemListView.SetActive(false);
    }

    //ショップ開く
    public void OpenShopButton()
    {
        shopView.SetActive(true);
    }

    //ショップ閉じる
    public void CloseShopButton()
    {
        shopView.SetActive(false);
    }

    //購入警告
    public void WarningMessage(string message)
    {
        warningText.text = message;
    }
}