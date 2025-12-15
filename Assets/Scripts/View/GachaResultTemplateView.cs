using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GachaResultTemplateView : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI characterRarityText;
    [SerializeField] TextMeshProUGUI characterNewText;

    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemRarityText;
    [SerializeField] TextMeshProUGUI itemAmountText;
    [SerializeField] GameObject itemOtherObject;

    public Image CharacterImage => characterImage;
    public TextMeshProUGUI CharacterNameText => characterNameText;
    public TextMeshProUGUI CharacterRarityText => characterRarityText;
    public TextMeshProUGUI CharacterNewText => characterNewText;

    public Image ItemImage => itemImage;
    public TextMeshProUGUI ItemNameText => itemNameText;
    public TextMeshProUGUI ItemRarityText => itemRarityText;
    public TextMeshProUGUI ItemAmountText => itemAmountText;
    public GameObject ItemOtherObject => itemOtherObject;

    //ガチャ単一報酬表記
    public void SetSingleGachaReward(bool isNew, GachaResultTemplateView view, GachaResultsModel[] singleExchangeItems, ref int singleExchangeIndex)
    {
        //ガチャ報酬配列の有効範囲内のみ
        if (singleExchangeItems != null && singleExchangeIndex < singleExchangeItems.Length)
        {
            //新規ガチャならガチャ報酬は空文字、既存ガチャで被ればガチャ報酬表示
            view.CharacterNewText.text = isNew ? GameUtility.Const.SHOW_GACHA_NEW : string.Empty;

            view.ItemNameText.text = string.Empty;
            view.ItemRarityText.text = string.Empty;
            view.ItemAmountText.text = string.Empty;
            view.ItemOtherObject.SetActive(false);
            view.ItemImage.gameObject.SetActive(false);

            if (isNew) return;

            //ガチャが被った時だけ、1要素ずつガチャ報酬(変換したアイテム)を表示
            var exchange = singleExchangeItems[singleExchangeIndex];

            //次の要素のガチャ報酬用にインクリメント
            singleExchangeIndex++;

            //データを取得
            var itemDataModel = ItemDataTable.SelectId(exchange.item_id);
            var itemRaritiesModel = ItemRaritiesTable.SelectId(itemDataModel.rarity_id);
            string itemImagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{exchange.item_id}";

            //表記
            view.ItemNameText.text = itemDataModel.name;
            view.ItemRarityText.text = itemRaritiesModel.name;
            view.ItemAmountText.text = exchange.amount.ToString();
            view.ItemOtherObject.SetActive(true);
            view.ItemImage.gameObject.SetActive(true);
            view.ItemImage.sprite = Resources.Load<Sprite>(itemImagePath);
            view.ItemImage.preserveAspect = true;
        }
    }
}
