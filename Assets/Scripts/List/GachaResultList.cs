using UnityEngine;

public class GachaResultList : MonoBehaviour
{
    [SerializeField] GameObject gachaResultView;
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;

    private ClientGacha clientGacha;

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
        }
    }

    public void ShowGachaResult(GachaResultsModel[] gachaResultsModel)
    {
        gachaResultView.SetActive(true);

        for (int i = 0; i < clientGacha.GachaCount; i++)
        {
            var gachaResult = gachaResultsModel[i];

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