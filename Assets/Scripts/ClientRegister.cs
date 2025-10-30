using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientRegister : MonoBehaviour
{
    [SerializeField] TMP_InputField userName;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] Toggle checkBox;

    private const int inputMinLimit     = 4;
    private const int timeOut           = 10;
    private const string endPoint       = "http://localhost/api/register";
    private const string warnName       = "名前は4文字以上で入力してください";
    private const string warnCheck      = "利用規約に同意してください";
    private const string columnUserName = "user_name";

    public void Send() => StartCoroutine(Register());

    public IEnumerator Register()
    {
        string userName = this.userName.text;

        if (userName.Length >= inputMinLimit && checkBox.isOn)
        {
            //POST送信用のフォームを作成
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(columnUserName, userName)
            };

            //POSTでデータを送信
            UnityWebRequest request = UnityWebRequest.Post(endPoint, form);
            request.timeout = timeOut;
            yield return request.SendWebRequest();

            //レスポンスが成功したら
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("アカウント登録完了");
            }
            //失敗したら
            else
            {
                //エラーの場合
                if (!string.IsNullOrEmpty(request.error))
                {
                    Debug.LogError(request.error);
                    yield break;
                }
            }
        }
        else if (userName.Length < inputMinLimit)
        {
            warningText.text = warnName;
        }
        else if (!checkBox.isOn)
        {
            warningText.text = warnCheck;
        }
    }
    public void RsgisterComplete()
    {

    }
}