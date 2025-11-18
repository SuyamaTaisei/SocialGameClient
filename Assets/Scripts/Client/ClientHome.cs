using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientHome : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;
    [SerializeField] TextMeshProUGUI userNameText;

    private ApiConnect apiConnect;

    private const string column_id = "id";

    //DBモデル
    private UsersModel userModel;
    private WalletsModel walletModel;

    private void Start()
    {
        apiConnect  = FindFirstObjectByType<ApiConnect>();
        userModel   = UsersTable.Select();
        walletModel = WalletsTable.Select();

        if (!string.IsNullOrEmpty(userModel.id))
        {
            string id = userModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.HOME_URL, form));

            coinText.text     = walletModel.coin_amount.ToString();
            gemFreeText.text  = walletModel.gem_free_amount.ToString();
            gemPaidText.text  = walletModel.gem_paid_amount.ToString();
            userNameText.text = userModel.user_name;
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
}