using System.Collections.Generic;
using UnityEngine;

public class GachaLogList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaLogTemplateView gachaLogTemplateView;

    //ガチャ履歴ボタン押下で、履歴リスト更新とリセット
    private void OnEnable() => Refresh();
    private void OnDisable() => Clear();

    //開いて更新
    private void Refresh()
    {
        List<GachaLogsModel> gachaLogsList = GachaLogsTable.SelectLatest(GameUtility.Const.LOG_GACHA_LIMIT);

        //何もガチャ履歴が無ければ
        if (gachaLogsList.Count == 0)
        {
            clientGacha.NothingGachaLogMessage(GameUtility.Const.SHOW_GACHA_LOG_NOTHING);
            return;
        }

        clientGacha.NothingGachaLogMessage("");

        for (int i = 0; i < gachaLogsList.Count; i++)
        {
            //データ実体の生成
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaLogTemplateView>();
            int index = i;

            //データの取得
            var characterDataModel = CharacterDataTable.SelectId(gachaLogsList[index].character_id);
            var characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
            var gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaLogsList[index].gacha_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{gachaLogsList[index].character_id}";

            //データの描画
            gachaLogTemplateView.Set(view, characterDataModel, characterRaritiesModel, gachaLogsList[i], gachaPeriodsModel, imagePath);
        }
    }

    //閉じてリセット
    private void Clear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
