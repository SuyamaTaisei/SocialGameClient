using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaPeriodTemplateView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gachaPeriodListTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodListText;

    [SerializeField] GachaFixedView gachaFixedView;

    private int gacha_id;

    public int GachaId => gacha_id;

    private void Awake()
    {
        Set(0);
    }

    //ガチャ期間リスト
    public void Set(int index)
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
        gachaFixedView.Set(gachaPeriodsModel, periodEnd);
    }
}
