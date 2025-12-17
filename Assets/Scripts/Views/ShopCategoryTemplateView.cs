using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryTemplateView : MonoBehaviour
{
    [SerializeField] Image categoryIcon;
    [SerializeField] TextMeshProUGUI categoryText;

    //ビューの表示
    [SerializeField] GameObject itemListView;
    [SerializeField] GameObject gemListView;

    //購入ボタン
    [SerializeField] Button buyMoneyButton;
    [SerializeField] Button buyItemCoinButton;
    [SerializeField] Button buyItemGemButton;

    private string imageFolderName;

    public string ImageFolderName => imageFolderName;

    private void Awake()
    {
        SetCategory(0);
    }

    private void Start()
    {
        SetButton(GameUtility.Const.FOLDER_NAME_GEMS, false, true, false, false, true);
    }

    //カテゴリ別ボタンの描画
    public void Set(ShopCategoryTemplateView view, ShopCategoriesModel data1, string imagePath)
    {
        view.categoryText.text = data1.name;
        view.categoryIcon.sprite = Resources.Load<Sprite>(imagePath);
    }

    //カテゴリ別ショップ内の描画
    public void SetCategory(int category)
    {
        switch (category)
        {
            case GameUtility.Const.SHOP_GEMS: SetButton(GameUtility.Const.FOLDER_NAME_GEMS, false, true, false, false, true);
                break;
            case GameUtility.Const.SHOP_ITEMS: SetButton(GameUtility.Const.FOLDER_NAME_ITEMS, true, false, true, true, false);
                break;
        }
    }

    //販売一覧の描画
    public void SetButton(string isName, bool isItem, bool isGem, bool coinBtn, bool gemBtn, bool moneyBtn)
    {
        imageFolderName = isName;
        itemListView.SetActive(isItem);
        gemListView.SetActive(isGem);
        buyItemCoinButton.gameObject.SetActive(coinBtn);
        buyItemGemButton.gameObject.SetActive(gemBtn);
        buyMoneyButton.gameObject.SetActive(moneyBtn);
    }
}
