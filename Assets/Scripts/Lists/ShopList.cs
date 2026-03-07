using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] ShopTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] ShopDetailFixedView shopDetailFixedView;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int productNumber1;
    [SerializeField] int productNumber2;

    [SerializeField] string imageFolderName;
    [SerializeField] int imageNumber;

    [SerializeField] bool isShopDetail;

    int itemId = GameUtility.Const.SHOP_ITEM_ID;

    private void Start()
    {
        List<ShopDataModel> shopList = ShopDataTable.SelectAll();

        for (int i = startCount; i <= maxCount; i++)
        {
            //データの生成
            var (view, button) = dataListManager.CreateDataListSync(templateView, content, true, true);

            //データの取得
            int index1 = productNumber1 + i;
            int index2 = productNumber2 + i;
            int imageindex = imageNumber;
            var data1 = ItemDataTable.SelectId(itemId);
            var data2 = ItemRaritiesTable.SelectId(data1.rarity_id);
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{imageFolderName}/{imageindex}";

            //データの描画
            view.Set(shopList[i], data2, imagePath);

            //ショップリストに応じて確認導線を変更
            if (isShopDetail)
            {
                button.onClick.AddListener(() => shopDetailFixedView.SetShopDetailOpen(index1, index2, imageindex, data2));
            }
            else
            {
                button.onClick.AddListener(() => shopDetailFixedView.SetBuyConfirmOpen(index1, GameUtility.Const.SHOW_YEN));
            }

            imageNumber++;
            itemId++;
        }
    }
}