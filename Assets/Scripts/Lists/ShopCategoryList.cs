using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientShop clientShop;
    [SerializeField] ShopCategoryTemplateView shopCategoryTemplateView;

    private void Start()
    {
        List<ShopCategoriesModel> shopCategoriesList = ShopCategoriesTable.SelectAll();

        for (int i = 0; i < shopCategoriesList.Count; i++)
        {
            //データの生成
            var item = Instantiate(templateView, content);
            var button = item.GetComponentInChildren<Button>();
            var view = item.GetComponent<ShopCategoryTemplateView>();

            //データの取得
            int index = i;
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{index}";
            var data = shopCategoriesList[index];
            var category = data.category;

            //データの描画
            view.Set(view, shopCategoriesList[i], imagePath);
            button.onClick.AddListener(() => shopCategoryTemplateView.SetCategory(category));
        }
    }
}