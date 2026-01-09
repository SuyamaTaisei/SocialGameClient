using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceItemTemplateView : MonoBehaviour
{
    [SerializeField] Image enhanceItemImage;
    [SerializeField] TextMeshProUGUI enhanceItemNameText;
    [SerializeField] TextMeshProUGUI enhanceItemRarityText;
    [SerializeField] TextMeshProUGUI enhanceItemTotalAmountText;
    [SerializeField] TextMeshProUGUI enhanceItemCurrentAmountText;
    [SerializeField] Button enhanceItemIncreaseButton;
    [SerializeField] Button enhanceItemDecreaseButton;

    [SerializeField] ClientInstance clientInstance;
    [SerializeField] InstanceCharacterDetailFixedView charaInstanceDetailFixedView;

    private int amountValue;
    private const int minAmount = 0;

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, ItemInstancesModel data3, string imagePath)
    {
        if (enhanceItemImage) enhanceItemImage.sprite = Resources.Load<Sprite>(imagePath);
        if (enhanceItemNameText) enhanceItemNameText.text = data1.name;
        if (enhanceItemRarityText) enhanceItemRarityText.text = data2.name;
        if (enhanceItemTotalAmountText) enhanceItemTotalAmountText.text = data3.amount.ToString();

        //二重登録防止
        enhanceItemIncreaseButton.onClick.RemoveAllListeners();
        enhanceItemDecreaseButton.onClick.RemoveAllListeners();

        //強化アイテム数、レベルアップ後の増減処理、選択アイテムIDと数量の保持
        enhanceItemIncreaseButton.onClick.AddListener(() => {
            SetAmount(amountValue + GameUtility.Const.SHOP_AMOUNT_MIN, data3.amount);
            charaInstanceDetailFixedView.SetAddAfterLevel(+data1.value);
            clientInstance.SaveEnhanceItems(data3.item_id, amountValue);
        });
        enhanceItemDecreaseButton.onClick.AddListener(() => {
            SetAmount(amountValue - GameUtility.Const.SHOP_AMOUNT_MIN, data3.amount);
            charaInstanceDetailFixedView.SetAddAfterLevel(-data1.value);
            clientInstance.SaveEnhanceItems(data3.item_id, amountValue);
        });

        //開くたびにリセット
        amountValue = minAmount;
        SetAmount(minAmount, data3.amount);
        clientInstance.SaveEnhanceItems(data3.item_id, amountValue);
    }

    //強化アイテム数の増減制御
    private void SetAmount(int currentValue, int maxAmount)
    {
        amountValue = Mathf.Clamp(currentValue, minAmount, maxAmount);
        enhanceItemCurrentAmountText.text = amountValue.ToString();
        enhanceItemIncreaseButton.interactable = amountValue < maxAmount;
        enhanceItemDecreaseButton.interactable = amountValue > minAmount;
    }
}
