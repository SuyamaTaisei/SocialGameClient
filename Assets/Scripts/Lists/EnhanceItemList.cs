using System.Collections.Generic;
using UnityEngine;

public class EnhanceItemList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientInstance clientInstance;

    private void OnEnable() => RefreshEnhanceList();
    private void OnDisable() => Clear();

    //外部更新用
    public void Refresh()
    {
        Clear();
        RefreshEnhanceList();
    }

    public void RefreshEnhanceList()
    {
        List<ItemInstancesModel> itemInstancesList = ItemInstacesTable.SelectEnhanceItemAll();

        //何もアイテムを所持していなければ
        if (itemInstancesList == null || itemInstancesList.Count == 0)
        {
            clientInstance.NothingEnhanceItemMessage(GameUtility.Const.SHOW_INSTANCE_ENHANCE_ITEM_NOTHING);
            return;
        }

        clientInstance.NothingEnhanceItemMessage("");

        for (int i = 0; i < itemInstancesList.Count; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<EnhanceItemTemplateView>();

            //データの取得
            int index = i;
            var data = itemInstancesList[index];
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{data.item_id}";
            ItemDataModel data1 = ItemDataTable.SelectId(data.item_id);
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
