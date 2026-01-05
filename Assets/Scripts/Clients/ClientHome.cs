using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientHome : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;
    [SerializeField] TextMeshProUGUI userNameText;

    [SerializeField] Image staminaGauge;
    [SerializeField] TextMeshProUGUI staminaValueText;
    [SerializeField] Button staminaRecoveryButton;
    [SerializeField] Button gameMatchButton;

    private UsersModel usersModel;

    private ApiConnect apiConnect;

    private const string column_id = "id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;
        usersModel = UsersTable.Select();

        RequestHome(usersModel, GameUtility.Const.HOME_URL);
        WalletApply(coinText, gemFreeText, gemPaidText);
        userNameText.text = usersModel.user_name;
    }

    private void Update()
    {
        WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //リクエスト送信処理
    public void RequestHome(UsersModel usersModel, string endPoint)
    {
        if (!string.IsNullOrEmpty(usersModel.id))
        {
            string id = usersModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(endPoint, form));
        }
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