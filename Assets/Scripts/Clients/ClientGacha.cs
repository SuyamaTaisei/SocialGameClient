using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientGacha : MonoBehaviour
{
    //ガチャ画面表示
    [SerializeField] GameObject gachaView;
    [SerializeField] GameObject gachaConfirmView;
    [SerializeField] GameObject gachaResultView;
    [SerializeField] GameObject gachaRewardView;
    [SerializeField] GameObject gachaOfferRateView;
    [SerializeField] GameObject gachaLogView;

    //ガチャ画面テキスト
    [SerializeField] TextMeshProUGUI gachaOfferRateTotalText;
    [SerializeField] TextMeshProUGUI gachaConfirmText;

    //ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //ボタン
    [SerializeField] Button gachaExecuteButton;
    [SerializeField] Button gachaCancelButton;
    [SerializeField] Button gachaSingleExecuteButton;
    [SerializeField] Button gachaMultiExecuteButton;

    [SerializeField] Button gachaOpenButton;
    [SerializeField] Button gachaRewardOpenButton;
    [SerializeField] Button gachaLogOpenButton;
    [SerializeField] Button gachaOfferRateOpenButton;

    [SerializeField] Button gachaCloseButton;
    [SerializeField] Button gachaLogCloseButton;
    [SerializeField] Button gachaOfferRateCloseButton;
    [SerializeField] Button gachaRewardCloseButton;
    [SerializeField] Button gachaResultCloseButton;

    //メッセージ
    [SerializeField] TextMeshProUGUI gachaWarningText;
    [SerializeField] TextMeshProUGUI gachaLogNothingText;

    [SerializeField] ClientHome clientHome;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    private int gacha_count;

    public int GachaCount => gacha_count;
    public TextMeshProUGUI GachaOfferRateTotalText => gachaOfferRateTotalText;
    public GameObject GachaResultView => gachaResultView;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_gacha_id = "gacha_id";
    private const string key_gacha_count = "gacha_count";

    void Start()
    {
        apiConnect = ApiConnect.Instance;
        gachaExecuteButton.onClick.AddListener(() => GachaExecuteButton(gachaPeriodTemplateView.GachaId, gacha_count));
        gachaCancelButton.onClick.AddListener(() => CloseConfirmButton());
        gachaSingleExecuteButton.onClick.AddListener(() => {
            GachaSingleButton();
            OpenConfirmButton();
        });
        gachaMultiExecuteButton.onClick.AddListener(() => {
            GachaMultiButton();
            OpenConfirmButton();
        });

        gachaOpenButton.onClick.AddListener(() => OpenGachaButton());
        gachaRewardOpenButton.onClick.AddListener(() => OpenGachaRewardButton());
        gachaLogOpenButton.onClick.AddListener(() => OpenGachaLogButton());
        gachaOfferRateOpenButton.onClick.AddListener(() => OpenGachaOfferRateButton());

        gachaCloseButton.onClick.AddListener(() => CloseGachaButton());
        gachaLogCloseButton.onClick.AddListener(() => CloseGachaLogButton());
        gachaOfferRateCloseButton.onClick.AddListener(() => CloseGachaOfferRateButton());
        gachaRewardCloseButton.onClick.AddListener(() => CloseGachaRewardButton());
        gachaResultCloseButton.onClick.AddListener(() => CloseGachaResultResetButton());

        WarningMessage("");
        gachaView.SetActive(false);
        gachaConfirmView.SetActive(false);
        gachaResultView.SetActive(false);
        gachaRewardView.SetActive(false);
        gachaOfferRateView.SetActive(false);
        gachaLogView.SetActive(false);
    }

    //表記リアルタイム更新
    private void Update()
    {
        clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //ガチャリクエスト送信
    public void GachaExecuteButton(int gacha_id, int gacha_count)
    {
        var usersModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_gacha_id, gacha_id.ToString()),
            new MultipartFormDataSection(key_gacha_count, gacha_count.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.GACHA_EXECUTE_URL, form));
    }

    //ガチャ結果、ガチャ報酬表示リセット
    public void CloseGachaResultResetButton()
    {
        gachaResultView.SetActive(false);
    }

    //単発
    public void GachaSingleButton()
    {
        var gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaPeriodTemplateView.GachaId);
        gacha_count = gachaPeriodsModel.single_count;
        gachaConfirmText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //連発
    public void GachaMultiButton()
    {
        var gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaPeriodTemplateView.GachaId);
        gacha_count = gachaPeriodsModel.multi_count;
        gachaConfirmText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //ガチャ画面開く
    public void OpenGachaButton()
    {
        gachaView.SetActive(true);
    }

    //ガチャ画面閉じる
    public void CloseGachaButton()
    {
        gachaView.SetActive(false);
    }

    //ガチャ実行確認画面開く
    public void OpenConfirmButton()
    {
        gachaConfirmView.SetActive(true);
        WarningMessage("");
    }

    //ガチャ実行確認画面閉じる
    public void CloseConfirmButton()
    {
        gachaConfirmView.SetActive(false);
        WarningMessage("");
    }

    //ガチャ報酬開く
    public void OpenGachaRewardButton()
    {
        gachaRewardView.SetActive(true);
    }

    //ガチャ報酬閉じる
    public void CloseGachaRewardButton()
    {
        gachaRewardView.SetActive(false);
    }

    //ガチャ提供割合開く
    public void OpenGachaOfferRateButton()
    {
        gachaOfferRateView.SetActive(true);
    }

    //ガチャ提供割合閉じる
    public void CloseGachaOfferRateButton()
    {
        gachaOfferRateView.SetActive(false);
    }

    //ガチャ履歴開く
    public void OpenGachaLogButton()
    {
        gachaLogView.SetActive(true);
    }

    //ガチャ履歴閉じる
    public void CloseGachaLogButton()
    {
        gachaLogView.SetActive(false);
    }

    //ガチャ報酬無し警告
    public void NothingRewardMessage(bool enabled)
    {
        gachaRewardOpenButton.interactable = enabled;
        var color = gachaRewardOpenButton.image.color;
        color.a = enabled ? 1 : 0.07f;
        gachaRewardOpenButton.image.color = color;

        var text = gachaRewardOpenButton.GetComponentInChildren<TextMeshProUGUI>();
        text.color = color;
    }

    //ガチャ履歴無し警告
    public void NothingGachaLogMessage(string message)
    {
        gachaLogNothingText.text = message;
    }

    //購入警告
    public void WarningMessage(string message)
    {
        gachaWarningText.text = message;
    }
}