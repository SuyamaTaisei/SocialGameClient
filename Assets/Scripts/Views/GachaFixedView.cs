using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gachaPeriodTitle;
    [SerializeField] TextMeshProUGUI gachaPeriodText;
    [SerializeField] TextMeshProUGUI gachaSingleCostText;
    [SerializeField] TextMeshProUGUI gachaMultiCostText;
    [SerializeField] TextMeshProUGUI gachaSingleText;
    [SerializeField] TextMeshProUGUI gachaMultiText;
    [SerializeField] TextMeshProUGUI gachaConfirmText;

    [SerializeField] Button gachaExecuteButton;
    [SerializeField] Button gachaCancelButton;
    [SerializeField] Button gachaSingleExecuteButton;
    [SerializeField] Button gachaMultiExecuteButton;

    [SerializeField] GameObject gachaConfirmView;

    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    private int gachaCount;

    public int GachaCount => gachaCount;

    private void Start()
    {
        SetGachaConfirmClose();

        gachaExecuteButton.onClick.AddListener(() => clientGacha.RequestGacha(gachaPeriodTemplateView.GachaId, gachaCount));
        gachaCancelButton.onClick.AddListener(() => SetGachaConfirmClose());
        gachaSingleExecuteButton.onClick.AddListener(() => {
            SetGachaSingle();
            SetGachaConfirmOpen();
        });
        gachaMultiExecuteButton.onClick.AddListener(() => {
            SetGachaMulti();
            SetGachaConfirmOpen();
        });
    }

    public void Set(GachaPeriodsModel gachaPeriodsModel, string periodEnd)
    {
        gachaPeriodTitle.text = gachaPeriodsModel.name;
        gachaPeriodText.text = periodEnd;
        gachaSingleCostText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GEM;
        gachaMultiCostText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GEM;
        gachaSingleText.text = gachaPeriodsModel.single_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
        gachaMultiText.text = gachaPeriodsModel.multi_count.ToString() + GameUtility.Const.SHOW_GACHA_COUNT;
    }

    //単発
    public void SetGachaSingle()
    {
        var gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaPeriodTemplateView.GachaId);
        gachaCount = gachaPeriodsModel.single_count;
        gachaConfirmText.text = gachaPeriodsModel.single_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //連発
    public void SetGachaMulti()
    {
        var gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaPeriodTemplateView.GachaId);
        gachaCount = gachaPeriodsModel.multi_count;
        gachaConfirmText.text = gachaPeriodsModel.multi_cost.ToString() + GameUtility.Const.SHOW_GACHA_CONFIRM_TEXT;
    }

    //ガチャ実行確認画面開く
    public void SetGachaConfirmOpen()
    {
        gachaConfirmView.SetActive(true);
        clientGacha.WarningMessage("");
    }

    //ガチャ実行確認画面閉じる
    public void SetGachaConfirmClose()
    {
        gachaConfirmView.SetActive(false);
        clientGacha.WarningMessage("");
    }
}
