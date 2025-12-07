using UnityEngine;

public class GachaOfferRateList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int index;

    private void Start()
    {
        //提供割合表示を切り替えるために、Updateで何かしらの条件をもとに随時更新する必要がある
        for (int i = startCount; i <= maxCount; i++)
        {
            GameObject item = Instantiate(templateView, content);
            int index = this.index + i;
            var view = item.GetComponent<GachaOfferRateTempView>();
            if (clientGacha) clientGacha.ShowGachaOfferRateList(view, index);
        }
    }
}
