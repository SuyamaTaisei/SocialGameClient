using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDetailFixedView : MonoBehaviour
{
    //各ビューの表示
    [SerializeField] GameObject shopDetailView;

    //商品情報表示
    [SerializeField] TextMeshProUGUI shopDetailNameText;
    [SerializeField] TextMeshProUGUI shopDetailRarityText;
    [SerializeField] TextMeshProUGUI shopDetailDescriptionText;
    [SerializeField] Image shopDetailImage;
    [SerializeField] Button shopDetailCloseButton;

    //価格表示
    [SerializeField] TextMeshProUGUI shopDetailMoneyText;
    [SerializeField] TextMeshProUGUI shopDetailCoinText;
    [SerializeField] TextMeshProUGUI shopDetailGemText;

    //購入数ボタン
    [SerializeField] TextMeshProUGUI shopDetailAmountText;
    [SerializeField] Button shopDetailIncreaseButton;
    [SerializeField] Button shopDetailDecreaseButton;

    //購入ボタン
    [SerializeField] Button shopDetailMoneyButton;
    [SerializeField] Button shopDetailCoinButton;
    [SerializeField] Button shopDetailGemButton;

    //購入確認画面
    [SerializeField] GameObject buyConfirmView;
    [SerializeField] TextMeshProUGUI buyConfirmText;
    [SerializeField] TextMeshProUGUI walletStateText;
    [SerializeField] Button buyConfirmExecuteButton;
    [SerializeField] Button buyConfirmCancelButton;

    //購入完了画面
    [SerializeField] GameObject paymentCompleteView;
    [SerializeField] Button paymentCloseButton;

    [SerializeField] ClientShop clientShop;
    [SerializeField] ShopCategoryTemplateView shopCategoryTemplateView;

    private int amountValue;
    private int priceCoin;
    private int priceGem;

    private void Start()
    {
        shopDetailView.SetActive(false);
        buyConfirmView.SetActive(false);
        paymentCompleteView.SetActive(false);

        shopDetailCloseButton.onClick.AddListener(() => SetShopDetailClose());
        paymentCloseButton.onClick.AddListener(() => SetPaymentComplete(false));
    }

    //価格更新
    private void SetUpdatePrice()
    {
        shopDetailCoinText.text = (priceCoin * amountValue).ToString();
        shopDetailGemText.text = (priceGem * amountValue).ToString();
    }

    //購入数の増減制御
    private void SetAmount(int value)
    {
        amountValue = Mathf.Clamp(value, GameUtility.Const.SHOP_AMOUNT_MIN, GameUtility.Const.SHOP_AMOUNT_MAX);
        shopDetailAmountText.text = amountValue.ToString();
        shopDetailIncreaseButton.interactable = amountValue < GameUtility.Const.SHOP_AMOUNT_MAX;
        shopDetailDecreaseButton.interactable = amountValue > GameUtility.Const.SHOP_AMOUNT_MIN;
        SetUpdatePrice();
    }

    //商品確認画面開く
    public void SetShopDetailOpen(int index1, int index2, int imageIndex, ItemRaritiesModel itemRarity)
    {
        //必ず購入状態をリセット
        shopDetailIncreaseButton.onClick.RemoveAllListeners();
        shopDetailDecreaseButton.onClick.RemoveAllListeners();
        shopDetailMoneyButton.onClick.RemoveAllListeners();
        shopDetailCoinButton.onClick.RemoveAllListeners();
        shopDetailGemButton.onClick.RemoveAllListeners();

        //product_idが一致するレコードを取得
        ShopDataModel data1 = ShopDataTable.SelectProductId(index1);
        ShopDataModel data2 = ShopDataTable.SelectProductId(index2);
        ItemDataModel data3 = ItemDataTable.SelectId(imageIndex);
        ShopDataModel data4 = ShopDataTable.SelectProductId(imageIndex);
        bool showText = (data3 != null);

        //表記
        shopDetailNameText.text = data1.name;
        shopDetailRarityText.text = showText ? itemRarity.name : "";
        shopDetailDescriptionText.text = showText
            ? data3.description
            : GameUtility.Const.SHOW_PAID_GEM + data4.paid_currency + GameUtility.Const.SHOW_AMOUNT + "　" +
              GameUtility.Const.SHOW_FREE_GEM + data4.free_currency + GameUtility.Const.SHOW_AMOUNT + " " +
              GameUtility.Const.SHOW_GET;
        shopDetailImage.sprite = Resources.Load<Sprite>($"{GameUtility.Const.FOLDER_NAME_IMAGES}/{shopCategoryTemplateView.ImageFolderName}/{imageIndex}");
        shopDetailMoneyText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        priceCoin = data1.price;
        priceGem = data2.price;

        //購入数増減処理
        shopDetailIncreaseButton.onClick.AddListener(() => SetAmount(amountValue + GameUtility.Const.SHOP_AMOUNT_MIN));
        shopDetailDecreaseButton.onClick.AddListener(() => SetAmount(amountValue - GameUtility.Const.SHOP_AMOUNT_MIN));
        shopDetailMoneyButton.onClick.AddListener(() => SetBuyConfirmOpen(index1, GameUtility.Const.SHOP_AMOUNT_MIN, GameUtility.Const.SHOW_YEN));
        shopDetailCoinButton.onClick.AddListener(() => SetBuyConfirmOpen(index1, amountValue, GameUtility.Const.SHOW_COIN));
        shopDetailGemButton.onClick.AddListener(() => SetBuyConfirmOpen(index2, amountValue, GameUtility.Const.SHOW_GEM));

        shopDetailView.SetActive(true);
        SetAmount(GameUtility.Const.SHOP_AMOUNT_MIN); //再度開いたら常に1に設定
        clientShop.WarningMessage("");
    }

    //商品確認画面閉じる
    public void SetShopDetailClose()
    {
        shopDetailView.SetActive(false);
        clientShop.WarningMessage("");
    }

    //購入確認画面開く
    private void SetBuyConfirmOpen(int productId, int amount, string currency)
    {
        buyConfirmExecuteButton.onClick.RemoveAllListeners();
        buyConfirmCancelButton.onClick.RemoveAllListeners();

        ShopDataModel data = ShopDataTable.SelectProductId(productId);
        WalletsModel walletsModel = WalletsTable.Select();

        //購入情報表記
        buyConfirmText.text =
            GameUtility.Const.SHOW_PRODUCT_NAME + "\n" +
            data.name + "\n\n" +
            amount + GameUtility.Const.SHOW_AMOUNT + " " + data.price * amount + currency + " " + GameUtility.Const.SHOW_BUY;

        //現在ウォレット残高表記
        walletStateText.text =
            GameUtility.Const.SHOW_GEM_WALLET +
            GameUtility.Const.SHOW_COIN_WALLET + walletsModel.coin_amount + GameUtility.Const.SHOW_COIN + "     " +
            GameUtility.Const.SHOW_PAID_GEM + walletsModel.gem_paid_amount + GameUtility.Const.SHOW_GEM + "     " +
            GameUtility.Const.SHOW_FREE_GEM + walletsModel.gem_free_amount + GameUtility.Const.SHOW_GEM;

        buyConfirmExecuteButton.onClick.AddListener(() => clientShop.RequestPayment(productId, amount));
        buyConfirmCancelButton.onClick.AddListener(() => SetBuyConfirmClose());
        buyConfirmView.SetActive(true);
    }

    //購入確認画面閉じる
    public void SetBuyConfirmClose()
    {
        buyConfirmView.SetActive(false);
        clientShop.WarningMessage("");
    }

    //購入完了画面
    public void SetPaymentComplete(bool enabled)
    {
        paymentCompleteView.SetActive(enabled);
    }
}
