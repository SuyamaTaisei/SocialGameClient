using TMPro;
using SoundSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameMatchFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameMatchConfirmText;
    [SerializeField] TextMeshProUGUI gameMatchResultText;

    [SerializeField] Button gameMatchOpenButton;
    [SerializeField] Button gameMatchConfirmExecuteButton;
    [SerializeField] Button gameMatchConfirmCancelButton;
    [SerializeField] Button gameMatchResultCloseButton;

    [SerializeField] GameObject gameMatchConfirmView;
    [SerializeField] GameObject gameMatchResultView;

    [SerializeField] ClientHome clientHome;

    public Button GameMatchOpenButton => gameMatchOpenButton;

    private void Start()
    {
        var usersModel = UsersTable.Select();
        gameMatchConfirmText.text = GameUtility.Const.STAMINA_DECREASE_VALUE + GameUtility.Const.SHOW_STAMINA_DECREASE_CONFIRM;

        gameMatchConfirmView.SetActive(false);
        SetResult(false);

        gameMatchOpenButton.onClick.AddListener(() => { SetConfirm(true); });
        gameMatchConfirmExecuteButton.onClick.AddListener(() => {
            clientHome.RequestHome(usersModel, GameUtility.Const.STAMINA_DECREASE_URL, true, "1001");
            SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_DECISION);
            gameMatchConfirmView.SetActive(false);
            SetResult(true);
        });
        gameMatchConfirmCancelButton.onClick.AddListener(() => { SetConfirm(false); });
        gameMatchResultCloseButton.onClick.AddListener(() => { SetResult(false); });
    }

    //対戦確認画面
    private void SetConfirm(bool enabled)
    {
        gameMatchConfirmView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //対戦結果画面
    private void SetResult(bool enabled)
    {
        gameMatchResultText.text = GameUtility.Const.STAMINA_GEM_VALUE + GameUtility.Const.SHOW_GAMEMATCH_RESULT;
        gameMatchResultView.SetActive(enabled);
    }
}
