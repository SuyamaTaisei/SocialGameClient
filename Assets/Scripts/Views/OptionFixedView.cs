using UnityEngine;
using SoundSystem;
using UnityEngine.UI;

public class OptionFixedView : MonoBehaviour
{
    [SerializeField] Button optionOpenButton;
    [SerializeField] Button optionCloseButton;
    [SerializeField] GameObject optionView;

    private void Start()
    {
        optionView.SetActive(false);
        optionOpenButton.onClick.AddListener(() => OptionOpenClose(true));
        optionCloseButton.onClick.AddListener(() => OptionOpenClose(false));
    }

    public void OptionOpenClose(bool enabled)
    {
        optionView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }
}
