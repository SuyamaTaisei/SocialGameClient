using System.Collections.Generic;
using TMPro;
using SoundSystem;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientGacha : MonoBehaviour
{
    //ガチャ画面テキスト
    [SerializeField] TextMeshProUGUI gachaOfferRateTotalText;

    //ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //メッセージ
    [SerializeField] TextMeshProUGUI gachaWarningText;
    [SerializeField] TextMeshProUGUI gachaLogNothingText;

    //ボタン
    [SerializeField] Button gachaOpenButton;
    [SerializeField] Button gachaRewardOpenButton;
    [SerializeField] Button gachaLogOpenButton;
    [SerializeField] Button gachaOfferRateOpenButton;
    [SerializeField] Button gachaCloseButton;
    [SerializeField] Button gachaLogCloseButton;
    [SerializeField] Button gachaOfferRateCloseButton;
    [SerializeField] Button gachaRewardCloseButton;
    [SerializeField] Button gachaResultCloseButton;

    //ガチャ画面表示
    [SerializeField] GameObject gachaView;
    [SerializeField] GameObject gachaResultView;
    [SerializeField] GameObject gachaRewardView;
    [SerializeField] GameObject gachaOfferRateView;
    [SerializeField] GameObject gachaLogView;

    [SerializeField] ClientHome clientHome;
    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_mission_id = "mission_id";
    private const string column_gacha_id = "gacha_id";
    private const string key_gacha_count = "gacha_count";

    public TextMeshProUGUI GachaOfferRateTotalText => gachaOfferRateTotalText;
    public Button GachaRewardOpenButton => gachaRewardOpenButton;
    public GameObject GachaResultView => gachaResultView;

    void Start()
    {
        apiConnect = ApiConnect.Instance;

        WarningMessage("");

        gachaView.SetActive(false);
        gachaResultView.SetActive(false);
        gachaRewardView.SetActive(false);
        gachaOfferRateView.SetActive(false);
        gachaLogView.SetActive(false);

        gachaOpenButton.onClick.AddListener(() => GachaOpenClose(true));
        gachaRewardOpenButton.onClick.AddListener(() => GachaRewardOpenClose(true));
        gachaLogOpenButton.onClick.AddListener(() => GachaLogOpenClose(true));
        gachaOfferRateOpenButton.onClick.AddListener(() => GachaOfferRateOpenClose(true));
        gachaCloseButton.onClick.AddListener(() => GachaOpenClose(false));
        gachaLogCloseButton.onClick.AddListener(() => GachaLogOpenClose(false));
        gachaOfferRateCloseButton.onClick.AddListener(() => GachaOfferRateOpenClose(false));
        gachaRewardCloseButton.onClick.AddListener(() => GachaRewardOpenClose(false));
        gachaResultCloseButton.onClick.AddListener(() => GachaResultClose());
    }

    //表記リアルタイム更新
    private void Update()
    {
        clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
    }

    //ガチャリクエスト送信
    public void RequestGacha(int gacha_id, int gacha_count)
    {
        var usersModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_mission_id, "1003"),
            new MultipartFormDataSection(column_gacha_id, gacha_id.ToString()),
            new MultipartFormDataSection(key_gacha_count, gacha_count.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.GACHA_EXECUTE_URL, form));
    }

    //ガチャ結果、ガチャ報酬表示リセット
    public void GachaResultClose()
    {
        gachaResultView.SetActive(false);
        SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_CLOSE);
    }

    //ガチャ画面開閉
    public void GachaOpenClose(bool enabled)
    {
        gachaView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //ガチャ報酬開閉
    public void GachaRewardOpenClose(bool enabled)
    {
        gachaRewardView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //ガチャ提供割合開閉
    public void GachaOfferRateOpenClose(bool enabled)
    {
        gachaOfferRateView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //ガチャ履歴書開閉
    public void GachaLogOpenClose(bool enabled)
    {
        gachaLogView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //ガチャ履歴無し警告
    public void GachaLogMessage(string message)
    {
        gachaLogNothingText.text = message;
    }

    //購入警告
    public void WarningMessage(string message)
    {
        gachaWarningText.text = message;
    }
}