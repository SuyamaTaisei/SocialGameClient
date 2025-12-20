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
    [SerializeField] TextMeshProUGUI productNameText;
    [SerializeField] TextMeshProUGUI productRarityText;
    [SerializeField] TextMeshProUGUI productDescriptionText;
    [SerializeField] Image productImage;

    //価格表示
    [SerializeField] TextMeshProUGUI priceMoneyText;
    [SerializeField] TextMeshProUGUI priceCoinText;
    [SerializeField] TextMeshProUGUI priceGemText;

    //購入数ボタン
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Button increaseButton;
    [SerializeField] Button decreaseButton;

    //購入ボタン
    [SerializeField] Button buyMoneyButton;
    [SerializeField] Button buyItemCoinButton;
    [SerializeField] Button buyItemGemButton;

    //購入確認画面
    [SerializeField] GameObject paymentConfirmView;
    [SerializeField] TextMeshProUGUI paymentConfirmText;
    [SerializeField] TextMeshProUGUI walletStateText;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    //購入完了画面
    [SerializeField] GameObject paymentCompleteView;

    //購入警告
    [SerializeField] TextMeshProUGUI warningText;

    [SerializeField] ClientHome clientHome;
    [SerializeField] ShopCategoryTemplateView shopCategoryTemplateView;

    private int amountValue;
    private const int amountMin = 1;
    private const int amountMax = 99;
    private int priceCoin;
    private int priceGem;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_product_id = "product_id";
    private const string key_amount = "amount";

    private void Start()
    {
        apiConnect = FindAnyObjectByType<ApiConnect>();
        shopView.SetActive(false);
        shopConfirmView.SetActive(false);
        paymentConfirmView.SetActive(false);
        paymentCompleteView.SetActive(false);
        WarningMessage("");
    }

    //表記のリアルタイム更新
    private void Update()
    {
        clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //購入処理
    public void PaymentButton(int index, int amount)
    {
        var usersModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_product_id, index.ToString()),
            new MultipartFormDataSection(key_amount, amount.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.PAYMENT_URL, form));
    }

    //価格更新
    private void UpdatePriceText()
    {
        priceCoinText.text = (priceCoin * amountValue).ToString();
        priceGemText.text = (priceGem * amountValue).ToString();
    }

    //購入数の増減制御
    private void SetAmount(int value)
    {
        amountValue = Mathf.Clamp(value, amountMin, amountMax);
        amountText.text = amountValue.ToString();
        increaseButton.interactable = amountValue < amountMax;
        decreaseButton.interactable = amountValue > amountMin;
        UpdatePriceText();
    }

    //商品確認画面開く
    public void OpenConfirmButton(int index1, int index2, int imageIndex, ItemRaritiesModel itemRarity)
    {
        //必ず購入状態をリセット
        increaseButton.onClick.RemoveAllListeners();
        decreaseButton.onClick.RemoveAllListeners();
        buyMoneyButton.onClick.RemoveAllListeners();
        buyItemCoinButton.onClick.RemoveAllListeners();
        buyItemGemButton.onClick.RemoveAllListeners();

        //product_idが一致するレコードを取得
        ShopDataModel data1 = ShopDataTable.SelectProductId(index1);
        ShopDataModel data2 = ShopDataTable.SelectProductId(index2);
        ItemDataModel data3 = ItemDataTable.SelectId(imageIndex);
        ShopDataModel data4 = ShopDataTable.SelectProductId(imageIndex);
        bool showText = (data3 != null);

        //表記
        productNameText.text = data1.name;
        productRarityText.text = showText ? itemRarity.name : "";
        productDescriptionText.text = showText ? data3.description : $"ジェムが有償{data4.paid_currency}個・無償{data4.free_currency}個もらえる";
        productImage.sprite = Resources.Load<Sprite>($"{GameUtility.Const.FOLDER_NAME_IMAGES}/{shopCategoryTemplateView.ImageFolderName}/{imageIndex}");
        priceMoneyText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        priceCoin = data1.price;
        priceGem = data2.price;

        //購入数増減処理
        increaseButton.onClick.AddListener(() => SetAmount(amountValue + amountMin));
        decreaseButton.onClick.AddListener(() => SetAmount(amountValue - amountMin));
        buyMoneyButton.onClick.AddListener(() => OpenConfirmPaymentButton(index1, amountMin, "円"));
        buyItemCoinButton.onClick.AddListener(() => OpenConfirmPaymentButton(index1, amountValue, "コイン"));
        buyItemGemButton.onClick.AddListener(() => OpenConfirmPaymentButton(index2, amountValue, "ジェム"));

        shopConfirmView.SetActive(true);
        SetAmount(amountMin); //再度開いたら常に1に設定
        WarningMessage("");
    }

    //商品確認画面閉じる
    public void CloseConfirmButton()
    {
        shopConfirmView.SetActive(false);
        WarningMessage("");
    }

    //購入確認画面開く
    public void OpenConfirmPaymentButton(int productId, int amount, string currency)
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        ShopDataModel data = ShopDataTable.SelectProductId(productId);
        WalletsModel walletsModel = WalletsTable.Select();
        paymentConfirmText.text = "商品名\n" + data.name + "\n\n" + amount + "個を" + data.price * amount + currency + "で購入しますか？";
        walletStateText.text =
        "現在のウォレット残高\n" +
        "コイン残高 " + walletsModel.coin_amount + "コイン" + "     " + "有償 " + walletsModel.gem_paid_amount + "ジェム" + "     " + "無償 " + walletsModel.gem_free_amount + "ジェム";

        yesButton.onClick.AddListener(() => PaymentButton(productId, amount));
        noButton.onClick.AddListener(() => CloseConfirmPaymentButton());
        paymentConfirmView.SetActive(true);
    }

    //購入確認画面閉じる
    public void CloseConfirmPaymentButton()
    {
        paymentConfirmView.SetActive(false);
        WarningMessage("");
    }

    //購入完了画面
    public void PaymentComplete(bool enabled)
    {
        paymentCompleteView.SetActive(enabled);
    }

    //ショップ開く
    public void OpenShopButton()
    {
        PaymentComplete(false);
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