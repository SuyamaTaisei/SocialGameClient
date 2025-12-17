using UnityEngine;

public class GachaRewardList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaRewardTemplateView gachaRewardTemplateView;

    public Transform Content => content;

    public void ShowGachaReward(GachaResultsModel[] totalExchangeItems)
    {
        //変換された個数分走査
        for (int i = 0; i < totalExchangeItems.Length; i++)
        {
            //何もガチャ報酬がなければ警告メッセージ表示
            if (totalExchangeItems.Length < 0) clientGacha.NothingMessage(GameUtility.Const.SHOW_GACHA_REWARD_NOTHING);
            else { clientGacha.NothingMessage(""); }

            //データ実体の生成
            var exchange = totalExchangeItems[i];
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaRewardTemplateView>();

            //データの取得
            var itemDataModel = ItemDataTable.SelectId(exchange.item_id);
            var itemRaritiesModel = ItemRaritiesTable.SelectId(itemDataModel.rarity_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{exchange.item_id}";

            //データの描画
            gachaRewardTemplateView.Set(view, itemDataModel, itemRaritiesModel, exchange, imagePath);
        }
    }
}
