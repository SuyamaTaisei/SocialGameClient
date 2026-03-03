using System.Collections.Generic;
using UnityEngine;

public class GachaPickUpList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
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
            var item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaPickUpTemplateView>();
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