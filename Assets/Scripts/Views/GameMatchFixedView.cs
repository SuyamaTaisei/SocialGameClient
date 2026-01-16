using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMatchFixedView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameMatchConfirmText;
    [SerializeField] Button gameMatchButton;
    [SerializeField] Button gameMatchExecuteButton;
    [SerializeField] Button gameMatchCancelButton;
    [SerializeField] GameObject gameMatchConfirmView;

    [SerializeField] ClientHome clientHome;

    public Button GameMatchButton => gameMatchButton;

    private void Start()
    {
        var usersModel = UsersTable.Select();
        gameMatchConfirmText.text = GameUtility.Const.STAMINA_DECREASE_VALUE + GameUtility.Const.SHOW_STAMINA_DECREASE_CONFIRM;

        gameMatchConfirmView.SetActive(false);

        gameMatchButton.onClick.AddListener(() => { gameMatchConfirmView.SetActive(true); });
        gameMatchExecuteButton.onClick.AddListener(() => {
            clientHome.RequestHome(usersModel, GameUtility.Const.STAMINA_DECREASE_URL);
            gameMatchConfirmView.SetActive(false);
        });
        gameMatchCancelButton.onClick.AddListener(() => { gameMatchConfirmView.SetActive(false); });
    }
}
