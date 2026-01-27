using System.Collections.Generic;
using UnityEngine;

public class InstancePresentList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientPresent clientPresent;
    [SerializeField] InstancePresentTemplateView instancePresentTemplateView;
    [SerializeField] int received;

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
        List<PresentInstancesModel> presentInstancesList = PresentInstancesTable.SelectAll(received);

        //何もアイテムを所持していなければ
        if (presentInstancesList == null || presentInstancesList.Count == 0)
        {
            clientPresent.Message(GameUtility.Const.SHOW_INSTANCE_PRESENT_NOTHING);
            return;
        }

        //受取済みかどうか
        if (received == 0)
        {
            instancePresentTemplateView.SetShowReceived(true, false);
        }
        else
        {
            instancePresentTemplateView.SetShowReceived(false, true);        
        }

        clientPresent.Message("");

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

    //閉じてリセット
    private void Clear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
