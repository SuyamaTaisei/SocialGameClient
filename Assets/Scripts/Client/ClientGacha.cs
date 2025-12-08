using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//上から順に、リスト系、ボタン系、メッセージ系、エフェクト系の順で記述
public class ClientGacha : MonoBehaviour
{
    //ガチャ画面表示
    [SerializeField] GameObject gachaView;
    [SerializeField] GameObject gachaConfirmView;
    [SerializeField] GameObject gachaResultView;
    [SerializeField] GameObject gachaRewardView;
    [SerializeField] GameObject gachaOfferRateView;

    //ガチャ画面テキスト
    [SerializeField] TextMeshProUGUI gachaPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodText;
    [SerializeField] TextMeshProUGUI gachaOfferRatePeriodText;
    [SerializeField] TextMeshProUGUI gachaOfferRateTotalText;
    [SerializeField] TextMeshProUGUI gachaListPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaListPeriodText;
    [SerializeField] TextMeshProUGUI singleCostText;
    [SerializeField] TextMeshProUGUI multiCostText;
    [SerializeField] TextMeshProUGUI singleText;
    [SerializeField] TextMeshProUGUI multiText;
    [SerializeField] TextMeshProUGUI confirmText;

    //ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    //リスト
    [SerializeField] GachaResultList gachaResultList;
    [SerializeField] GachaRewardList gachaRewardList;

    //ボタン
    [SerializeField] Button gachaExecuteButton;

    //メッセージ
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI nothingText;

    private float rateN = 0;
    private float rateR = 0;
    private float rateSR = 0;
    private float rateSSR = 0;
    private int gacha_id;
    private int gacha_count;

    public int GachaId => gacha_id;
    public int GachaCount => gacha_count;
    public GameObject GachaResultView => gachaResultView;

    private ApiConnect apiConnect;

    private const string column_id = "id";
    private const string column_gacha_id = "gacha_id";
    private const string key_gacha_count = "gacha_count";

    //DBモデル
    private UsersModel usersModel;
    private WalletsModel walletsModel;
    private GachaPeriodsModel gachaPeriodsModel;
    private CharacterDataModel characterDataModel;
    private CharacterRaritiesModel characterRaritiesModel;
    private ItemDataModel itemDataModel;
    private ItemRaritiesModel itemRaritiesModel;

    private void Awake()
    {
        ShowGachaPeriodList(0);
    }

    void Start()
    {
        apiConnect = FindFirstObjectByType<ApiConnect>();
        gachaExecuteButton.onClick.AddListener(() => GachaExecuteButton(gacha_id, gacha_count));
        NothingMessage(GameUtility.Const.SHOW_GACHA_REWARD_NOTHING);
        usersModel = UsersTable.Select();
        warningText.text = "";
        gachaView.SetActive(false);
        gachaConfirmView.SetActive(false);
        gachaResultView.SetActive(false);
        gachaRewardView.SetActive(false);
        gachaOfferRateView.SetActive(false);
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

    //ガチャ期間リスト
    public void ShowGachaPeriodList(int index)
    {
        //全ガチャ期間のレコード取得
        List<GachaPeriodsModel> gachaDataList = GachaPeriodsTable.SelectAll();
        var data = gachaDataList[index];

        //idが一致するレコードを取得
        gacha_id = data.id;
        gachaPeriodsModel = GachaPeriodsTable.SelectId(gacha_id);

        //ガチャ期間リスト表記
        gachaListPeriodTitle.text = gachaPeriodsModel.name;
        gachaListPeriodText.text = GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_1 + gachaPeriodsModel.end + GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_2;

        //各ガチャ期間内の表記
        gachaPeriodTitle.text = gachaPeriodsModel.name;
        gachaPeriodText.text = GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_1 + gachaPeriodsModel.end + GameUtility.Const.SHOW_GACHA_PERIOD_TEXT_2;
        singleCostText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GEM;
        multiCostText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GEM;
        singleText.text = gachaPeriodsModel.single_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
        multiText.text = gachaPeriodsModel.multi_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;

        gachaOfferRatePeriodText.text = gachaPeriodsModel.name;
    }

    //ピックアップ表示リスト
    public void ShowGachaPickUpList(GachaPickUpTempView viewGacha, int index)
    {
        List<GachaDataModel> gachaDataModel = GachaDataTable.SelectAllGachaId(gacha_id);

        foreach (var list in gachaDataModel)
        {
            if (list.character_id == index)
            {
                //ガチャ期間idが同じcharacter_id全部と、任意のピックアップガチャの値が一致するデータのみを取得
                characterDataModel = CharacterDataTable.SelectId(index);
                characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
                string imagePath = $"Images/Characters/{index}";

                //表記
                viewGacha.NameText.text = characterDataModel.name;
                viewGacha.RarityText.text = characterRaritiesModel.name;
                viewGacha.CharacterImage.sprite = Resources.Load<Sprite>(imagePath);
                viewGacha.CharacterImage.preserveAspect = true;
            }
        }
    }

    //ガチャ提供割合リスト
    public void ShowGachaOfferRateList(GachaOfferRateTempView viewGacha, int index)
    {
        List<GachaDataModel> gachaDataList = GachaDataTable.SelectAllGachaId(gacha_id);

        //そのガチャ期間IDと一致するレコードのindex番目を取得
        var data = gachaDataList[index];

        if (data.character_id >= GameUtility.Const.GACHA_1000_NUMBER && data.character_id <= GameUtility.Const.GACHA_1999_NUMBER) rateN += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        if (data.character_id >= GameUtility.Const.GACHA_2000_NUMBER && data.character_id <= GameUtility.Const.GACHA_2999_NUMBER) rateR += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        if (data.character_id >= GameUtility.Const.GACHA_3000_NUMBER && data.character_id <= GameUtility.Const.GACHA_3999_NUMBER) rateSR += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        if (data.character_id >= GameUtility.Const.GACHA_4000_NUMBER && data.character_id <= GameUtility.Const.GACHA_4999_NUMBER) rateSSR += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;

        gachaOfferRateTotalText.text = "N : " + rateN.ToString("0.###") + "%      " + "R : " + rateR.ToString("0.###") + "%      " + "SR : " + rateSR.ToString("0.###") + "%      " + "SSR : " + rateSSR.ToString("0.###") + "%";

        //ガチャ期間idのcharacter_id全部と、任意のcharacter_idが一致する場合のみ
        characterDataModel = CharacterDataTable.SelectId(data.character_id);
        characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
        string imagePath = $"Images/Characters/{data.character_id}";
        float rate = data.weight / GameUtility.Const.GACHA_TOTAL_RATE;

        //表記
        viewGacha.NameText.text = characterDataModel.name;
        viewGacha.RarityText.text = characterRaritiesModel.name;
        viewGacha.RateText.text = rate.ToString("0.###") + "%";
        viewGacha.CharacterImage.sprite = Resources.Load<Sprite>(imagePath);
        viewGacha.CharacterImage.preserveAspect = true;
    }

    //ガチャ報酬単一表示リスト
    public void ShowGachaSingleRewardList(GachaResultTempView view, GachaResultsModel[] singleExchangeItems, ref int singleExchangeIndex)
    {
        //ガチャ報酬配列の有効範囲内のみ
        if (singleExchangeItems != null && singleExchangeIndex < singleExchangeItems.Length)
        {
            //ガチャが被った時だけ、1要素ずつガチャ報酬(変換したアイテム)を表示
            var exchange = singleExchangeItems[singleExchangeIndex];

            //次の要素のガチャ報酬用にインクリメント
            singleExchangeIndex++;

            //アイテムidが一致するデータを取得
            itemDataModel = ItemDataTable.SelectId(exchange.item_id);
            itemRaritiesModel = ItemRaritiesTable.SelectId(itemDataModel.rarity_id);
            string itemImagePath = $"Images/Items/{exchange.item_id}";

            //表記
            view.ItemNameText.text = itemDataModel.name;
            view.ItemRarityText.text = itemRaritiesModel.name;
            view.ItemAmountText.text = exchange.amount.ToString();
            view.ItemOtherObject.SetActive(true);
            Sprite imageSprite = Resources.Load<Sprite>(itemImagePath);

            //画像設定
            if (view != null)
            {
                view.ItemImage.gameObject.SetActive(true);
                view.ItemImage.sprite = imageSprite;
                view.ItemImage.preserveAspect = true;
            }
        }
    }

    //ガチャリクエスト送信
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

    //ガチャ結果、ガチャ報酬表示リセット
    public void CloseGachaResultResetButton()
    {
        gachaResultView.SetActive(false);
        NothingMessage(GameUtility.Const.SHOW_GACHA_REWARD_NOTHING);
        foreach (Transform child in gachaResultList.Content)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in gachaRewardList.Content)
        {
            Destroy(child.gameObject);
        }
    }

    //単発
    public void GachaSingleButton()
    {
        gacha_count = gachaPeriodsModel.single_count;
        confirmText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //連発
    public void GachaMultiButton()
    {
        gacha_count = gachaPeriodsModel.multi_count;
        confirmText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
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
    }

    //ガチャ実行確認画面閉じる
    public void CloseConfirmButton()
    {
        gachaConfirmView.SetActive(false);
        warningText.text = "";
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

    //ガチャ報酬無し警告
    public void NothingMessage(string message)
    {
        nothingText.text = message;
    }

    //購入警告
    public void WarningMessage(string message)
    {
        warningText.text = message;
    }

    //新規と所持済みで透明度を変更
    public void GachaResultColorChangeEffect(GachaResultTempView view, float value)
    {
        var color = view.CharacterImage.color;
        color.a = value;
        view.CharacterImage.color = color;
    }
}