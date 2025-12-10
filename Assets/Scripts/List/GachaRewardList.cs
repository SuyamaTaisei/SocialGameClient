using UnityEngine;

public class GachaRewardList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;

    public Transform Content => content;

    //DBモデル
    ItemDataModel itemDataModel;
    ItemRaritiesModel itemRaritiesModel;

    public void ShowGachaReward(GachaResultsModel[] totalExchangeItems)
    {
        //変換された個数分走査
        for (int i = 0; i < totalExchangeItems.Length; i++)
        {
            //何もガチャ報酬がなければ警告メッセージ表示
            if (totalExchangeItems.Length < 0) clientGacha.NothingMessage(GameUtility.Const.SHOW_GACHA_REWARD_NOTHING);
            else { clientGacha.NothingMessage(""); }

            var exchange = totalExchangeItems[i];
        
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaRewardTempView>();

            //アイテムidが一致するデータを取得
            itemDataModel = ItemDataTable.SelectId(exchange.item_id);
            itemRaritiesModel = ItemRaritiesTable.SelectId(itemDataModel.rarity_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{exchange.item_id}";

            //表記
            view.NameText.text = itemDataModel.name;
            view.RarityText.text = itemRaritiesModel.name;
            view.AmountText.text = exchange.amount.ToString();
            Sprite sprite = Resources.Load<Sprite>(imagePath);

            //画像設定
            if (view != null)
            {
                view.ItemImage.sprite = sprite;
                view.ItemImage.preserveAspect = true;
            }
        }
    }
}
