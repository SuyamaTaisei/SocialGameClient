using UnityEngine;
using System.Collections.Generic;

public class GachaResultList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GachaResultTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] DataGetManager dataGetManager;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaFixedView gachaFixedView;
    [SerializeField] GachaResultTemplateView gachaResultTemplateView;
    [SerializeField] GachaRewardList gachaRewardList;

    private void OnDisable() => Clear();

    //ガチャ結果表示処理
    public void DataList(GachaResultsModel[] gachaResults, GachaResultsModel[] newGachaResults, GachaResultsModel[] singleExchangeItems)
    {
        clientGacha.GachaResultView.SetActive(true);

        //新規キャラクターIDの重複防止
        HashSet<int> existCharacterId = new HashSet<int>();

        int singleExchangeIndex = 0;

        for (int i = 0; i < gachaFixedView.GachaCount; i++)
        {
            //ガチャ回数分全てを取得
            var gachaResult = gachaResults[i];

            //データの生成
            var (view, _) = dataListManager.CreateDataListSync(templateView, content);

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
                view.SetColorChange(view, GameUtility.Const.GACHA_COLOR_NEW);
                view.SetSingleGachaReward(isNew, view, singleExchangeItems, ref singleExchangeIndex);
            }
            //所持済み
            else
            {
                view.SetColorChange(view, GameUtility.Const.GACHA_COLOR_EXIST);
                view.SetSingleGachaReward(isNew, view, singleExchangeItems, ref singleExchangeIndex);
            }

            //データの取得
            var (data1, data2, imagePath) = dataGetManager.GetCharacterData(gachaResult.character_id);

            //データの描画
            view.SetGachaResult(view, data1, data2, imagePath);
        }
    }

    //閉じてリセット
    private void Clear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in gachaRewardList.Content)
        {
            Destroy(child.gameObject);
        }
    }
}