using UnityEngine;

public class GachaRewardList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GachaRewardTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] DataGetManager dataGetManager;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaRewardTemplateView gachaRewardTemplateView;
    [SerializeField] ButtonEffect buttonEffect;

    public Transform Content => content;

    public void DataList(GachaResultsModel[] totalExchangeItems)
    {
        //何もガチャ報酬がなければボタンを押せない
        if (totalExchangeItems == null || totalExchangeItems.Length == 0)
        {
            buttonEffect.ButtonTextOpacityEffect(false, clientGacha.GachaRewardOpenButton);
            return;
        }

        buttonEffect.ButtonTextOpacityEffect(true, clientGacha.GachaRewardOpenButton);

        //変換された個数分走査
        for (int i = 0; i < totalExchangeItems.Length; i++)
        {
            //データの生成
            var (view, _) = dataListManager.CreateDataListSync(templateView, content);

            //データの取得
            var data = totalExchangeItems[i];
            var (data1, data2, imagePath) = dataGetManager.GetItemData(data.item_id);

            //データの描画
            view.Set(view, data1, data2, data, imagePath);
        }
    }
}
