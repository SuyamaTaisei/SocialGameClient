using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientPresent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI presentInstanceMessageText;
    [SerializeField] Button presentInstanceOpenButton;
    [SerializeField] Button presentInstanceCloseButton;
    [SerializeField] GameObject presentInstanceView;

    [SerializeField] InstancePresentList instancePresentList;
    [SerializeField] InstancePresentFixedView instancePresentFixedView;
    private ApiConnect apiConnect;

    //プレゼントインスタンスID取得
    private int instanceId;

    //プレゼントインスタンス一覧で選択したカテゴリ、内容、数量の紐づけ
    private readonly Dictionary<int, (int, int)> savePresents = new();
    private const string column_id = "id";
    private const string column_instance_id = "instance_id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        presentInstanceView.SetActive(false);
        presentInstanceOpenButton.onClick.AddListener(() => presentInstanceView.SetActive(true));    
        presentInstanceCloseButton.onClick.AddListener(() => presentInstanceView.SetActive(false));    
    }

    //プレゼントインスタンスIDを取得
    public void GetPresentId(int presentId)
    {
        instanceId = presentId;
    }

    //プレゼントのカテゴリ、内容、数量を保持
    public void SavePresent(int category, int content, int amount)
    {
        savePresents[category] = (content, amount);
    }

    //保持された紐づけをリセット
    public void ClearPresent()
    {
        savePresents.Clear();
    }

    //プレゼント受取の送信処理
    public void RequestPresentReceived()
    {
        var usersModel = UsersTable.Select();

        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_instance_id, instanceId.ToString()),
        };

        //保持したカテゴリ、内容、数量でペアを生成
        var presents = new List<KeyValuePair<int, (int, int)>>(savePresents);

        //カテゴリ、内容、数量をペアで送信して、URLの末尾に追加
        for (int i = 0; i < presents.Count; i++)
        {
            form.Add(new MultipartFormDataSection($"presents[{i}][category]", presents[i].Key.ToString()));
            form.Add(new MultipartFormDataSection($"presents[{i}][content]", presents[i].Value.ToString()));
            form.Add(new MultipartFormDataSection($"presents[{i}][amount]", presents[i].Value.ToString()));
        }

        //リクエスト送信後の成功時レスポンス受け取りコールバック
        StartCoroutine(apiConnect.Send(GameUtility.Const.PRESENT_RECEIVED_URL, form, (action) =>
        {
            instancePresentList.Refresh();
            instancePresentFixedView.SetConfirm(false); //確認画面閉じる
            instancePresentFixedView.SetComplete(true); //プレゼント受取完了画面表示
            ClearPresent();
        }));
    }

    //警告表示
    public void Message(string message)
    {
        presentInstanceMessageText.text = message;
    }
}
