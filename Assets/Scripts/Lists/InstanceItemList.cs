using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceItemList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientInstance clientInstance;
    [SerializeField] InstanceItemDetailFixedView itemInstanceDetailFixedView;
    [SerializeField] TMP_Dropdown dropDownList;

    //標準ソート。最後に選ばれたソートを保持
    private string lastSortColumn = "amount";
    private string lastSortForm = GameUtility.Const.DESC;

    //現在選択中のソートリストで再表示、別のソート選択で表示更新
    private void OnEnable()
    {
        dropDownList.onValueChanged.AddListener(SortList);
        SortList(dropDownList.value);
    }

    //閉じたらボタンリセット＆表示リセット
    private void OnDisable()
    {
        dropDownList.onValueChanged.RemoveListener(SortList);
        Clear();
    }

    //ソート選択リスト
    private void SortList(int value)
    {
        switch (value)
        {
            case 0: lastSortColumn = "amount";    lastSortForm = GameUtility.Const.DESC; break;
            case 1: lastSortColumn = "amount";    lastSortForm = GameUtility.Const.ASC;  break;
            case 2: lastSortColumn = "rarity_id"; lastSortForm = GameUtility.Const.DESC; break;
            case 3: lastSortColumn = "rarity_id"; lastSortForm = GameUtility.Const.ASC;  break;
        }
        RefreshSort(lastSortColumn, lastSortForm);
    }

    //ソート付きで全てのデータを取得
    public void RefreshSort(string column, string sort)
    {
        Clear(); //再選択時に必ず破棄
        List<ItemInstancesModel> itemInstancesList = ItemInstacesTable.SelectSortAll(column, sort);
        DataList(itemInstancesList);
    }

    //開いて更新
    private void DataList(List<ItemInstancesModel> itemInstancesList)
    {
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
            button.onClick.AddListener(() => itemInstanceDetailFixedView.Set(data1, data2, data, imagePath));
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
