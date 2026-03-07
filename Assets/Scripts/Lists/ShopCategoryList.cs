using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] ShopCategoryTemplateView templateView;
    [SerializeField] DataListManager dataListManager;
    [SerializeField] ClientShop clientShop;
    [SerializeField] ShopCategoryTemplateView shopCategoryTemplateView;

    private void Start()
    {
        List<ShopCategoriesModel> shopCategoriesList = ShopCategoriesTable.SelectAll();

        for (int i = 0; i < shopCategoriesList.Count; i++)
        {
            //データの生成
            var (view, button) = dataListManager.CreateDataListSync(templateView, content, true, true);

            //データの取得
            int index = i;
            var data = shopCategoriesList[index];
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{index}";

            //データの描画
            view.Set(view, shopCategoriesList[i], imagePath);
            button.onClick.AddListener(() => shopCategoryTemplateView.SetCategory(data.category));
        }
    }
}