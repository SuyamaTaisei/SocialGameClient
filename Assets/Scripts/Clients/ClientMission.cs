using System.Collections.Generic;
using SoundSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientMission : MonoBehaviour
{
    //ウォレット表示
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI gemFreeText;
    [SerializeField] TextMeshProUGUI gemPaidText;

    [SerializeField] Button missionInstanceOpenButton;
    [SerializeField] Button missionInstanceCloseButton;
    [SerializeField] GameObject missionInstanceView;

    [SerializeField] InstanceMissionList instanceMissionList;
    [SerializeField] InstanceMissionFixedView instanceMissionFixedView;

    [SerializeField] ClientHome clientHome;
    private ApiConnect apiConnect;

    //ミッションidをキーにしたカテゴリを紐づけ
    private readonly Dictionary<int, int> saveMission = new();
    private const string column_id = "id";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        missionInstanceView.SetActive(false);
        missionInstanceOpenButton.onClick.AddListener(() => MissionOpenClose(true));
        missionInstanceCloseButton.onClick.AddListener(() => MissionOpenClose(false));
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
        SoundManager.Instance.PlaySeOneShot(GameUtility.Const.SE_DECISION);
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
            clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
            ClearMission();
        }));
    }

    //ミッション画面開閉
    public void MissionOpenClose(bool enabled)
    {
        instanceMissionFixedView.SetCtrlAllReceivedButton(); //一括受取ボタン押下制御
        missionInstanceView.SetActive(enabled);
        string soundName = enabled ? GameUtility.Const.SE_OPEN_1 : GameUtility.Const.SE_CLOSE;
        SoundManager.Instance.PlaySeOneShot(soundName);
        clientHome.WalletApply(coinText, gemFreeText, gemPaidText);
        clientHome.RefreshHomeWallet();
    }
}
