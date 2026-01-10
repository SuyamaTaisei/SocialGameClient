using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientInstance clientInstance;
    [SerializeField] InstanceCharacterDetailFixedView charaInstanceDetailFixedView;
    [SerializeField] TMP_Dropdown dropDownList;

    //標準ソート。最後に選ばれたソートを保持
    private string lastSortColumn = "id";
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
            case 0: lastSortColumn = "id";        lastSortForm = GameUtility.Const.DESC; break;
            case 1: lastSortColumn = "id";        lastSortForm = GameUtility.Const.ASC;  break;
            case 2: lastSortColumn = "level";     lastSortForm = GameUtility.Const.DESC; break;
            case 3: lastSortColumn = "level";     lastSortForm = GameUtility.Const.ASC;  break;
            case 4: lastSortColumn = "rarity_id"; lastSortForm = GameUtility.Const.DESC; break;
            case 5: lastSortColumn = "rarity_id"; lastSortForm = GameUtility.Const.ASC;  break;
        }
        RefreshSort(lastSortColumn, lastSortForm);
    }

    //外部更新用
    public void Refresh()
    {
        RefreshSort(lastSortColumn, lastSortForm);
    }

    //ソート付きで全てのデータを取得
    public void RefreshSort(string column, string sort)
    {
        Clear(); //再選択時に必ず破棄
        List<CharacterInstancesModel> characterInstancesList = CharacterInstancesTable.SelectSortAll(column, sort);
        DataList(characterInstancesList);
    }

    public void DataList(List<CharacterInstancesModel> characterInstancesList)
    {
        //何もキャラを所持していなければ
        if (characterInstancesList == null || characterInstancesList.Count == 0)
        {
            clientInstance.CharaInstanceMessage(GameUtility.Const.SHOW_INSTANCE_CHARA_NOTHING);
            return;
        }

        clientInstance.CharaInstanceMessage("");

        for (int i = 0; i < characterInstancesList.Count; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            var view = item.GetComponent<InstanceCharacterTemplateView>();

            //データの取得
            int index = i;
            var data = characterInstancesList[index];
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{data.character_id}";
            CharacterDataModel data1 = CharacterDataTable.SelectId(data.character_id);
            CharacterRaritiesModel data2 = CharacterRaritiesTable.SelectId(data1.rarity_id);

            //データの描画
            view.Set(data1, data2, data, imagePath);
            button.onClick.AddListener(() => charaInstanceDetailFixedView.Set(data1, data2, data, imagePath));
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
