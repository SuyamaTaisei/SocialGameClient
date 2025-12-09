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
    [SerializeField] GameObject shopConfirmView;

    //現在所持ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //商品情報表示
    [SerializeField] TextMeshProUGUI productName;
    [SerializeField] TextMeshProUGUI productDescription;
    [SerializeField] Image productImage;

    //価格表示
    [SerializeField] TextMeshProUGUI priceMoneyText;
    [SerializeField] TextMeshProUGUI priceCoinText;
    [SerializeField] TextMeshProUGUI priceGemText;

    //購入ボタン
    [SerializeField] Button buyMoneyButton;
    [SerializeField] Button buyItemCoinButton;
    [SerializeField] Button buyItemGemButton;

    //購入警告
    [SerializeField] TextMeshProUGUI warningText;

    private string imageFolderName;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_product_id = "product_id";

    //DBモデル
    private UsersModel usersModel;
    private WalletsModel walletsModel;

    private void Awake()
    {
        ShowShopCategoryList(0);
    }

    private void Start()
    {
        usersModel = UsersTable.Select();
        apiConnect = FindAnyObjectByType<ApiConnect>();
        shopView.SetActive(false);
        shopConfirmView.SetActive(false);
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

    //ショップカテゴリ内の商品リスト
    public void ShowShopCategoryList(int index)
    {
        //全カテゴリ期間のレコード取得
        List<ShopCategoriesModel> shopCategoriesList = ShopCategoriesTable.SelectAll();
        var data = shopCategoriesList[index];

        //カテゴリIDが一致するレコードを取得
        var category = data.category;

        switch(category)
        {
            case 1001: OpenGemListButton();
                break;
            case 1002: OpenItemListButton();
                break;
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
    public void OpenConfirmButton(int index1, int index2, int imageIndex)
    {
        //必ず購入状態をリセット
        buyMoneyButton.onClick.RemoveAllListeners();
        buyItemCoinButton.onClick.RemoveAllListeners();
        buyItemGemButton.onClick.RemoveAllListeners();

        //product_idが一致するレコードを取得
        ShopDataModel data1 = ShopDataTable.SelectProductId(index1);
        ShopDataModel data2 = ShopDataTable.SelectProductId(index2);
        ItemDataModel data3 = ItemDataTable.SelectId(imageIndex);
        ShopDataModel data4 = ShopDataTable.SelectProductId(imageIndex);

        //表記
        productName.text = data1.name;
        bool showText = (data3 != null);
        productDescription.text = showText ? data3.description : $"購入後のウォレット\n\n有償ジェム{walletsModel.gem_paid_amount + data4.paid_currency}個\n無償ジェム{walletsModel.gem_free_amount + data4.free_currency}個";
        productImage.sprite = Resources.Load<Sprite>($"Images/{imageFolderName}/{imageIndex}");
        priceMoneyText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        priceCoinText.text = data1.price.ToString();
        priceGemText.text = data2.price.ToString();

        //購入ボタン処理
        buyMoneyButton.onClick.AddListener(() => PaymentButton(index1));
        buyItemCoinButton.onClick.AddListener(() => PaymentButton(index1));
        buyItemGemButton.onClick.AddListener(() => PaymentButton(index2));

        shopConfirmView.SetActive(true);
    }

    //購入確認画面閉じる
    public void CloseConfirmButton()
    {
        shopConfirmView.SetActive(false);
        warningText.text = "";
    }

    //販売アイテム一覧表示
    public void OpenItemListButton()
    {
        imageFolderName = "Items";
        itemListView.SetActive(true);
        buyItemCoinButton.gameObject.SetActive(true);
        buyItemGemButton.gameObject.SetActive(true);
        buyMoneyButton.gameObject.SetActive(false);
        gemListView.SetActive(false);
    }

    //販売ジェム一覧表示
    public void OpenGemListButton()
    {
        imageFolderName = "Shops";
        gemListView.SetActive(true);
        buyMoneyButton.gameObject.SetActive(true);
        buyItemCoinButton.gameObject.SetActive(false);
        buyItemGemButton.gameObject.SetActive(false);
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