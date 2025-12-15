using UnityEngine;
using System.Collections.Generic;

public class GachaResultList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaResultTemplateView gachaResultTemplateView;

    public Transform Content => content;

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

            //データ実体の生成
            GameObject item = Instantiate(templateView, content);
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
                gachaResultTemplateView.SetColorChange(view, GameUtility.Const.GACHA_COLOR_NEW);
                gachaResultTemplateView.SetSingleGachaReward(isNew, view, singleExchangeItems, ref singleExchangeIndex);
            }
            //所持済み
            else
            {
                gachaResultTemplateView.SetColorChange(view, GameUtility.Const.GACHA_COLOR_EXIST);
                gachaResultTemplateView.SetSingleGachaReward(isNew, view, singleExchangeItems, ref singleExchangeIndex);
            }

            //データの取得
            var characterDataModel = CharacterDataTable.SelectId(gachaResult.character_id);
            var characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{gachaResult.character_id}";

            //データの描画
            gachaResultTemplateView.SetGachaResult(view, characterDataModel, characterRaritiesModel, imagePath);
        }
    }
}