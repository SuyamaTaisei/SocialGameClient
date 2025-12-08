using UnityEngine;
using UnityEngine.UI;

public class GachaPeriodList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int index;

    private void Start()
    {
        for (int i = startCount; i <= maxCount; i++)
        {
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            int index = this.index + i;
            if (clientGacha) button.onClick.AddListener(() => clientGacha.ShowGachaPeriodList(index));
        }
    }
}