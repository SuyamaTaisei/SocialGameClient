using UnityEngine;
using UnityEngine.UI;

public class InstancePresentFixedView : MonoBehaviour
{
    [SerializeField] Button presentInstanceCommonOpenButton;
    [SerializeField] Button presentInstancePersonalOpenButton;
    [SerializeField] Button presentInstanceLogOpenButton;

    [SerializeField] GameObject presentInstanceCommonList;
    [SerializeField] GameObject presentInstancePersonalList;
    [SerializeField] GameObject presentInstanceLogList;

    private void Start()
    {
        Set(false, true, false);
        
        presentInstanceCommonOpenButton.onClick.AddListener(()   => Set(true, false, false));
        presentInstancePersonalOpenButton.onClick.AddListener(() => Set(false, true, false));
        presentInstanceLogOpenButton.onClick.AddListener(()      => Set(false, false, true));
    }

    //項目表示の切り替え
    public void Set(bool common, bool personal, bool log)
    {
        presentInstanceCommonList.SetActive(common);
        presentInstancePersonalList.SetActive(personal);
        presentInstanceLogList.SetActive(log);
    }
}
