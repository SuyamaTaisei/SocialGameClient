using UnityEngine;
using System.Collections.Generic;

public class GachaOfferRateList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;

    private void Start()
    {
        List<GachaDataModel> gachaDataList = GachaDataTable.SelectAllGachaId(clientGacha.GachaId);

        //提供割合表示を切り替えるために、Updateで何かしらの条件をもとに随時更新する必要がある
        //ガチャ期間IDが一致するレコードのみで全件走査
        for (int i = 0; i < gachaDataList.Count; i++)
        {
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaOfferRateTempView>();
            if (clientGacha) clientGacha.ShowGachaOfferRateList(view, i);
        }
    }
}
