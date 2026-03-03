using System.Collections.Generic;
using UnityEngine;

public class GachaLogList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] DataGetManager dataGetManager;
    [SerializeField] ClientGacha clientGacha;
    [SerializeField] GachaLogTemplateView gachaLogTemplateView;

    //ガチャ履歴ボタン押下で、履歴リスト更新とリセット
    private void OnEnable() => DataList();
    private void OnDisable() => Clear();

    //開いて更新
    private void DataList()
    {
        List<GachaLogsModel> gachaLogsList = GachaLogsTable.SelectIdLatest(GameUtility.Const.LOG_GACHA_LIMIT);

        //何もガチャ履歴が無ければ
        if (gachaLogsList.Count == 0)
        {
            clientGacha.GachaLogMessage(GameUtility.Const.SHOW_GACHA_LOG_NOTHING);
            return;
        }

        clientGacha.GachaLogMessage("");

        for (int i = 0; i < gachaLogsList.Count; i++)
        {
            //データの生成
            var item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaLogTemplateView>();

            //データの取得
            int index = i;
            var (data1, data2, imagePath) = dataGetManager.GetCharacterData(gachaLogsList[index].character_id);
            var data3 = dataGetManager.GetGachaPeriodData(gachaLogsList[index].gacha_id);

            //データの描画
            view.Set(view, data1, data2, gachaLogsList[i], data3, imagePath);
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
