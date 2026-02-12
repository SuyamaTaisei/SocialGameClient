using UnityEngine;
using UnityEngine.UI;

public class InstanceMissionList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
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
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            var view = item.GetComponent<InstanceMissionTemplateView>();

            //データの取得
            var data = missionDataList[i];
            string imagePath = "";
            switch(data.reward_category)
            {
                case 1001: imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_GEMS}/{imageIndex}"; break;
                case 1002: imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_COINS}/{imageIndex}"; break;
            }
            var data1 = MissionInstancesTable.SelectId(data.id, data.mission_category);

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
