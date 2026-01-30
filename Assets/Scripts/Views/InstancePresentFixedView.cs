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

    [SerializeField] GameObject presentInstanceCommonList;
    [SerializeField] GameObject presentInstancePersonalList;
    [SerializeField] GameObject presentInstanceLogList;

    [SerializeField] GameObject presentInstanceConfirmView;
    [SerializeField] GameObject presentInstanceCompleteView;

    [SerializeField] ClientPresent clientPresent;

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
    }

    //項目表示の切り替え
    public void Set(bool common, bool personal, bool log)
    {
        presentInstanceCommonList.SetActive(common);
        presentInstancePersonalList.SetActive(personal);
        presentInstanceLogList.SetActive(log);
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
