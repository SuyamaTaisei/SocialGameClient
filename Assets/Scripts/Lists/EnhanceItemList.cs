using System.Collections.Generic;
using UnityEngine;

public class EnhanceItemList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] EnhanceItemTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] DataGetManager dataGetManager;
    [SerializeField] ClientInstance clientInstance;

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
        List<ItemInstancesModel> itemInstancesList = ItemInstancesTable.SelectEnhanceItemAll();

        //何もアイテムを所持していなければ
        if (itemInstancesList == null || itemInstancesList.Count == 0)
        {
            clientInstance.EnhanceItemMessage(GameUtility.Const.SHOW_INSTANCE_ENHANCE_ITEM_NOTHING);
            return;
        }

        clientInstance.EnhanceItemMessage("");

        for (int i = 0; i < itemInstancesList.Count; i++)
        {
            //データの生成
            var (view, _) = dataListManager.CreateDataListSync(templateView, content);

            //データの取得
            var data = itemInstancesList[i];
            var (data1, data2, imagePath) = dataGetManager.GetItemData(data.item_id);

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
