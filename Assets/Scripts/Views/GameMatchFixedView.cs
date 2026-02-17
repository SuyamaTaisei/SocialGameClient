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

    private string endPoint = "";
    private string missionId = "";

    private void Start()
    {
        var usersModel = UsersTable.Select();

        gameMatchConfirmView.SetActive(false);
        SetResult(false);

        gameMatchOpenButton.onClick.AddListener(() => { SetConfirm(true); });
        gameMatchConfirmExecuteButton.onClick.AddListener(() => {
            clientHome.RequestHome(usersModel, endPoint, true, missionId);
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
        var usersModel = UsersTable.Select();

        if (usersModel.last_stamina >= GameUtility.Const.STAMINA_DECREASE_VALUE)
        {
            endPoint = GameUtility.Const.STAMINA_DECREASE_URL;
            missionId = "1001";
            gameMatchConfirmText.text = GameUtility.Const.STAMINA_DECREASE_VALUE + GameUtility.Const.SHOW_STAMINA_DECREASE_CONFIRM;
        }
        else
        {
            endPoint = GameUtility.Const.STAMINA_INCREASE_URL;
            missionId = "1004";
            gameMatchConfirmText.text = "スタミナが足りません\n" + GameUtility.Const.STAMINA_GEM_VALUE + GameUtility.Const.SHOW_STAMINA_RECOVERY_CONFIRM;
        }

        gameMatchConfirmView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //対戦結果画面
    private void SetResult(bool enabled)
    {
        switch (endPoint)
        {
            case GameUtility.Const.STAMINA_DECREASE_URL: gameMatchResultText.text = GameUtility.Const.STAMINA_GEM_VALUE + GameUtility.Const.SHOW_GAMEMATCH_RESULT; break;
            case GameUtility.Const.STAMINA_INCREASE_URL: gameMatchResultText.text = GameUtility.Const.SHOW_STAMINA_RECOVERY; break;
        }
        gameMatchResultView.SetActive(enabled);
    }
}
