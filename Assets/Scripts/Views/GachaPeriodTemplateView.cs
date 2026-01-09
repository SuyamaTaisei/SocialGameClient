using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaPeriodTemplateView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gachaPeriodListTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodListText;
    [SerializeField] TextMeshProUGUI gachaPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodText;
    [SerializeField] TextMeshProUGUI gachaSingleCostText;
    [SerializeField] TextMeshProUGUI gachaMultiCostText;
    [SerializeField] TextMeshProUGUI gachaSingleText;
    [SerializeField] TextMeshProUGUI gachaMultiText;

    [SerializeField] ClientGacha clientGacha;

    private int gacha_id;

    public int GachaId => gacha_id;

    private void Awake()
    {
        SetList(0);
    }

    //ガチャ期間リスト
    public void SetList(int index)
    {
        //データの取得
        List<GachaPeriodsModel> gachaDataList = GachaPeriodsTable.SelectAll();
        var data = gachaDataList[index];
        gacha_id = data.id;
        var gachaPeriodsModel = GachaPeriodsTable.SelectId(gacha_id);

        //ガチャ期間リスト表記
        gachaPeriodListTitle.text = gachaPeriodsModel.name;
        var periodEnd = gacha_id != GameUtility.Const.GACHA_PERIOD_DEFAULT ? GameUtility.Const.SHOW_GACHA_PERIOD_START + gachaPeriodsModel.end + GameUtility.Const.SHOW_GACHA_PERIOD_END : GameUtility.Const.SHOW_GACHA_PERIOD_NOTHING;
        gachaPeriodListText.text = periodEnd;

        //ガチャ期間内の表記
        gachaPeriodTitle.text = gachaPeriodsModel.name;
        gachaPeriodText.text = periodEnd;
        gachaSingleCostText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GEM;
        gachaMultiCostText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GEM;
        gachaSingleText.text = gachaPeriodsModel.single_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
        gachaMultiText.text = gachaPeriodsModel.multi_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
    }
}
