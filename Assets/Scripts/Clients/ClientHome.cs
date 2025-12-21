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

    private void Start()
    {
        apiConnect = ApiConnect.Instance;
        var usersModel = UsersTable.Select();

        if (!string.IsNullOrEmpty(usersModel.id))
        {
            string id = usersModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.HOME_URL, form));

            WalletApply(coinText, gemFreeText, gemPaidText);
            userNameText.text = usersModel.user_name;
        }
    }

    private void Update()
    {
        WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //ウォレット反映処理
    public void WalletApply(TextMeshProUGUI coinText, TextMeshProUGUI gemFreeText, TextMeshProUGUI gemPaidText)
    {
        var walletsModel = WalletsTable.Select();
        coinText.text = walletsModel.coin_amount.ToString();
        gemFreeText.text = walletsModel.gem_free_amount.ToString();
        gemPaidText.text = walletsModel.gem_paid_amount.ToString();
    }
}