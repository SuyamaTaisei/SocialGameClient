using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] ClientShop clientShop;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int product_id1;
    [SerializeField] int product_id2;

    private void Start()
    {
        List<ShopDataModel> list = ShopDataTable.SelectAll();

        for (int i = startCount; i < list.Count && i <= maxCount; i++)
        {
            GameObject item = Instantiate(itemPrefab, content);

            //ボタンの取得
            Button button = item.GetComponentInChildren<Button>();

            //選択されたボタンの番号をセット
            int index1 = product_id1 + i;
            int index2 = product_id2 + i;

            //生成されたリストから商品名や価格をセットする
            ShopItemView view = item.GetComponent<ShopItemView>();
            if (view != null)
            {
                view.Set(list[i]);
            }

            //ボタン押下処理
            button.onClick.AddListener(() => clientShop.OpenConfirmButton(index1, index2));
        }
    }
}