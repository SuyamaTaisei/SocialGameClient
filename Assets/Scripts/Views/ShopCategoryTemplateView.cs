using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryTemplateView : MonoBehaviour
{
    [SerializeField] Image shopCategoryIcon;
    [SerializeField] TextMeshProUGUI shopCategoryText;

    //ビューの表示
    [SerializeField] GameObject shopItemList;
    [SerializeField] GameObject shopGemList;

    //購入ボタン
    [SerializeField] Button shopDetailMoneyButton;
    [SerializeField] Button shopDetailCoinButton;
    [SerializeField] Button shopDetailGemButton;

    [SerializeField] GameObject shopDetailAmountObject;

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
        view.shopCategoryText.text = data1.name;
        view.shopCategoryIcon.sprite = Resources.Load<Sprite>(imagePath);
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
        shopItemList.SetActive(isItem);
        shopGemList.SetActive(isGem);
        shopDetailCoinButton.gameObject.SetActive(coinBtn);
        shopDetailGemButton.gameObject.SetActive(gemBtn);
        shopDetailMoneyButton.gameObject.SetActive(moneyBtn);
        shopDetailAmountObject.SetActive(isItem);
    }
}
