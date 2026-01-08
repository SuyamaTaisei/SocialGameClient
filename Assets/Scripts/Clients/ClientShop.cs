using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientShop : MonoBehaviour
{
    //各ビューの表示
    [SerializeField] GameObject shopView;

    //現在所持ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //購入警告
    [SerializeField] TextMeshProUGUI buyConfirmWarningText;

    [SerializeField] ClientHome clientHome;
    [SerializeField] ProductDetailFixedView shopDetailFixedView;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_product_id = "product_id";
    private const string key_amount = "amount";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;
        shopView.SetActive(false);
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
        StartCoroutine(apiConnect.Send(GameUtility.Const.PAYMENT_URL, form, (action) =>
        {
            clientHome.StaminaApply();
            clientHome.StaminaButtonCtrl();
        }));
    }

    //ショップ開く
    public void OpenShopButton()
    {
        shopDetailFixedView.PaymentComplete(false);
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
        buyConfirmWarningText.text = message;
    }
}