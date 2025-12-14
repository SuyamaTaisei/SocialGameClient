using System.Collections.Generic;
using UnityEngine;

public class GachaLogList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientGacha clientGacha;

    //DBモデル
    private GachaPeriodsModel gachaPeriodsModel;
    private CharacterDataModel characterDataModel;
    private CharacterRaritiesModel characterRaritiesModel;

    //ガチャ履歴ボタン押下で、履歴リスト更新と破棄
    private void OnEnable() => Refresh();
    private void OnDisable() => Destroy();

    //開いて更新
    private void Refresh()
    {
        List<GachaLogsModel> gachaLogsList = GachaLogsTable.SelectLatest(GameUtility.Const.LOG_GACHA_LIMIT);

        //何もガチャ履歴が無ければ
        if (gachaLogsList.Count == 0)
        {
            clientGacha.NothingGachaLogMessage(GameUtility.Const.SHOW_GACHA_LOG_NOTHING);
            return;
        }

        clientGacha.NothingGachaLogMessage("");

        for (int i = 0; i < gachaLogsList.Count; i++)
        {
            GameObject item = Instantiate(templateView, content);
            var view = item.GetComponent<GachaLogTemplateView>();
            int index = i;

            characterDataModel = CharacterDataTable.SelectId(gachaLogsList[index].character_id);
            characterRaritiesModel = CharacterRaritiesTable.SelectId(characterDataModel.rarity_id);
            gachaPeriodsModel = GachaPeriodsTable.SelectId(gachaLogsList[index].gacha_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_CHARACTERS}/{gachaLogsList[index].character_id}";

            //表記
            view.CharacterImage.sprite = Resources.Load<Sprite>(imagePath);
            view.NameText.text = characterDataModel.name;
            view.RarityText.text = characterRaritiesModel.name;
            view.DateTimeText.text = gachaLogsList[i].created_at;
            view.GachaPeriodText.text = gachaPeriodsModel.name;
        }
    }

    //閉じて破棄
    private void Destroy()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
