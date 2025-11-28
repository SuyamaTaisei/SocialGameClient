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
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] TextMeshProUGUI gachaPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodText;
    [SerializeField] TextMeshProUGUI gachaListPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaListPeriodText;
    [SerializeField] TextMeshProUGUI singleCostText;
    [SerializeField] TextMeshProUGUI multiCostText;
    [SerializeField] TextMeshProUGUI singleText;
    [SerializeField] TextMeshProUGUI multiText;
    [SerializeField] TextMeshProUGUI confirmText;

    //現在所持ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //購入警告
    [SerializeField] TextMeshProUGUI warningText;

    private int gacha_id;
    private int gacha_count;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_gacha_id = "gacha_id";
    private const string key_gacha_count = "gacha_count";

    //DBモデル
    private UsersModel usersModel;
    private WalletsModel walletsModel;
    private GachaPeriodsModel gachaPeriodsModel;

    void Start()
    {
        GachaPeriodList(GameUtility.Const.GACHA_START_DEFAULT_LIST);
        apiConnect = FindFirstObjectByType<ApiConnect>();
        yesButton.onClick.AddListener(() => GachaExecuteButton(gacha_id, gacha_count));
        usersModel = UsersTable.Select();
        warningText.text = "";
        gachaView.SetActive(false);
        gachaConfirmView.SetActive(false);
    }

    //表記リアルタイム更新
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

    //ガチャ開く
    public void OpenButton()
    {
        gachaView.SetActive(true);
    }

    //ガチャ閉じる
    public void CloseButton()
    {
        gachaView.SetActive(false);
    }

    //ガチャ種類
    public void GachaPeriodList(int index)
    {
        gacha_id = index;

        //idが一致するレコードを取得
        gachaPeriodsModel = GachaPeriodsTable.SelectId(index);

        //表記
        gachaListPeriodTitle.text = gachaPeriodsModel.name;
        gachaListPeriodText.text = GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_1 + gachaPeriodsModel.end + GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_2;

        gachaPeriodTitle.text = gachaPeriodsModel.name;
        gachaPeriodText.text = GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_1 + gachaPeriodsModel.end + GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_2;
        singleCostText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GEM;
        multiCostText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GEM;
        singleText.text = GameUtility.Const.GACHA_SINGLE_COUNT.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
        multiText.text = GameUtility.Const.GACHA_MULTI_COUNT.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
    }

    //単発
    public void SingleGachaButton()
    {
        gacha_count = GameUtility.Const.GACHA_SINGLE_COUNT;
        confirmText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //連発
    public void MultiGachaButton()
    {
        gacha_count = GameUtility.Const.GACHA_MULTI_COUNT;
        confirmText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //確認画面開く
    public void OpenConfirmButton()
    {
        gachaConfirmView.SetActive(true);
    }

    //はいボタン
    public void GachaExecuteButton(int gacha_id, int gacha_count)
    {
        usersModel = UsersTable.Select();
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_gacha_id, gacha_id.ToString()),
            new MultipartFormDataSection(key_gacha_count, gacha_count.ToString())
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.GACHA_EXECUTE_URL, form));
    }

    //確認画面閉じる
    public void CloseConfirmButton()
    {
        gachaConfirmView.SetActive(false);
        warningText.text = "";
    }

    //購入警告
    public void WarningMessage(string message)
    {
        warningText.text = message;
    }
}