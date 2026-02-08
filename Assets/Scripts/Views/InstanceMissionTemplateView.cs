using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceMissionTemplateView : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] Image progressGauge;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI receivedText;
    [SerializeField] TextMeshProUGUI progressValueText;
    [SerializeField] Button receivedButton;
    [SerializeField] ButtonEffect buttonEffect;

    public void Set(MissionDataModel data, MissionInstancesModel data1, string imagePath)
    {
        if (Image)
        {
            Image.sprite = Resources.Load<Sprite>(imagePath);
        }
        if (nameText)
        {
            switch (data.reward_category)
            {
                case 1001: nameText.text = data.reward_value + GameUtility.Const.SHOW_GEM; break;
                case 1002: nameText.text = data.reward_value + GameUtility.Const.SHOW_COIN; break;
            }
        }
        if (descriptionText)
        {
            descriptionText.text = data.description;
        }

        int progressValue = data1 != null ? data1.progress : 0;
        int received = data1 != null ? data1.received : 0;
        if (progressGauge)
        {
            progressGauge.fillAmount = (float)progressValue / data.goal;
        }
        if (progressValueText)
        {
            progressValueText.text = progressValue.ToString() + "/" + data.goal.ToString();
        }

        if (receivedText && receivedButton)
        {
            //各ミッションが未受取 かつ 進捗が目標値以下の場合
            if (received == 0 && progressValue < data.goal)
            {
                receivedText.text = GameUtility.Const.SHOW_MISSION_RECEIVE;
                receivedButton.interactable = false;
                buttonEffect.ButtonTextOpacityEffect(false, receivedButton);
            }
            else if (received == 0 && progressValue >= data.goal)
            {
                receivedText.text = GameUtility.Const.SHOW_MISSION_RECEIVE;
                receivedButton.interactable = true;
                buttonEffect.ButtonTextOpacityEffect(true, receivedButton);
            }
            else if (received == 1 && progressValue >= data.goal)
            {
                receivedText.text = GameUtility.Const.SHOW_MISSION_RECEIVED;
                receivedButton.interactable = false;
                buttonEffect.ButtonTextOpacityEffect(false, receivedButton);
            }
        }
    }
}
