using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientShop clientShop;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int productNumber1;
    [SerializeField] int productNumber2;

    [SerializeField] string imageFolderName;
    [SerializeField] int imageNumber;

    private void Start()
    {
        List<ShopDataModel> shopList = ShopDataTable.SelectAll();

        for (int i = startCount; i <= maxCount; i++)
        {
            //データの生成
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            ShopTemplateView view = item.GetComponent<ShopTemplateView>();

            //データの取得
            int index1 = productNumber1 + i;
            int index2 = productNumber2 + i;
            int itemId = imageNumber;
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{imageFolderName}/{itemId}";

            //データの描画
            view.Set(shopList[i], imagePath);
            button.onClick.AddListener(() => clientShop.OpenConfirmButton(index1, index2, itemId));
            imageNumber++;
        }
    }
}