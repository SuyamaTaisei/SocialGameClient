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
    private UsersModel usersModel;
    private WalletsModel walletsModel;

    private void Start()
    {
        apiConnect  = FindFirstObjectByType<ApiConnect>();
        usersModel   = UsersTable.Select();
        walletsModel = WalletsTable.Select();

        if (!string.IsNullOrEmpty(usersModel.id))
        {
            string id = usersModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.HOME_URL, form));

            coinText.text     = walletsModel.coin_amount.ToString();
            gemFreeText.text  = walletsModel.gem_free_amount.ToString();
            gemPaidText.text  = walletsModel.gem_paid_amount.ToString();
            userNameText.text = usersModel.user_name;
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(usersModel.id))
        {
            walletsModel = WalletsTable.Select();
            coinText.text = walletsModel.coin_amount.ToString();
            gemFreeText.text = walletsModel.gem_free_amount.ToString();
            gemPaidText.text = walletsModel.gem_paid_amount.ToString();
        }
    }
}