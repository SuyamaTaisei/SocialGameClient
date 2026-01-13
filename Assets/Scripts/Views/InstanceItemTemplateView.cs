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

    [SerializeField] InstanceItemDetailFixedView itemDetailFixedView;

    private void Start()
    {
        itemInstanceDetailOpenButton.onClick.AddListener(() => itemDetailFixedView.SetItemInstanceDetail(true));
    }

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (itemImage) itemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (itemInstanceNameText) itemInstanceNameText.text = data1.name;
        if (itemInstanceRarityText) itemInstanceRarityText.text = data2.name;
        if (itemInstanceAmountText) itemInstanceAmountText.text = data3.amount + "/" + GameUtility.Const.SHOW_INSTANCE_AMOUNT_MAX;
    }
}
