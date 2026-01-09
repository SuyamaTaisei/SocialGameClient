using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ShopDetailFixedView shopDetailFixedView;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int productNumber1;
    [SerializeField] int productNumber2;

    [SerializeField] string imageFolderName;
    [SerializeField] int imageNumber;
    int itemId = GameUtility.Const.SHOP_ITEM_ID;

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
            int imageindex = imageNumber;
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{imageFolderName}/{imageindex}";
            ItemDataModel data1 = ItemDataTable.SelectId(itemId);
            ItemRaritiesModel data2 = ItemRaritiesTable.SelectId(data1.rarity_id);

            //データの描画
            view.Set(shopList[i], data2, imagePath);
            button.onClick.AddListener(() => shopDetailFixedView.SetShopDetailOpen(index1, index2, imageindex, data2));
            imageNumber++;
            itemId++;
        }
    }
}