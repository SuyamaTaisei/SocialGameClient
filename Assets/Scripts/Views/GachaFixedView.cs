using TMPro;
using UnityEngine;

public class GachaFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gachaPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodText;
    [SerializeField] TextMeshProUGUI gachaSingleCostText;
    [SerializeField] TextMeshProUGUI gachaMultiCostText;
    [SerializeField] TextMeshProUGUI gachaSingleText;
    [SerializeField] TextMeshProUGUI gachaMultiText;

    public void Set(GachaPeriodsModel gachaPeriodsModel, string periodEnd)
    {
        gachaPeriodTitle.text = gachaPeriodsModel.name;
        gachaPeriodText.text = periodEnd;
        gachaSingleCostText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GEM;
        gachaMultiCostText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GEM;
        gachaSingleText.text = gachaPeriodsModel.single_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
        gachaMultiText.text = gachaPeriodsModel.multi_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
    }
}
