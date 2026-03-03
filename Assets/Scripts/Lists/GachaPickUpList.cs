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
        //データの生成
        for (int i = startCount; i <= maxCount; i++)
        {
            var item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaPickUpTemplateView>();
            int index = pickUpNumber + i;

            List<GachaDataModel> gachaDataModel = GachaDataTable.SelectAllGachaId(gachaPeriodTemplateView.GachaId);

            foreach (var list in gachaDataModel)
            {
                if (list.character_id == index)
                {
                    //データの取得
                    var data1 = CharacterDataTable.SelectId(index);
                    var data2 = CharacterRaritiesTable.SelectId(data1.rarity_id);
                    string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{index}";

                    //データの描画
                    view.Set(data1, data2, imagePath);
                }
            }
        }
    }
}