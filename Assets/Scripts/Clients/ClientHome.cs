using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

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

    private ApiConnect apiConnect;

    private const string column_id = "id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;
        var usersModel = UsersTable.Select();

        RequestHome(usersModel, GameUtility.Const.HOME_URL);
        WalletApply(coinText, gemFreeText, gemPaidText);
        StartCoroutine(StaminaAutoIncrease());
        StaminaButtonCtrl();

        userNameText.text = usersModel.user_name;
        staminaValueText.text = usersModel.last_stamina.ToString() + "/100";
        staminaGauge.fillAmount = (float)usersModel.last_stamina / 100;

        staminaRecoveryButton.onClick.AddListener(() => { RequestHome(usersModel, GameUtility.Const.STAMINA_INCREASE_URL); });
        gameMatchButton.onClick.AddListener(() => { RequestHome(usersModel, GameUtility.Const.STAMINA_DECREASE_URL); });
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
            StartCoroutine(apiConnect.Send(endPoint, form, (action) => {
                StaminaApply();
                StaminaButtonCtrl();
            }));
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

    //スタミナ反映処理
    public void StaminaApply()
    {
        var usersModel = UsersTable.Select();
        staminaValueText.text = usersModel.last_stamina.ToString() + "/100";
        staminaGauge.fillAmount = (float)usersModel.last_stamina / 100;
    }

    //スタミナ、対戦ボタン押下制御
    public void StaminaButtonCtrl()
    {
        var usersModel = UsersTable.Select();
        var walletsModel = WalletsTable.Select();
        staminaRecoveryButton.interactable = usersModel.last_stamina < 199 && walletsModel.gem_paid_amount + walletsModel.gem_free_amount >= 50;
        gameMatchButton.interactable = usersModel.last_stamina >= 5;
    }

    //スタミナ自然回復処理。1分毎に1回復。最大値の場合はスキップ (基本はホームにいる時のみ実行。ゲームプレイ時などは差分計算で増やして負荷軽減)
    private IEnumerator StaminaAutoIncrease()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(60f);
            var usersModel = UsersTable.Select();

            if (usersModel.last_stamina >= 199)
            {
                continue;
            }

            RequestHome(usersModel, GameUtility.Const.STAMINA_AUTO_INCREASE_URL);
        }
    }
}