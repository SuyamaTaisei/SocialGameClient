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

    public void ShowGachaReward(GachaResultsModel[] gachaReward)
    {
        //変換された個数分走査
        for (int i = 0; i < gachaReward.Length; i++)
        {
            var exhange = gachaReward[i];
        
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaRewardTempView>();

            //アイテムidが一致するデータを取得
            itemDataModel = ItemDataTable.SelectId(exhange.item_id);
            itemRaritiesModel = ItemRaritiesTable.SelectId(itemDataModel.rarity_id);
            string imagePath = $"Images/Items/{exhange.item_id}";

            //表記
            view.NameText.text = itemDataModel.name;
            view.RarityText.text = itemRaritiesModel.name;
            view.AmountText.text = exhange.amount.ToString();
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
