using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GachaPeriodList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;

    private void Start()
    {
        List<GachaPeriodsModel> gachaPeriodsList = GachaPeriodsTable.SelectAll();

        for (int i = 0; i < gachaPeriodsList.Count; i++)
        {
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            int index = i;

            clientGacha.ShowGachaPeriodList(index);
            if (clientGacha) button.onClick.AddListener(() => clientGacha.ShowGachaPeriodList(index));
        }
    }
}