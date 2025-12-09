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
    [SerializeField] int index1;
    [SerializeField] int index2;

    [SerializeField] string imageFolderName;
    [SerializeField] int imageIndex;

    private void Start()
    {
        List<ShopDataModel> shopList = ShopDataTable.SelectAll();

        for (int i = startCount; i <= maxCount; i++)
        {
            //選択されたボタンの番号をセット
            int index1 = this.index1 + i;
            int index2 = this.index2 + i;

            //雛形をもとにリストを生成
            GameObject item = Instantiate(templateView, content);

            Button button = item.GetComponentInChildren<Button>();
            ShopTempView view = item.GetComponent<ShopTempView>();

            //キャプチャしてから渡す
            int itemId = imageIndex;
            string imagePath = $"Images/{imageFolderName}/{itemId}";

            //処理
            if (clientShop) button.onClick.AddListener(() => clientShop.OpenConfirmButton(index1, index2, itemId));
            if (view != null) view.Set(shopList[i], imagePath);
            imageIndex++;
        }
    }
}