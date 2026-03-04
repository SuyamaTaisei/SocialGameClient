using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GachaPeriodList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GachaPeriodTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    private void Start()
    {
        List<GachaPeriodsModel> gachaPeriodsList = GachaPeriodsTable.SelectAll();

        for (int i = 0; i < gachaPeriodsList.Count; i++)
        {
            //データの生成
            var (_, button) = dataListManager.CreateDataListSync(templateView, content, false, true);

            //データの取得
            int index = i;

            //データの描画
            gachaPeriodTemplateView.Set(index);
            button.onClick.AddListener(() => gachaPeriodTemplateView.Set(index));
        }
    }
}