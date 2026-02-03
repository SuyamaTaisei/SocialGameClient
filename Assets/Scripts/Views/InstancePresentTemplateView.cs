using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstancePresentTemplateView : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI periodText;
    [SerializeField] TextMeshProUGUI receivedTimeText;

    [SerializeField] GameObject presentInstanceSetNotReceived;
    [SerializeField] GameObject presentInstanceSetReceived;

    [SerializeField] bool isShowText;

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, PresentInstancesModel data3, string imagePath)
    {
        if (Image)
        {
            Image.sprite = Resources.Load<Sprite>(imagePath);
        }
        if (nameText)
        {
            nameText.text = data1.name;
        }
        if (rarityText)
        {
            rarityText.text = data2.name;
        }
        if (amountText)
        {
            string text = data3.amount.ToString() + GameUtility.Const.SHOW_AMOUNT;
            amountText.text = isShowText ? text + GameUtility.Const.SHOW_POSSESSION : text;
        }
        if (descriptionText)
        {
            descriptionText.text = data1.description;
        }
        if (periodText)
        {
            int day = (DateTime.Parse(data3.period) - DateTime.Now).Days;
            periodText.text = GameUtility.Const.SHOW_RECEIVED_PERIOD + day.ToString() + GameUtility.Const.SHOW_DAY;
        }
        if (receivedTimeText)
        {
            receivedTimeText.text = data3.updated_at.ToString();
        }
    }

    //受取一覧と履歴で表示する内容を変更
    public void SetShowReceived(bool showSet1, bool showSet2)
    {
        if (presentInstanceSetNotReceived)
        {
            presentInstanceSetNotReceived.SetActive(showSet1);
        }
        if (presentInstanceSetReceived)
        {
            presentInstanceSetReceived.SetActive(showSet2);
        }
    }
}
