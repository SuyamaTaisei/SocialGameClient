using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientMission : MonoBehaviour
{
    [SerializeField] Button missionInstanceOpenButton;
    [SerializeField] Button missionInstanceCloseButton;
    [SerializeField] GameObject missionInstanceView;

    [SerializeField] InstanceMissionList instanceMissionList;
    [SerializeField] InstanceMissionFixedView instanceMissionFixedView;
    private ApiConnect apiConnect;

    //ミッションidをキーにしたカテゴリを紐づけ
    private readonly Dictionary<int, int> saveMission = new();
    private const string column_id = "id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        missionInstanceView.SetActive(false);
        missionInstanceOpenButton.onClick.AddListener(() =>
        {
            instanceMissionFixedView.SetCtrlAllReceivedButton(); //一括受取ボタン押下制御
            missionInstanceView.SetActive(true);
        });
        missionInstanceCloseButton.onClick.AddListener(() => missionInstanceView.SetActive(false));
    }

    //ミッションid、カテゴリを保持
    public void SaveMission(int id, int category)
    {
        saveMission[id] = category;
    }

    //保持された紐づけをリセット
    public void ClearMission()
    {
        saveMission.Clear();
    }

    //ミッション受取の送信処理
    public void RequestMissionReceived()
    {
        var usersModel = UsersTable.Select();

        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(column_id, usersModel.id),
        };

        //保持id、カテゴリでペアを生成
        var mission = new List<KeyValuePair<int, int>>(saveMission);

        //id、カテゴリをペアで送信して、URLの末尾に追加
        for (int i = 0; i < mission.Count; i++)
        {
            //値の取り出し
            var category = mission[i].Value;

            form.Add(new MultipartFormDataSection($"mission[{i}][mission_id]", mission[i].Key.ToString()));
            form.Add(new MultipartFormDataSection($"mission[{i}][mission_category]", category.ToString()));
        }

        //リクエスト送信後の成功時レスポンス受け取りコールバック
        StartCoroutine(apiConnect.Send(GameUtility.Const.MISSION_RECEIVED_URL, form, (action) =>
        {
            instanceMissionList.Refresh();
            instanceMissionFixedView.SetCtrlAllReceivedButton(); //一括受取ボタン押下制御
            instanceMissionFixedView.SetConfirm(false);          //確認画面閉じる
            instanceMissionFixedView.SetComplete(true);          //受取完了画面表示
            ClearMission();
        }));
    }
}
