using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceMissionFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI missionInstanceCompleteText;

    [SerializeField] Button missionInstanceConfirmExecuteButton;
    [SerializeField] Button missionInstanceConfirmCancelButton;
    [SerializeField] Button missionInstanceCompleteCloseButton;
    [SerializeField] Button missionInstanceAllReceivedOpenButton;

    [SerializeField] GameObject missionInstanceConfirmView;
    [SerializeField] GameObject missionInstanceCompleteView;

    [SerializeField] ClientMission clientMission;
    [SerializeField] InstanceMissionConfirmList instanceMissionConfirmList;
    [SerializeField] ButtonEffect buttonEffect;

    private void Start()
    {
        SetConfirm(false);
        SetComplete(false);

        missionInstanceConfirmExecuteButton.onClick.AddListener(() => clientMission.RequestMissionReceived()); //ミッション受け取りリクエスト
        missionInstanceConfirmCancelButton.onClick.AddListener(() => SetConfirm(false));
        missionInstanceCompleteCloseButton.onClick.AddListener(() => SetComplete(false));

        //一括受取ボタン
        missionInstanceAllReceivedOpenButton.onClick.AddListener(() =>
        {
            //辞書データをクリア
            clientMission.ClearMission();

            //未受取ミッションインスタンス全件取得
            var allData = MissionInstancesTable.SelectAll(0, GameUtility.Const.LOG_PRESENT_LIMIT);

            //ミッション確認画面表示＆データセット
            foreach (var all in allData)
            {
                clientMission.SaveMission(all.mission_id, all.mission_category);
            }
            instanceMissionConfirmList.AllDataList(allData);
            SetConfirm(true);
        });
    }

    //一括受取ボタン押下制御
    public void SetCtrlAllReceivedButton(bool enabled = true)
    {
        var allData = MissionInstancesTable.SelectAll(0, GameUtility.Const.LOG_PRESENT_LIMIT);
        bool canPressed = allData.Count > 0;
        buttonEffect.ButtonTextOpacityEffect(canPressed, missionInstanceAllReceivedOpenButton);
    }

    //受け取り確認画面
    public void SetConfirm(bool enabled)
    {
        missionInstanceConfirmView.SetActive(enabled);
    }

    //受け取り完了画面
    public void SetComplete(bool enabled)
    {
        missionInstanceCompleteView.SetActive(enabled);
    }

    //完了画面文字変更
    public void SetCompleteText(string text)
    {
        missionInstanceCompleteText.text = text;
    }
}
