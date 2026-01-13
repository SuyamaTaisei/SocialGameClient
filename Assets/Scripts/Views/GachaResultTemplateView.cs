using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GachaResultTemplateView : MonoBehaviour
{
    [SerializeField] Image gachaResultImage;
    [SerializeField] TextMeshProUGUI gachaResultNameText;
    [SerializeField] TextMeshProUGUI gachaResultRarityText;
    [SerializeField] TextMeshProUGUI gachaResultNewText;

    [SerializeField] Image gachaSingleRewardImage;
    [SerializeField] TextMeshProUGUI gachaSingleRewardNameText;
    [SerializeField] TextMeshProUGUI gachaSingleRewardRarityText;
    [SerializeField] TextMeshProUGUI gachaSingleRewardAmountText;
    [SerializeField] GameObject gachaSingleRewardOtherObject;

    //ガチャ結果表記
    public void SetGachaResult(GachaResultTemplateView view, CharacterDataModel data1, CharacterRaritiesModel data2, string imagePath)
    {
        view.gachaResultNameText.text = data1.name;
        view.gachaResultRarityText.text = data2.name;
        view.gachaResultImage.sprite = Resources.Load<Sprite>(imagePath);
        view.gachaResultImage.preserveAspect = true;
    }

    //ガチャ単一報酬表記
    public void SetSingleGachaReward(bool isNew, GachaResultTemplateView view, GachaResultsModel[] singleExchangeItems, ref int singleExchangeIndex)
    {
        //新規ガチャならガチャ報酬は空文字、既存ガチャで被ればガチャ報酬表示
        view.gachaResultNewText.text = isNew ? GameUtility.Const.SHOW_GACHA_NEW : string.Empty;

        view.gachaSingleRewardNameText.text = string.Empty;
        view.gachaSingleRewardRarityText.text = string.Empty;
        view.gachaSingleRewardAmountText.text = string.Empty;
        view.gachaSingleRewardOtherObject.SetActive(false);
        view.gachaSingleRewardImage.gameObject.SetActive(false);

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
        view.gachaSingleRewardNameText.text = itemDataModel.name;
        view.gachaSingleRewardRarityText.text = itemRaritiesModel.name;
        view.gachaSingleRewardAmountText.text = exchange.amount.ToString();
        view.gachaSingleRewardOtherObject.SetActive(true);
        view.gachaSingleRewardImage.gameObject.SetActive(true);
        view.gachaSingleRewardImage.sprite = Resources.Load<Sprite>(itemImagePath);
        view.gachaSingleRewardImage.preserveAspect = true;
    }

    //新規と所持済みで透明度を変更
    public void SetColorChange(GachaResultTemplateView view, float value)
    {
        var color = view.gachaResultImage.color;
        color.a = value;
        view.gachaResultImage.color = color;
    }
}
