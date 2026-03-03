using UnityEngine;
using System.Collections.Generic;

public class GachaOfferRateList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] DataGetManager dataGetManager;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaOfferRateTemplateView gachaOfferRateTemplateView;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    private void Start()
    {
        List<GachaDataModel> gachaDataList = GachaDataTable.SelectAllGachaId(gachaPeriodTemplateView.GachaId);

        float rateN = 0f, rateR = 0f, rateSR = 0f, rateSSR = 0f;

        for (int i = 0; i < gachaDataList.Count; i++)
        {
            //データの生成
            var item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaOfferRateTemplateView>();

            //データの取得
            var data = gachaDataList[i];
            var (data1, data2, imagePath) = dataGetManager.GetCharacterData(data.character_id);
            float rate = data.weight / GameUtility.Const.GACHA_TOTAL_RATE;

            //データの描画
            view.Set(data, data1, data2, rate, imagePath);
            view.SetCalculate(data, ref rateN, ref rateR, ref rateSR, ref rateSSR);
        }

        //ガチャ期間別ガチャ提供割合のデータ描画
        gachaOfferRateTemplateView.SetPeriod();

        //合計値のデータ描画
        gachaOfferRateTemplateView.SetTotalRate(rateN, rateR, rateSR, rateSSR);
    }
}
