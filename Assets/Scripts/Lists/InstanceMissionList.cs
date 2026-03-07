using UnityEngine;
using UnityEngine.UI;

public class InstanceMissionList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] InstanceMissionTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] DataGetManager dataGetManager;
    [SerializeField] ClientMission clientMission;
    [SerializeField] InstanceMissionFixedView instanceMissionFixedView;
    [SerializeField] InstanceMissionTemplateView instanceMissionTemplateView;
    [SerializeField] InstanceMissionConfirmList instanceMissionConfirmList;

    private void OnEnable() => DataList();
    private void OnDisable() => Clear();

    //外部更新用
    public void Refresh()
    {
        Clear();
        DataList();
    }

    public void DataList()
    {
        var missionDataList = MissionDataTable.SelectAll(GameUtility.Const.LOG_MISSION_LIMIT);
        int imageIndex = 10001;

        for (int i = 0; i < missionDataList.Count; i++)
        {
            //データの生成
            var (view, button) = dataListManager.CreateDataListSync(templateView, content, true, true);

            //データの取得
            var data = missionDataList[i];
            var data1 = dataGetManager.GetMissionInstanceData(data.id, data.mission_category);
            string imagePath = dataGetManager.GetMissionReward(data.reward_category, imageIndex);

            //データの描画
            view.Set(data, data1, imagePath);

            //単一受取ボタン
            if (button)
            {
                button.onClick.AddListener(() =>
                {
                    //辞書データをクリア
                    clientMission.ClearMission();

                    //キャプチャしてから渡す(複数同値レコード取得阻止)
                    int missionId = data1.mission_id;
                    int category = data1.mission_category;

                    //ミッション確認画面表示＆データセット
                    clientMission.SaveMission(missionId, category);
                    instanceMissionConfirmList.SingleDataList(missionId, category);
                    instanceMissionFixedView.SetConfirm(true);
                });
            }
        }
    }

    //閉じてリセット
    private void Clear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
