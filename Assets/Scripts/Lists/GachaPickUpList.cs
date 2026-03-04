using System.Collections.Generic;
using UnityEngine;

public class GachaPickUpList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GachaPickUpTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;
    [SerializeField] DataGetManager dataGetManager;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int pickUpNumber;

    private void Start()
    {
        //データの生成
        for (int i = startCount; i <= maxCount; i++)
        {
            var (view, _) = dataListManager.CreateDataListSync(templateView, content);
            int index = pickUpNumber + i;

            List<GachaDataModel> gachaDataModel = GachaDataTable.SelectAllGachaId(gachaPeriodTemplateView.GachaId);

            foreach (var list in gachaDataModel)
            {
                if (list.character_id == index)
                {
                    //データの取得
                    var (data1, data2, imagePath) = dataGetManager.GetCharacterData(index);

                    //データの描画
                    view.Set(data1, data2, imagePath);
                }
            }
        }
    }
}