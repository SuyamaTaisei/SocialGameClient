using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceItemTemplateView : MonoBehaviour
{
    [SerializeField] Image enhanceItemImage;
    [SerializeField] TextMeshProUGUI enhanceItemNameText;
    [SerializeField] TextMeshProUGUI enhanceItemRarityText;
    [SerializeField] TextMeshProUGUI totalAmountText;
    [SerializeField] TextMeshProUGUI currentAmountText;
    [SerializeField] Button increaseButton;
    [SerializeField] Button decreaseButton;

    [SerializeField] ClientInstance clientInstance;
    [SerializeField] InstanceCharacterDetailFixedView characterDetailView;

    private int amountValue;
    private const int minAmount = 0;

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (enhanceItemImage) enhanceItemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (enhanceItemNameText) enhanceItemNameText.text = data1.name;
        if (enhanceItemRarityText) enhanceItemRarityText.text = data2.name;
        if (totalAmountText) totalAmountText.text = data3.amount.ToString();

        //二重登録防止
        increaseButton.onClick.RemoveAllListeners();
        decreaseButton.onClick.RemoveAllListeners();

        //強化アイテム数、レベルアップ後の増減処理、選択アイテムIDと数量の保持
        increaseButton.onClick.AddListener(() => {
            SetAmount(amountValue + GameUtility.Const.SHOP_AMOUNT_MIN, data3.amount);
            characterDetailView.SetAddAfterLevel(+data1.value);
            clientInstance.SetEnhanceItems(data3.item_id, amountValue);
        });
        decreaseButton.onClick.AddListener(() => {
            SetAmount(amountValue - GameUtility.Const.SHOP_AMOUNT_MIN, data3.amount);
            characterDetailView.SetAddAfterLevel(-data1.value);
            clientInstance.SetEnhanceItems(data3.item_id, amountValue);
        });

        //開くたびにリセット
        amountValue = minAmount;
        SetAmount(minAmount, data3.amount);
        clientInstance.SetEnhanceItems(data3.item_id, amountValue);
    }

    //強化アイテム数の増減制御
    private void SetAmount(int currentValue, int maxAmount)
    {
        amountValue = Mathf.Clamp(currentValue, minAmount, maxAmount);
        currentAmountText.text = amountValue.ToString();
        increaseButton.interactable = amountValue < maxAmount;
        decreaseButton.interactable = amountValue > minAmount;
    }
}
