using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientShop : MonoBehaviour
{
    [SerializeField] GameObject shopView;
    [SerializeField] GameObject itemListView;
    [SerializeField] GameObject gemListView;

    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    private ApiConnect apiConnect;
    private const string column_id = "id";
    private const string column_product_id = "product_id";

    //DBモデル
    private UsersModel userModel;
    private WalletsModel walletModel;

    private void Start()
    {
        userModel = UsersTable.Select();
        apiConnect = FindAnyObjectByType<ApiConnect>();
        shopView.SetActive(false);
        OpenGemListButton();
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(userModel.id))
        {
            walletModel = WalletsTable.Select();
            coinText.text = walletModel.coin_amount.ToString();
            gemFreeText.text = walletModel.gem_free_amount.ToString();
            gemPaidText.text = walletModel.gem_paid_amount.ToString();
        }
    }

    //ボタン押下処理
    public void PaymentButton(int index1)
    {
        userModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, userModel.id),
            new MultipartFormDataSection(column_product_id, index1.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.PAYMENT_URL, form));
    }

    public void OpenItemListButton()
    {
        itemListView.SetActive(true);
        gemListView.SetActive(false);
    }

    public void OpenGemListButton()
    {
        gemListView.SetActive(true);
        itemListView.SetActive(false);
    }

    //ショップ開く
    public void ShopOpenButton()
    {
        shopView.SetActive(true);
    }

    //閉じる
    public void ShopCloseButton()
    {
        shopView.SetActive(false);
    }
}