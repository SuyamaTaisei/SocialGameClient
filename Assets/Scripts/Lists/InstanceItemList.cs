using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceItemList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientInstance clientInstance;

    private void OnEnable() => Refresh();
    private void OnDisable() => Clear();

    //開いて更新
    private void Refresh()
    {
        List<ItemInstancesModel> itemInstancesList = ItemInstacesTable.SelectAll();

        //何もアイテムを所持していなければ
        if (itemInstancesList == null || itemInstancesList.Count == 0)
        {
            clientInstance.NothingItemMessage(GameUtility.Const.SHOW_INSTANCE_ITEM_NOTHING);
            return;
        }

        clientInstance.NothingItemMessage("");

        for (int i = 0; i < itemInstancesList.Count; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            var view = item.GetComponent<InstanceItemTemplateView>();

            //データの取得
            int index = i;
            var data = itemInstancesList[index];
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{data.item_id}";
            ItemDataModel data1 = ItemDataTable.SelectId(data.item_id);
            ItemRaritiesModel data2 = ItemRaritiesTable.SelectId(data1.rarity_id);

            //データの描画
            view.Set(data1, data2, data, imagePath);
            //button.onClick.AddListener(() => instanceCharacterTemplateView...;
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
