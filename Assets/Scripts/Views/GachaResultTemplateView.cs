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

    //ガチャ結果表記
    public void SetGachaResult(GachaResultTemplateView view, CharacterDataModel data1, CharacterRaritiesModel data2, string imagePath)
    {
        view.characterNameText.text = data1.name;
        view.characterRarityText.text = data2.name;
        view.characterImage.sprite = Resources.Load<Sprite>(imagePath);
        view.characterImage.preserveAspect = true;
    }

    //ガチャ単一報酬表記
    public void SetSingleGachaReward(bool isNew, GachaResultTemplateView view, GachaResultsModel[] singleExchangeItems, ref int singleExchangeIndex)
    {
        //新規ガチャならガチャ報酬は空文字、既存ガチャで被ればガチャ報酬表示
        view.characterNewText.text = isNew ? GameUtility.Const.SHOW_GACHA_NEW : string.Empty;

        view.itemNameText.text = string.Empty;
        view.itemRarityText.text = string.Empty;
        view.itemAmountText.text = string.Empty;
        view.itemOtherObject.SetActive(false);
        view.itemImage.gameObject.SetActive(false);

        if (isNew)
        {
            return;
        }

        //ガチャが被った時だけ、1要素ずつガチャ報酬(変換したアイテム)を表示
        var exchange = singleExchangeItems[singleExchangeIndex];

        //次の要素のガチャ報酬用にインクリメント
        singleExchangeIndex++;

        //データの取得
        var itemDataModel = ItemDataTable.SelectId(exchange.item_id);
        var itemRaritiesModel = ItemRaritiesTable.SelectId(itemDataModel.rarity_id);
        string itemImagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{exchange.item_id}";

        //表記
        view.itemNameText.text = itemDataModel.name;
        view.itemRarityText.text = itemRaritiesModel.name;
        view.itemAmountText.text = exchange.amount.ToString();
        view.itemOtherObject.SetActive(true);
        view.itemImage.gameObject.SetActive(true);
        view.itemImage.sprite = Resources.Load<Sprite>(itemImagePath);
        view.itemImage.preserveAspect = true;
    }

    //新規と所持済みで透明度を変更
    public void SetColorChange(GachaResultTemplateView view, float value)
    {
        var color = view.characterImage.color;
        color.a = value;
        view.characterImage.color = color;
    }
}
