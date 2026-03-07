using SoundSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientShop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;
    [SerializeField] TextMeshProUGUI buyConfirmWarningText;

    [SerializeField] Button shopOpenButton;
    [SerializeField] Button shopCloseButton;

    [SerializeField] GameObject shopView;

    [SerializeField] ClientHome clientHome;
    [SerializeField] ShopDetailFixedView shopDetailFixedView;
    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_product_id = "product_id";
    private const string key_amount = "amount";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;
        WarningMessage("");
        shopView.SetActive(false);

        shopOpenButton.onClick.AddListener(() => ShopOpen());
        shopCloseButton.onClick.AddListener(() => ShopClose());
    }

    //購入処理
    public void RequestPayment(int index, int amount)
    {
        SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_DECISION);
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
            clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
        }));
    }

    //ショップ開く
    public void ShopOpen()
    {
        shopDetailFixedView.SetPaymentComplete(false);
        shopView.SetActive(true);
        SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_OPEN_1);
        clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //ショップ閉じる
    public void ShopClose()
    {
        shopView.SetActive(false);
        SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_CLOSE);
        clientHome.RefreshHomeWallet();
    }

    //購入警告
    public void WarningMessage(string message)
    {
        buyConfirmWarningText.text = message;
    }
}