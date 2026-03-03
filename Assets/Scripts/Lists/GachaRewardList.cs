using UnityEngine;

public class GachaRewardList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
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
            var item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaRewardTemplateView>();

            //データの取得
            var data = totalExchangeItems[i];
            var data1 = ItemDataTable.SelectId(data.item_id);
            var data2 = ItemRaritiesTable.SelectId(data1.rarity_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{data.item_id}";

            //データの描画
            view.Set(view, data1, data2, data, imagePath);
        }
    }
}
