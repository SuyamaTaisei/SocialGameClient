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

    private void Start()
    {
        List<ShopDataModel> shopList = ShopDataTable.SelectAll();

        for (int i = startCount; i <= maxCount; i++)
        {
            //雛形をもとにリストを生成
            GameObject item = Instantiate(templateView, content);

            //ボタンの取得
            Button button = item.GetComponentInChildren<Button>();

            //選択されたボタンの番号をセット
            int index1 = this.index1 + i;
            int index2 = this.index2 + i;

            //生成されたリストから商品名や価格をセットする
            ShopTempView view = item.GetComponent<ShopTempView>();
            if (view != null) view.Set(shopList[i]);

            //ボタン押下処理
            if (clientShop) button.onClick.AddListener(() => clientShop.OpenConfirmButton(index1, index2));
        }
    }
}