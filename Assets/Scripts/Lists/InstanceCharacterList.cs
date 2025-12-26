using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientInstance clientInstance;

    private void OnEnable() => Refresh();
    private void OnDisable() => Clear();

    //開いて更新
    private void Refresh()
    {
        List<CharacterInstancesModel> characterInstancesList = CharacterInstancesTable.SelectAll();

        //何もキャラを所持していなければ
        if (characterInstancesList == null || characterInstancesList.Count == 0)
        {
            clientInstance.NothingMessage("キャラクターを所持していません");
            return;
        }

        clientInstance.NothingMessage("");

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
