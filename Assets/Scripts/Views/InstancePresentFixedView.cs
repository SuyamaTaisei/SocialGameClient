using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstancePresentFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI presentInstanceCompleteText;

    [SerializeField] Toggle[] presentTabList;

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
    [SerializeField] ButtonEffect buttonEffect;

    private void Start()
    {
        Set(false, true, false);
        SetConfirm(false);        
        SetComplete(false);

        presentTabList[0].onValueChanged.AddListener(action => { if(action) Set(true, false, false); });
        presentTabList[1].onValueChanged.AddListener(action => { if(action) Set(false, true, false); });
        presentTabList[2].onValueChanged.AddListener(action => { if(action) Set(false, false, true); });

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

    //標準項目の表示用
    public void SetDefaultTab()
    {
        presentTabList[0].SetIsOnWithoutNotify(false);
        presentTabList[1].SetIsOnWithoutNotify(true);
        presentTabList[2].SetIsOnWithoutNotify(false);
        Set(false, true, false);
    }

    //項目表示の切り替え
    public void Set(bool common, bool personal, bool log)
    {
        presentInstanceCommonList.SetActive(common);
        presentInstancePersonalList.SetActive(personal);
        presentInstanceLogList.SetActive(log);
        SetCtrlAllReceivedButton(personal); //一括受取ボタン押下制御
    }

    //一括受取ボタン押下制御
    public void SetCtrlAllReceivedButton(bool enabled = true)
    {
        var allData = PresentInstancesTable.SelectAll(0, GameUtility.Const.LOG_PRESENT_LIMIT);
        bool canTab = !presentTabList[1].isOn ? false : enabled;
        bool canPressed = allData.Count > 0 && canTab;
        buttonEffect.ButtonTextOpacityEffect(canPressed, presentInstanceAllReceivedOpenButton);
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

    //完了画面文字変更
    public void SetCompleteText(string text)
    {
        presentInstanceCompleteText.text = text;
    }
}
