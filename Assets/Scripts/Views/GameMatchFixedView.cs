using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMatchFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameMatchConfirmText;
    [SerializeField] TextMeshProUGUI gameMatchResultText;

    [SerializeField] Button gameMatchOpenButton;
    [SerializeField] Button gameMatchExecuteButton;
    [SerializeField] Button gameMatchCancelButton;
    [SerializeField] Button gameMatchResultCloseButton;

    [SerializeField] GameObject gameMatchConfirmView;
    [SerializeField] GameObject gameMatchResultView;

    [SerializeField] ClientHome clientHome;

    public Button GameMatchOpenButton => gameMatchOpenButton;

    private void Start()
    {
        var usersModel = UsersTable.Select();
        gameMatchConfirmText.text = GameUtility.Const.STAMINA_DECREASE_VALUE + GameUtility.Const.SHOW_STAMINA_DECREASE_CONFIRM;

        SetConfirm(false);
        SetResult(false);

        gameMatchOpenButton.onClick.AddListener(() => { SetConfirm(true); });
        gameMatchExecuteButton.onClick.AddListener(() => {
            clientHome.RequestHome(usersModel, GameUtility.Const.STAMINA_DECREASE_URL);
            SetConfirm(false);
            SetResult(true);
        });
        gameMatchCancelButton.onClick.AddListener(() => { SetConfirm(false); });
        gameMatchResultCloseButton.onClick.AddListener(() => { SetResult(false); });
    }

    //対戦確認画面
    private void SetConfirm(bool enabled)
    {
        gameMatchConfirmView.SetActive(enabled);
    }

    //対戦結果画面
    private void SetResult(bool enabled)
    {
        gameMatchResultText.text = GameUtility.Const.STAMINA_GEM_VALUE + GameUtility.Const.SHOW_GAMEMATCH_RESULT;
        gameMatchResultView.SetActive(enabled);
    }
}
