using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientShop : MonoBehaviour
{
    [SerializeField] GameObject shopView;
    [SerializeField] Transform content;
    [SerializeField] GameObject itemPrefab;

    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    private ApiConnect apiConnect;
    private const string column_id = "id";
    private const string column_product_id = "product_id";
    private const int maxCount = 4;

    //DBモデル
    private UsersModel userModel;
    private WalletsModel walletModel;

    private void Start()
    {
        userModel = UsersTable.Select();
        apiConnect = FindAnyObjectByType<ApiConnect>();
        shopView.SetActive(false);

        List<ShopDataModel> list = ShopDataTable.SelectAll();

        for (int i = 0; i < list.Count && i < maxCount; i++)
        {
            GameObject item = Instantiate(itemPrefab, content);

            //ボタンの取得
            Button button = item.GetComponentInChildren<Button>();

            //選択されたボタンの番号をセット
            int index = list[i].product_id;

            //生成されたリストからセットする
            ShopItemView view = item.GetComponent<ShopItemView>();
            if (view != null)
            {
                view.Set(list[i]);
            }

            //ボタン押下処理
            button.onClick.AddListener(() => PaymentButton(index));
        }
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
    public void PaymentButton(int index)
    {
        userModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, userModel.id),
            new MultipartFormDataSection(column_product_id, index.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.PAYMENT_URL, form));
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