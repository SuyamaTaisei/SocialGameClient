using System;
using UnityEngine;

[Serializable]
public class ResponseObjects
{
    public int master_data_version;
    public int errcode;

    public UsersModel users;
    public WalletsModel wallets;
    public CharacterInstancesModel[] character_instances;
    public ItemInstancesModel[] item_instances;

    public ShopCategoriesModel[] shop_categories;
    public ShopDataModel[] shop_data;
    public ShopRewardsModel[] shop_rewards;

    public CharacterCategoriesModel[] character_categories;
    public CharacterDataModel[] character_data;
    public CharacterRaritiesModel[] character_rarities;

    public ItemCategoriesModel[] item_categories;
    public ItemDataModel[] item_data;
    public ItemRaritiesModel[] item_rarities;

    public GachaPeriodsModel[] gacha_periods;
    public GachaDataModel[] gacha_data;

    public GachaResultsModel[] gacha_results;
    public GachaResultsModel[] new_characters;
    public GachaResultsModel[] total_exchange_items;
    public GachaResultsModel[] single_exchange_items;

    public GachaLogsModel[] gacha_logs;
}

public class ResponseManager : MonoBehaviour
{
    private ClientShop clientShop;
    private ShopDetailFixedView shopConfirmFixedView;
    private ClientGacha clientGacha;
    private GachaResultList gachaResultList;
    private GachaRewardList gachaRewardList;
    public static ResponseManager Instance { get; private set; }

    private void Awake()
    {
        //ゲーム内に一つだけ存在
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ExecuteRegister(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("アカウント登録完了");
            UsersTable.Insert(responseObjects.users);
            WalletsTable.Insert(responseObjects.wallets);
        }
        else
        {
            Debug.LogError("アカウント登録できなかった");
        }
    }

    public void ExecuteLogin(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("ログイン完了");
            UsersTable.Insert(responseObjects.users);
            GachaLogsTable.Insert(responseObjects.gacha_logs);
        }
        else
        {
            Debug.LogError("ログインできなかった");
        }
    }

    public void ExecuteHome(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("ホーム更新完了");
            if (responseObjects.users != null)
            {
                UsersTable.Insert(responseObjects.users);
            }
            if (responseObjects.wallets != null)
            {
                WalletsTable.Insert(responseObjects.wallets);
            }
            if (responseObjects.item_instances != null)
            {
                ItemInstacesTable.Insert(responseObjects.item_instances);
            }
            if (responseObjects.character_instances != null)
            {
                CharacterInstancesTable.Insert(responseObjects.character_instances);
            }
        }
        else
        {
            Debug.LogError("ホーム更新できなかった");
        }
    }

    public void ExecuteGacha(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("ガチャ実行完了");
            CharacterInstancesTable.Insert(responseObjects.character_instances);
            ItemInstacesTable.Insert(responseObjects.item_instances);

            //ガチャ結果の表示(非アクティブ状態でも取得)
            gachaResultList = FindAnyObjectByType<GachaResultList>(FindObjectsInactive.Include);
            gachaRewardList = FindAnyObjectByType<GachaRewardList>(FindObjectsInactive.Include);
            if(gachaResultList != null && responseObjects.gacha_results != null)
            {
                gachaResultList.DataList(responseObjects.gacha_results, responseObjects.new_characters, responseObjects.single_exchange_items);
            }
            //変換されたガチャ報酬の表示
            if(gachaRewardList != null && responseObjects.total_exchange_items != null)
            {
                gachaRewardList.DataList(responseObjects.total_exchange_items);
            }
            //ガチャログ実行
            if (responseObjects.gacha_logs != null)
            {
                Debug.Log("ガチャログ実行完了");
                GachaLogsTable.Insert(responseObjects.gacha_logs);
            }
        }
        else
        {
            Debug.LogError("ガチャ実行できなかった");
        }
    }

    public void ExecuteMasterData(ResponseObjects responseObjects)
    {
        if (responseObjects.shop_categories != null)
        {
            Debug.Log("マスタデータ更新完了(ショップカテゴリ)");
            ShopCategoriesTable.Insert(responseObjects.shop_categories);
        }
        if (responseObjects.shop_data != null)
        {
            Debug.Log("マスタデータ更新完了(ショップデータ)");
            ShopDataTable.Insert(responseObjects.shop_data);
        }
        if (responseObjects.shop_rewards != null)
        {
            Debug.Log("マスタデータ更新完了(ショップ提供報酬)");
            ShopRewardsTable.Insert(responseObjects.shop_rewards);
        }

        if (responseObjects.character_categories != null)
        {
            Debug.Log("マスタデータ更新完了(キャラクターカテゴリ)");
            CharacterCategoriesTable.Insert(responseObjects.character_categories);
        }
        if (responseObjects.character_data != null)
        {
            Debug.Log("マスタデータ更新完了(キャラクターデータ)");
            CharacterDataTable.Insert(responseObjects.character_data);
        }
        if (responseObjects.character_rarities != null)
        {
            Debug.Log("マスタデータ更新完了(キャラクターレアリティ)");
            CharacterRaritiesTable.Insert(responseObjects.character_rarities);
        }

        if (responseObjects.item_categories != null)
        {
            Debug.Log("マスタデータ更新完了(アイテムカテゴリ)");
            ItemCategoriesTable.Insert(responseObjects.item_categories);
        }
        if (responseObjects.item_data != null)
        {
            Debug.Log("マスタデータ更新完了(アイテムデータ)");
            ItemDataTable.Insert(responseObjects.item_data);
        }
        if (responseObjects.item_rarities != null)
        {
            Debug.Log("マスタデータ更新完了(アイテムレアリティ)");
            ItemRaritiesTable.Insert(responseObjects.item_rarities);
        }

        if (responseObjects.gacha_periods != null)
        {
            Debug.Log("マスタデータ更新完了(ガチャ期間)");
            GachaPeriodsTable.Insert(responseObjects.gacha_periods);
        }
        if (responseObjects.gacha_data != null)
        {
            Debug.Log("マスタデータ更新完了(ガチャデータ)");
            GachaDataTable.Insert(responseObjects.gacha_data);
        }
    }

    public void ExecutePayment(ResponseObjects responseObjects)
    {
        clientShop = FindAnyObjectByType<ClientShop>();
        clientGacha = FindAnyObjectByType<ClientGacha>();        
        shopConfirmFixedView = FindAnyObjectByType<ShopDetailFixedView>(FindObjectsInactive.Include);

        if (responseObjects.errcode == int.Parse(GameUtility.Const.ERRCODE_NOT_PAYMENT))
        {
            Debug.LogError("残高不足");
            clientShop.WarningMessage(GameUtility.Const.ERROR_PAYMENT_1);
            clientGacha.WarningMessage(GameUtility.Const.ERROR_PAYMENT_1);
        }
        else if (responseObjects.errcode == int.Parse(GameUtility.Const.ERRCODE_LIMIT_WALLETS))
        {
            Debug.Log("これ以上ウォレットを増やせない");
            clientShop.WarningMessage(GameUtility.Const.ERROR_PAYMENT_2);
            clientGacha.WarningMessage(GameUtility.Const.ERROR_PAYMENT_2);
        }
        else
        {
            Debug.Log("支払い完了");
            clientShop.WarningMessage("");
            shopConfirmFixedView.SetShopDetailClose();
            shopConfirmFixedView.SetBuyConfirmClose();
            shopConfirmFixedView.SetPaymentComplete(true);
            clientGacha.WarningMessage("");
            clientGacha.GachaConfirmClose();
        }
    }

    public void ExecuteObjects(string endPoint, ResponseObjects responseObjects)
    {
        switch (endPoint)
        {
            case GameUtility.Const.REGISTER_URL:
                ExecuteRegister(responseObjects);
                break;
            case GameUtility.Const.LOGIN_URL:
                ExecuteLogin(responseObjects);
                break;
            case GameUtility.Const.HOME_URL:
                ExecuteHome(responseObjects);
                break;
            case GameUtility.Const.MASTER_DATA_GET_URL:
                ExecuteMasterData(responseObjects);
                break;
            case GameUtility.Const.PAYMENT_URL:
                ExecuteHome(responseObjects);
                ExecutePayment(responseObjects);
                break;
            case GameUtility.Const.GACHA_EXECUTE_URL:
                ExecuteHome(responseObjects);
                ExecuteGacha(responseObjects);
                ExecutePayment(responseObjects);
                break;
            case GameUtility.Const.CHARACTER_ENHANCE_URL:
                ExecuteHome(responseObjects);
                break;
            case GameUtility.Const.STAMINA_DECREASE_URL:
                ExecuteHome(responseObjects);
                break;
            case GameUtility.Const.STAMINA_INCREASE_URL:
                ExecuteHome(responseObjects);
                ExecutePayment(responseObjects);
                break;
            case GameUtility.Const.STAMINA_AUTO_INCREASE_URL:
                ExecuteHome(responseObjects);
                break;
        }
    }
}