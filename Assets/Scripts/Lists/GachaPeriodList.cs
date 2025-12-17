using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GachaPeriodList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    private void Start()
    {
        List<GachaPeriodsModel> gachaPeriodsList = GachaPeriodsTable.SelectAll();

        for (int i = 0; i < gachaPeriodsList.Count; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();

            //データの取得
            int index = i;

            //データの描画
            gachaPeriodTemplateView.SetList(index);
            button.onClick.AddListener(() => gachaPeriodTemplateView.SetList(index));
        }
    }
}