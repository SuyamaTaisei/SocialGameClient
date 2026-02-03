using System.Collections.Generic;
using UnityEngine;

public class InstancePresentConfirmList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;

    private void OnDisable() => Clear();

    //一括受取の場合
    public void AllDataList(List<PresentInstancesModel> presentInstancesList)
    {
        Clear();

        for (int i = 0; i < presentInstancesList.Count; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<InstancePresentTemplateView>();

            //データの取得
            int index = i;
            var data = presentInstancesList[index];
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{data.content}";
            ItemDataModel data1 = ItemDataTable.SelectId(data.content);
            ItemRaritiesModel data2 = ItemRaritiesTable.SelectId(data1.rarity_id);

            //データの描画
            view.Set(data1, data2, data, imagePath);
        }
    }

    //単一受取の場合
    public void SingleDataList(int instanceId, int category, int content, int amount)
    {
        var presentInstancesList = PresentInstancesTable.SelectId(instanceId, category, content, amount);
        AllDataList(presentInstancesList);
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
