using UnityEngine;

public class DataGetManager : MonoBehaviour
{
    //キャラクターデータ取得
    public (CharacterDataModel, CharacterRaritiesModel, string) GetCharacterData(int index)
    {
        var data1 = CharacterDataTable.SelectId(index);
        var data2 = CharacterRaritiesTable.SelectId(data1.rarity_id);
        string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{index}";
        return (data1, data2, imagePath);
    }

    //アイテムデータ取得
    public (ItemDataModel, ItemRaritiesModel, string) GetItemData(int index)
    {
        var data1 = ItemDataTable.SelectId(index);
        var data2 = ItemRaritiesTable.SelectId(data1.rarity_id);
        string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{index}";
        return (data1, data2, imagePath);
    }

    //ガチャ期間データ取得
    public GachaPeriodsModel GetGachaPeriodData(int index)
    {
        var data3 = GachaPeriodsTable.SelectId(index);
        return data3;
    }

    //ミッションインスタンスデータ取得
    public MissionInstancesModel GetMissionInstanceData(int index1, int index2)
    {
        var data1 = MissionInstancesTable.SelectId(index1, index2);
        return data1;
    }

    //ミッションデータ取得
    public MissionDataModel GetMissionData(int index)
    {
        var data1 = MissionDataTable.SelectId(index);
        return data1;
    }

    //ミッション報酬データ取得
    public string GetMissionReward(int index1, int index2)
    {
        string imagePath = "";
        switch (index1)
        {
            case 1001: imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_GEMS}/{index2}"; break;
            case 1002: imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_COINS}/{index2}"; break;
        }
        return imagePath;
    }
}
