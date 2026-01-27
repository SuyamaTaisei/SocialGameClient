using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientPresent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI presentInstanceMessageText;
    [SerializeField] Button presentInstanceOpenButton;
    [SerializeField] Button presentInstanceCloseButton;
    [SerializeField] GameObject presentInstanceView;

    private void Start()
    {
        presentInstanceView.SetActive(false);
        presentInstanceOpenButton.onClick.AddListener(() => presentInstanceView.SetActive(true));    
        presentInstanceCloseButton.onClick.AddListener(() => presentInstanceView.SetActive(false));    
    }

    //警告表示
    public void Message(string message)
    {
        presentInstanceMessageText.text = message;
    }
}
