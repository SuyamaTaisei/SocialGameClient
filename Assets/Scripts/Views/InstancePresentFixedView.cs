using UnityEngine;
using UnityEngine.UI;

public class InstancePresentFixedView : MonoBehaviour
{
    [SerializeField] Button presentInstanceCommonOpenButton;
    [SerializeField] Button presentInstancePersonalOpenButton;
    [SerializeField] Button presentInstanceLogOpenButton;

    [SerializeField] Button presentInstanceConfirmExecuteButton;
    [SerializeField] Button presentInstanceConfirmCancelButton;
    [SerializeField] Button presentInstanceCompleteCloseButton;

    [SerializeField] Button presentInstanceAllReceivedOpenButton;

    [SerializeField] GameObject presentInstanceCommonList;
    [SerializeField] GameObject presentInstancePersonalList;
    [SerializeField] GameObject presentInstanceLogList;

    [SerializeField] GameObject presentInstanceConfirmView;
    [SerializeField] GameObject presentInstanceCompleteView;

    [SerializeField] ClientPresent clientPresent;
    [SerializeField] InstancePresentConfirmList instancePresentConfirmList;

    private void Start()
    {
        Set(false, true, false);
        SetConfirm(false);        
        SetComplete(false);

        presentInstanceCommonOpenButton.onClick.AddListener(()   => Set(true, false, false));
        presentInstancePersonalOpenButton.onClick.AddListener(() => Set(false, true, false));
        presentInstanceLogOpenButton.onClick.AddListener(()      => Set(false, false, true));

        presentInstanceConfirmExecuteButton.onClick.AddListener(() => clientPresent.RequestPresentReceived()); //プレゼント受け取りリクエスト
        presentInstanceConfirmCancelButton.onClick.AddListener(() => SetConfirm(false));
        presentInstanceCompleteCloseButton.onClick.AddListener(() => SetComplete(false));

        //一括受取ボタン
        presentInstanceAllReceivedOpenButton.onClick.AddListener(() =>
        {
            //辞書データをクリア
            clientPresent.ClearPresent();

            //未受取プレゼントインスタンス全件取得
            var allData = PresentInstancesTable.SelectAll(0, GameUtility.Const.LOG_PRESENT_LIMIT);

            //プレゼント確認画面表示＆データセット
            foreach (var all in allData)
            {
                clientPresent.SavePresent(all.id, all.present_category, all.content, all.amount);
            }
            instancePresentConfirmList.AllDataList(allData);
            SetConfirm(true);
        });
    }

    //項目表示の切り替え
    public void Set(bool common, bool personal, bool log)
    {
        presentInstanceCommonList.SetActive(common);
        presentInstancePersonalList.SetActive(personal);
        presentInstanceLogList.SetActive(log);
        SetCtrlAllReceivedButton(); //一括受取ボタン押下制御
    }

    //一括受取ボタン押下制御
    public void SetCtrlAllReceivedButton()
    {
        var allData = PresentInstancesTable.SelectAll(0, GameUtility.Const.LOG_PRESENT_LIMIT);
        presentInstanceAllReceivedOpenButton.interactable = allData.Count > 0;
    }

    //受け取り確認画面
    public void SetConfirm(bool enabled)
    {
        presentInstanceConfirmView.SetActive(enabled);
    }

    //受け取り完了画面
    public void SetComplete(bool enabled)
    {
        presentInstanceCompleteView.SetActive(enabled);
    }
}
