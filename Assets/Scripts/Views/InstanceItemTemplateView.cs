using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceItemTemplateView : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemInstanceNameText;
    [SerializeField] TextMeshProUGUI itemInstanceRarityText;
    [SerializeField] TextMeshProUGUI itemInstanceAmountText;
    [SerializeField] Button itemInstanceDetailOpenButton;
    [SerializeField] Button itemDetailCloseButton;
    [SerializeField] GameObject itemInstanceDetailFixedView;

    private void Start()
    {
        itemInstanceDetailFixedView.SetActive(false);

        itemInstanceDetailOpenButton.onClick.AddListener(() => SetItemInstanceDetail(true));
        itemDetailCloseButton.onClick.AddListener(() => SetItemInstanceDetail(false));
    }

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (itemImage) itemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (itemInstanceNameText) itemInstanceNameText.text = data1.name;
        if (itemInstanceRarityText) itemInstanceRarityText.text = data2.name;
        if (itemInstanceAmountText) itemInstanceAmountText.text = data3.amount + "/" + GameUtility.Const.SHOW_INSTANCE_AMOUNT_MAX;
    }

    //アイテム詳細画面開閉
    public void SetItemInstanceDetail(bool enabled)
    {
        itemInstanceDetailFixedView.SetActive(enabled);
    }
}
