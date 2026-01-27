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

    public void Set(ItemDataModel data1, ItemRaritiesModel data2, PresentInstancesModel data3, string imagePath)
    {
        Image.sprite = Resources.Load<Sprite>(imagePath);
        nameText.text = data1.name;
        rarityText.text = data2.name;
        amountText.text = data3.amount.ToString() + GameUtility.Const.SHOW_AMOUNT + GameUtility.Const.SHOW_POSSESSION;
        descriptionText.text = data1.description;

        //受取期限から現在時刻の差分計算 (デバイス依存なのでサーバー時間で統一した方がいい)
        int day = (DateTime.Parse(data3.period) - DateTime.Now).Days;
        periodText.text = GameUtility.Const.SHOW_RECEIVED_PERIOD + day.ToString() + GameUtility.Const.SHOW_DAY;

        receivedTimeText.text = data3.updated_at.ToString();
    }

    //受取一覧と履歴で表示する内容を変更
    public void SetShowReceived(bool showSet1, bool showSet2)
    {
        presentInstanceSetNotReceived.SetActive(showSet1);
        presentInstanceSetReceived.SetActive(showSet2);
    }
}
