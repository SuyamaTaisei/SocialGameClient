using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GachaResultList : MonoBehaviour
{
    [SerializeField] GameObject gachaResultView;
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] TextMeshProUGUI newText;

    private ClientGacha clientGacha;

    //DBモデル
    private CharacterDataModel characterDataModel;
    private CharacterRaritiesModel characterRaritiesModel;

    private void Start()
    {
        gachaResultView.SetActive(false);
        clientGacha = FindFirstObjectByType<ClientGacha>();
    }

    //戻った時は、再度ガチャ結果を表示するためにリセット
    public void CloseButton()
    {
        gachaResultView.SetActive(false);
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
            newText.text = "";
        }
    }

    //ガチャ結果表示処理
    public void ShowGachaResult(GachaResultsModel[] gachaResults, GachaResultsModel[] newGachaResults)
    {
        gachaResultView.SetActive(true);

        //新規キャラクターIDの重複防止
        HashSet<int> existCharacterId = new HashSet<int>();

        for (int i = 0; i < clientGacha.GachaCount; i++)
        {
            //ガチャ回数分全てを取得
            var gachaResult = gachaResults[i];

            //ガチャ回数分の内、新規で出たキャラクターIDのみ
            bool isNew = false;
            for (int j = 0; j < newGachaResults.Length; j++)
            {
                //新規キャラのみで走査し、ガチャ結果と一致かつHashSetのインスタンスに含まれていなければ新規追加
                if (gachaResult.character_id == newGachaResults[j].character_id && !existCharacterId.Contains(gachaResult.character_id))
                {
                    isNew = true;
                    existCharacterId.Add(gachaResult.character_id);
                    break;
                }
            }

            //新規入手
            if (isNew)
            {
                newText.text = GameUtility.Const.SHOW_GACHA_NEW;
            }
            //所持済み
            else
            {
                newText.text = "";
            }

            //idが一致するレコードを取得
            characterDataModel = CharacterDataTable.SelectId(gachaResult.character_id);
            nameText.text = characterDataModel.name;

            //レアリティIDをキャプチャ
            int rarityId = characterDataModel.rarity_id;
            characterRaritiesModel = CharacterRaritiesTable.SelectId(rarityId);
            rarityText.text = characterRaritiesModel.name;

            //ガチャ結果のキャラクターid画像を取得
            string imagePath = $"Images/Characters/{gachaResult.character_id}";
            Sprite sprite = Resources.Load<Sprite>(imagePath);

            //雛形をもとにリストを生成
            GameObject item = Instantiate(templateView, content);

            //キャラクター画像の取得
            var image = item.GetComponent<GachaResultItemView>();
            if (image != null)
            {
                image.CharacterImage.sprite = sprite;
                image.CharacterImage.preserveAspect = true;
            }
        }
    }
}