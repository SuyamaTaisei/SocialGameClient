using System.Collections.Generic;
using UnityEngine;

public class GachaPickUpList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] GachaPeriodTemplateView gachaPeriodTemplateView;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int pickUpNumber;

    private void Start()
    {
        //データ実体の生成
        for (int i = startCount; i <= maxCount; i++)
        {
            GameObject item = Instantiate(templateView, content);
            int index = pickUpNumber + i;
            var view = item.GetComponent<GachaPickUpTemplateView>();

            List<GachaDataModel> gachaDataModel = GachaDataTable.SelectAllGachaId(gachaPeriodTemplateView.GachaId);

            foreach (var list in gachaDataModel)
            {
                if (list.character_id == index)
                {
                    //一致するデータの取得
                    var characterDataModel = CharacterDataTable.SelectId(index);
                    var characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
                    string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{index}";

                    //データの描画
                    view.Set(characterDataModel, characterRaritiesModel, imagePath);
                }
            }
        }
    }
}