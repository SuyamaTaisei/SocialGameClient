using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject templateView;
    [SerializeField] ClientShop clientShop;
    [SerializeField] ClientGacha clientGacha;

    [SerializeField] int startCount;
    [SerializeField] int maxCount;
    [SerializeField] int product_id1;
    [SerializeField] int product_id2;

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
            int index1 = product_id1 + i;
            int index2 = product_id2 + i;

            //生成されたリストから商品名や価格をセットする
            ShopItemView view = item.GetComponent<ShopItemView>();
            if (view != null) view.Set(shopList[i]);

            //ボタン押下処理
            if (clientShop) button.onClick.AddListener(() => clientShop.OpenConfirmButton(index1, index2));
            if (clientGacha) button.onClick.AddListener(() => clientGacha.GachaPeriodList(index1));
        }
    }
}