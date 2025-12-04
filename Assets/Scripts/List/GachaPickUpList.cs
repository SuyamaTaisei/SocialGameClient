using UnityEngine;

public class GachaPickUpList : MonoBehaviour
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
            int index = this.index + i;
            var view = item.GetComponent<GachaPickUpTempView>();
            if (clientGacha) clientGacha.ShowGachaUI(view, index);
        }
    }
}