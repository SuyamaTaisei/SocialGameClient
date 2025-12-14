using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientShop clientShop;

    private void Start()
    {
        List<ShopCategoriesModel> shopCategoriesList = ShopCategoriesTable.SelectAll();

        for (int i = 0; i < shopCategoriesList.Count; i++)
        {
            GameObject item = Instantiate(templateView, content);
            Button button = item.GetComponentInChildren<Button>();
            var view = item.GetComponent<ShopCategoryTemplateView>();
            int index = i;
            string imagePath = $"{GameUtility.Const.FOLDER_NAME_IMAGES}/{GameUtility.Const.FOLDER_NAME_ITEMS}/{index}";

            //表記
            view.ShopCategoryText.text = shopCategoriesList[index].name;
            view.ShopCategoryIcon.sprite = Resources.Load<Sprite>(imagePath);
            if (clientShop) button.onClick.AddListener(() => clientShop.ShowShopCategoryList(index));
        }
    }
}