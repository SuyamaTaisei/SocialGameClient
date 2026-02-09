using System.Collections.Generic;
using UnityEngine;

public class InstanceMissionConfirmList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;

    private void OnDisable() => Clear();

    //一括受取の場合
    public void AllDataList(List<MissionInstancesModel> missionInstancesList)
    {
        Clear();
        int imageIndex = 10001;

        for (int i = 0; i < missionInstancesList.Count; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<InstanceMissionTemplateView>();

            //データの取得
            var data = missionInstancesList[i];
            var data1 = MissionDataTable.SelectId(data.mission_id);
            string imagePath = "";
            switch (data1.reward_category)
            {
                case 1001: imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_GEMS}/{imageIndex}"; break;
                case 1002: imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_COINS}/{imageIndex}"; break;
            }

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
