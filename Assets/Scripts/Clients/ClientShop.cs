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
    [SerializeField] TextMeshProUGUI productDescriptionText;
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

    [SerializeField] ClientHome clientHome;
    [SerializeField] ShopCategoryTemplateView shopCategoryTemplateView;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_product_id = "product_id";

    private void Start()
    {
        apiConnect = FindAnyObjectByType<ApiConnect>();
        shopView.SetActive(false);
        shopConfirmView.SetActive(false);
        WarningMessage("");
    }

    //表記のリアルタイム更新
    private void Update()
    {
        clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //購入処理
    public void PaymentButton(int index)
    {
        var usersModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_product_id, index.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.PAYMENT_URL, form));
    }

    //購入確認画面開く
    public void OpenConfirmButton(int index1, int index2, int itemId)
    {
        //必ず購入状態をリセット
        buyMoneyButton.onClick.RemoveAllListeners();
        buyItemCoinButton.onClick.RemoveAllListeners();
        buyItemGemButton.onClick.RemoveAllListeners();

        //product_idが一致するレコードを取得
        ShopDataModel data1 = ShopDataTable.SelectProductId(index1);
        ShopDataModel data2 = ShopDataTable.SelectProductId(index2);
        ItemDataModel data3 = ItemDataTable.SelectId(itemId);
        ShopDataModel data4 = ShopDataTable.SelectProductId(itemId);
        WalletsModel walletsModel = WalletsTable.Select();

        //表記
        productNameText.text = data1.name;
        bool showText = (data3 != null);
        productDescriptionText.text = showText ? data3.description : $"{GameUtility.Const.SHOW_AFTER_WALLET}{GameUtility.Const.SHOW_PAID_GEM}{walletsModel.gem_paid_amount + data4.paid_currency}{GameUtility.Const.SHOW_FREE_GEM}{walletsModel.gem_free_amount + data4.free_currency}";
        productImage.sprite = Resources.Load<Sprite>($"{GameUtility.Const.FOLDER_NAME_IMAGES}/{shopCategoryTemplateView.ImageFolderName}/{itemId}");
        priceMoneyText.text = data1.price.ToString() + GameUtility.Const.SHOW_YEN;
        priceCoinText.text = data1.price.ToString();
        priceGemText.text = data2.price.ToString();

        //購入ボタン処理
        buyMoneyButton.onClick.AddListener(() => PaymentButton(index1));
        buyItemCoinButton.onClick.AddListener(() => PaymentButton(index1));
        buyItemGemButton.onClick.AddListener(() => PaymentButton(index2));

        shopConfirmView.SetActive(true);
        WarningMessage("");
    }

    //購入確認画面閉じる
    public void CloseConfirmButton()
    {
        shopConfirmView.SetActive(false);
        WarningMessage("");
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