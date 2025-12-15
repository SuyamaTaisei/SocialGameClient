using UnityEngine;
using System.Collections.Generic;

public class GachaResultList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaResultTemplateView gachaResultTemplateView;

    public Transform Content => content;

    //DBモデル
    private CharacterDataModel characterDataModel;
    private CharacterRaritiesModel characterRaritiesModel;

    //ガチャ結果表示処理
    public void ShowGachaResult(GachaResultsModel[] gachaResults, GachaResultsModel[] newGachaResults, GachaResultsModel[] singleExchangeItems)
    {
        clientGacha.GachaResultView.SetActive(true);

        //新規キャラクターIDの重複防止
        HashSet<int> existCharacterId = new HashSet<int>();

        int singleExchangeIndex = 0;

        for (int i = 0; i < clientGacha.GachaCount; i++)
        {
            //ガチャ回数分全てを取得
            var gachaResult = gachaResults[i];

            //雛形をもとにリストを生成
            GameObject item = Instantiate(templateView, content);
            //ガチャ用viewの取得
            var view = item.GetComponent<GachaResultTemplateView>();

            //ガチャ回数分の内、新規で出たキャラクターIDのみ
            bool isNew = false;
            for (int j = 0; j < newGachaResults.Length; j++)
            {
                //新規キャラのみで走査し、ガチャ結果と一致かつHashSetのインスタンスに含まれていなければ新規追加
                if (gachaResult.character_id == newGachaResults[j].character_id && !existCharacterId.Contains(gachaResult.character_id))
                {
                    isNew = true;
                    existCharacterId.Add(gachaResult.character_id);
                    break;
                }
            }

            //新規入手
            if (isNew)
            {
                clientGacha.GachaResultColorChangeEffect(view, GameUtility.Const.GACHA_COLOR_NEW);
                gachaResultTemplateView.SetSingleGachaReward(isNew, view, singleExchangeItems, ref singleExchangeIndex);
            }
            //所持済み
            else
            {
                clientGacha.GachaResultColorChangeEffect(view, GameUtility.Const.GACHA_COLOR_EXIST);
                gachaResultTemplateView.SetSingleGachaReward(isNew, view, singleExchangeItems, ref singleExchangeIndex);
            }

            //idが一致するデータを取得
            characterDataModel = CharacterDataTable.SelectId(gachaResult.character_id);
            characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{gachaResult.character_id}";

            //表記
            view.CharacterNameText.text = characterDataModel.name;
            view.CharacterRarityText.text = characterRaritiesModel.name;
            Sprite sprite = Resources.Load<Sprite>(imagePath);

            //画像設定
            if (view != null)
            {
                view.CharacterImage.sprite = sprite;
                view.CharacterImage.preserveAspect = true;
            }
        }
    }
}