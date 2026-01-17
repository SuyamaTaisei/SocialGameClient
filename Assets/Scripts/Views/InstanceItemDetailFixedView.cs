using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceItemDetailFixedView : MonoBehaviour
{
    [SerializeField] Image itemDetailImage;
    [SerializeField] TextMeshProUGUI itemDetailNameText;
    [SerializeField] TextMeshProUGUI itemDetailRarityText;
    [SerializeField] TextMeshProUGUI itemDetailDescriptionText;
    [SerializeField] TextMeshProUGUI itemDetailAmountText;
    [SerializeField] Button itemDetailCloseButton;
    [SerializeField] GameObject itemInstanceDetailFixedView;

    private void Start()
    {
        itemInstanceDetailFixedView.SetActive(false);
        itemDetailCloseButton.onClick.AddListener(() => SetItemInstanceDetail(false));
    }

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (itemDetailImage)
        {
            itemDetailImage.sprite = Resources.Load<Sprite>(imagePath);
        }
        if (itemDetailNameText)
        {
            itemDetailNameText.text = data1.name;
        }
        if (itemDetailRarityText)
        {
            itemDetailRarityText.text = data2.name;
        }
        if (itemDetailDescriptionText)
        {
            itemDetailDescriptionText.text = data1.description;
        }
        if (itemDetailAmountText)
        {
            itemDetailAmountText.text = data3.amount + GameUtility.Const.SHOW_AMOUNT + GameUtility.Const.SHOW_POSSESSION;
        }
    }

    //アイテム詳細画面開閉
    public void SetItemInstanceDetail(bool enabled)
    {
        itemInstanceDetailFixedView.SetActive(enabled);
    }
}
