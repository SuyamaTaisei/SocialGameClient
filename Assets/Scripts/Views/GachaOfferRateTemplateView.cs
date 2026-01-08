using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaOfferRateTemplateView : MonoBehaviour
{
    [SerializeField] Image gachaOfferRateImage;
    [SerializeField] TextMeshProUGUI gachaOfferRateNameText;
    [SerializeField] TextMeshProUGUI gachaOfferRateRarityText;
    [SerializeField] TextMeshProUGUI gachaOfferRateText;
    [SerializeField] TextMeshProUGUI gachaOfferRatePeriodTitle;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    //ガチャ期間別提供割合表記
    public void SetPeriod()
    {
        var gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaPeriodTemplateView.GachaId);
        gachaOfferRatePeriodTitle.text = gachaPeriodsModel.name;
    }

    //各ガチャ提供割合表記
    public void Set(GachaDataModel data, CharacterDataModel data1, CharacterRaritiesModel data2, float rate, string imagePath)
    {
        gachaOfferRateNameText.text = data1.name;
        gachaOfferRateRarityText.text = data2.name;
        gachaOfferRateText.text = rate.ToString(GameUtility.Const.SHOW_GACHA_RATE_DECIMAL) + GameUtility.Const.SHOW_GACHA_RATE_PERCENT;
        gachaOfferRateImage.sprite = Resources.Load<Sprite>(imagePath);
        gachaOfferRateImage.preserveAspect = true;
    }

    //レアリティごとの合計排出率の表記計算
    public void SetCalculate(GachaDataModel data, ref float rateN, ref float rateR, ref float rateSR, ref float rateSSR)
    {
        if (data.character_id >= GameUtility.Const.GACHA_1000_NUMBER && data.character_id <= GameUtility.Const.GACHA_1999_NUMBER)
        {
            rateN += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        }
        if (data.character_id >= GameUtility.Const.GACHA_2000_NUMBER && data.character_id <= GameUtility.Const.GACHA_2999_NUMBER)
        {
            rateR += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        }
        if (data.character_id >= GameUtility.Const.GACHA_3000_NUMBER && data.character_id <= GameUtility.Const.GACHA_3999_NUMBER)
        {
            rateSR += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        }
        if (data.character_id >= GameUtility.Const.GACHA_4000_NUMBER && data.character_id <= GameUtility.Const.GACHA_4999_NUMBER)
        {
            rateSSR += data.weight / GameUtility.Const.GACHA_TOTAL_RATE;
        }
    }

    //合計排出率の表記
    public void SetTotalRate(float rateN, float rateR, float rateSR, float rateSSR)
    {
        clientGacha.GachaOfferRateTotalText.text =
                       GameUtility.Const.SHOW_GACHA_RARITY_N + rateN.ToString(GameUtility.Const.SHOW_GACHA_RATE_DECIMAL) + GameUtility.Const.SHOW_GACHA_RATE_PERCENT +
            "      " + GameUtility.Const.SHOW_GACHA_RARITY_R + rateR.ToString(GameUtility.Const.SHOW_GACHA_RATE_DECIMAL) + GameUtility.Const.SHOW_GACHA_RATE_PERCENT +
            "      " + GameUtility.Const.SHOW_GACHA_RARITY_SR + rateSR.ToString(GameUtility.Const.SHOW_GACHA_RATE_DECIMAL) + GameUtility.Const.SHOW_GACHA_RATE_PERCENT +
            "      " + GameUtility.Const.SHOW_GACHA_RARITY_SSR + rateSSR.ToString(GameUtility.Const.SHOW_GACHA_RATE_DECIMAL) + GameUtility.Const.SHOW_GACHA_RATE_PERCENT;
    }
}
