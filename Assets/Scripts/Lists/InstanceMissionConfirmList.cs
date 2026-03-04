using System.Collections.Generic;
using UnityEngine;

public class InstanceMissionConfirmList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] InstanceMissionTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] DataGetManager dataGetManager;

    private void OnDisable() => Clear();

    //一括受取の場合
    public void AllDataList(List<MissionInstancesModel> missionInstancesList)
    {
        Clear();
        int imageIndex = 10001;

        for (int i = 0; i < missionInstancesList.Count; i++)
        {
            //データの生成
            var (view, _) = dataListManager.CreateDataListSync(templateView, content);

            //データの取得
            var data = missionInstancesList[i];
            var data1 = dataGetManager.GetMissionData(data.mission_id);
            string imagePath = dataGetManager.GetMissionReward(data1.reward_category, imageIndex);

            //データの描画
            view.Set(data1, data, imagePath);
        }
    }

    //単一受取の場合
    public void SingleDataList(int missionId, int category)
    {
        var missionInstancesList = MissionInstancesTable.SelectIdAll(missionId, category);
        AllDataList(missionInstancesList);
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
