using TMPro;
using SoundSystem;
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

    //プレゼントインスタンスidをキーにしたカテゴリ、内容、数量の値をタプルで紐づけ
    private readonly Dictionary<int, (int, int, int)> savePresents = new();
    private const string column_id = "id";
    private const string column_mission_id = "mission_id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        presentInstanceView.SetActive(false);
        presentInstanceOpenButton.onClick.AddListener(() =>
        {
            instancePresentFixedView.SetDefaultTab(); //常に標準項目を表示
            PresentOpenClose(true);
        });
        presentInstanceCloseButton.onClick.AddListener(() => PresentOpenClose(false));    
    }

    //プレゼントのid、カテゴリ、内容、数量を保持
    public void SavePresent(int id, int category, int content, int amount)
    {
        savePresents[id] = (category, content, amount);
    }

    //保持された紐づけをリセット
    public void ClearPresent()
    {
        savePresents.Clear();
    }

    //プレゼント受取の送信処理
    public void RequestPresentReceived()
    {
        SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_DECISION);
        var usersModel = UsersTable.Select();

        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
            new MultipartFormDataSection(column_mission_id, "1005"),
        };

        //保持したid、カテゴリ、内容、数量でペアを生成
        var presents = new List<KeyValuePair<int, (int, int, int)>>(savePresents);

        //id、カテゴリ、内容、数量をペアで送信して、URLの末尾に追加
        for (int i = 0; i < presents.Count; i++)
        {
            //値となるタプルの取り出し
            var (category, content, amount) = presents[i].Value;

            form.Add(new MultipartFormDataSection($"presents[{i}][instance_id]", presents[i].Key.ToString()));
            form.Add(new MultipartFormDataSection($"presents[{i}][category]", category.ToString()));
            form.Add(new MultipartFormDataSection($"presents[{i}][content]", content.ToString()));
            form.Add(new MultipartFormDataSection($"presents[{i}][amount]", amount.ToString()));
        }

        //リクエスト送信後の成功時レスポンス受け取りコールバック
        StartCoroutine(apiConnect.Send(GameUtility.Const.PRESENT_RECEIVED_URL, form, (action) =>
        {
            instancePresentList.Refresh();
            instancePresentFixedView.SetCtrlAllReceivedButton(); //一括受取ボタン押下制御
            instancePresentFixedView.SetConfirm(false); //確認画面閉じる
            instancePresentFixedView.SetComplete(true); //プレゼント受取完了画面表示
            ClearPresent();
        }));
    }

    //プレゼントインスタンス開閉
    public void PresentOpenClose(bool enabled)
    {
        presentInstanceView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
    }

    //警告表示
    public void Message(string message)
    {
        presentInstanceMessageText.text = message;
    }
}
