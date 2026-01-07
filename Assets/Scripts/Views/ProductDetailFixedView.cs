using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductDetailFixedView : MonoBehaviour
{
    //各ビューの表示
    [SerializeField] GameObject shopConfirmView;

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

    [SerializeField] ClientShop clientShop;
    [SerializeField] ShopCategoryTemplateView shopCategoryTemplateView;

    private int amountValue;
    private int priceCoin;
    private int priceGem;

    private void Start()
    {
        shopConfirmView.SetActive(false);
        paymentConfirmView.SetActive(false);
        paymentCompleteView.SetActive(false);
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
        amountValue = Mathf.Clamp(value, GameUtility.Const.SHOP_AMOUNT_MIN, GameUtility.Const.SHOP_AMOUNT_MAX);
        amountText.text = amountValue.ToString();
        increaseButton.interactable = amountValue < GameUtility.Const.SHOP_AMOUNT_MAX;
        decreaseButton.interactable = amountValue > GameUtility.Const.SHOP_AMOUNT_MIN;
        UpdatePriceText();
    }

    //商品確認画面開く
    public void OpenProductInfoButton(int index1, int index2, int imageIndex, ItemRaritiesModel itemRarity)
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
        productDescriptionText.text = showText
            ? data3.description
            : GameUtility.Const.SHOW_PAID_GEM + data4.paid_currency + GameUtility.Const.SHOW_AMOUNT + "　" +
              GameUtility.Const.SHOW_FREE_GEM + data4.free_currency + GameUtility.Const.SHOW_AMOUNT + " " +
              GameUtility.Const.SHOW_GET;
        productImage.sprite = Resources.Load<Sprite>($"{GameUtility.Const.FOLDER_NAME_IMAGES}/{shopCategoryTemplateView.ImageFolderName}/{imageIndex}");
        priceMoneyText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        priceCoin = data1.price;
        priceGem = data2.price;

        //購入数増減処理
        increaseButton.onClick.AddListener(() => SetAmount(amountValue + GameUtility.Const.SHOP_AMOUNT_MIN));
        decreaseButton.onClick.AddListener(() => SetAmount(amountValue - GameUtility.Const.SHOP_AMOUNT_MIN));
        buyMoneyButton.onClick.AddListener(() => OpenConfirmButton(index1, GameUtility.Const.SHOP_AMOUNT_MIN, GameUtility.Const.SHOW_YEN));
        buyItemCoinButton.onClick.AddListener(() => OpenConfirmButton(index1, amountValue, GameUtility.Const.SHOW_COIN));
        buyItemGemButton.onClick.AddListener(() => OpenConfirmButton(index2, amountValue, GameUtility.Const.SHOW_GEM));

        shopConfirmView.SetActive(true);
        SetAmount(GameUtility.Const.SHOP_AMOUNT_MIN); //再度開いたら常に1に設定
        clientShop.WarningMessage("");
    }

    //商品確認画面閉じる
    public void CloseProductInfoButton()
    {
        shopConfirmView.SetActive(false);
        clientShop.WarningMessage("");
    }

    //購入確認画面開く
    public void OpenConfirmButton(int productId, int amount, string currency)
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        ShopDataModel data = ShopDataTable.SelectProductId(productId);
        WalletsModel walletsModel = WalletsTable.Select();

        //購入情報表記
        paymentConfirmText.text =
            GameUtility.Const.SHOW_PRODUCT_NAME + "\n" +
            data.name + "\n\n" +
            amount + GameUtility.Const.SHOW_AMOUNT + " " + data.price * amount + currency + " " + GameUtility.Const.SHOW_BUY;

        //現在ウォレット残高表記
        walletStateText.text =
            GameUtility.Const.SHOW_GEM_WALLET +
            GameUtility.Const.SHOW_COIN_WALLET + walletsModel.coin_amount + GameUtility.Const.SHOW_COIN + "     " +
            GameUtility.Const.SHOW_PAID_GEM + walletsModel.gem_paid_amount + GameUtility.Const.SHOW_GEM + "     " +
            GameUtility.Const.SHOW_FREE_GEM + walletsModel.gem_free_amount + GameUtility.Const.SHOW_GEM;

        yesButton.onClick.AddListener(() => clientShop.PaymentButton(productId, amount));
        noButton.onClick.AddListener(() => CloseConfirmButton());
        paymentConfirmView.SetActive(true);
    }

    //購入確認画面閉じる
    public void CloseConfirmButton()
    {
        paymentConfirmView.SetActive(false);
        clientShop.WarningMessage("");
    }

    //購入完了画面
    public void PaymentComplete(bool enabled)
    {
        paymentCompleteView.SetActive(enabled);
    }
}
