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
    public GachaResultsModel[] exchange_items;
    public GachaResultsModel[] single_exchange_items;
}

public class ResponseManager : MonoBehaviour
{
    private ClientShop clientShop;
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
            Debug.Log("SQLiteへINSERTした");
            UsersTable.Insert(responseObjects.users);
            WalletsTable.Insert(responseObjects.wallets);
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
        }
    }

    public void ExecuteLogin(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("SQLiteへINSERTした");
            UsersTable.Insert(responseObjects.users);
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
        }
    }

    public void ExecuteHome(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("SQLiteへINSERTした");
            WalletsTable.Insert(responseObjects.wallets);
            ItemInstacesTable.Insert(responseObjects.item_instances);
            CharacterInstancesTable.Insert(responseObjects.character_instances);
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
        }
    }

    public void ExecuteGacha(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            Debug.Log("ガチャSQLiteへINSERTした");
            CharacterInstancesTable.Insert(responseObjects.character_instances);
            ItemInstacesTable.Insert(responseObjects.item_instances);

            //ガチャ結果の表示(非アクティブ状態でも取得)
            gachaResultList = FindAnyObjectByType<GachaResultList>(FindObjectsInactive.Include);
            gachaRewardList = FindAnyObjectByType<GachaRewardList>(FindObjectsInactive.Include);
            if(gachaResultList != null && responseObjects.gacha_results != null)
            {
                gachaResultList.ShowGachaResult(responseObjects.gacha_results, responseObjects.new_characters, responseObjects.single_exchange_items);
            }
            //変換されたガチャ報酬の表示
            if(gachaRewardList != null && responseObjects.exchange_items != null)
            {
                gachaRewardList.ShowGachaReward(responseObjects.exchange_items);
            }
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
        }
    }

    public void ExecuteMasterDataCheck(ResponseObjects responseObjects)
    {
        Debug.Log(responseObjects.master_data_version);
        int error_number = int.Parse(GameUtility.Const.ERRCODE_MASTER_DATA_UPDATE);
        if (responseObjects.master_data_version != error_number)
        {
            Debug.Log("マスターデータバージョンを保存");
            MasterDataManager.SetMasterDataVersion(responseObjects.master_data_version);
        }
        else if (responseObjects.master_data_version == error_number)
        {
            Debug.Log("マスターデータバージョンを保存できなかった");
        }
    }

    public void ExecuteMasterData(ResponseObjects responseObjects)
    {
        if (responseObjects.shop_categories != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ShopCategoriesTable.Insert(responseObjects.shop_categories);
        }
        if (responseObjects.shop_data != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ShopDataTable.Insert(responseObjects.shop_data);
        }
        if (responseObjects.shop_rewards != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ShopRewardsTable.Insert(responseObjects.shop_rewards);
        }

        if (responseObjects.character_categories != null)
        {
            Debug.Log("SQLiteへINSERTした");
            CharacterCategoriesTable.Insert(responseObjects.character_categories);
        }
        if (responseObjects.character_data != null)
        {
            Debug.Log("SQLiteへINSERTした");
            CharacterDataTable.Insert(responseObjects.character_data);
        }
        if (responseObjects.character_rarities != null)
        {
            Debug.Log("SQLiteへINSERTした");
            CharacterRaritiesTable.Insert(responseObjects.character_rarities);
        }

        if (responseObjects.item_categories != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ItemCategoriesTable.Insert(responseObjects.item_categories);
        }
        if (responseObjects.item_data != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ItemDataTable.Insert(responseObjects.item_data);
        }
        if (responseObjects.item_rarities != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ItemRaritiesTable.Insert(responseObjects.item_rarities);
        }

        if (responseObjects.gacha_periods != null)
        {
            Debug.Log("SQLiteへINSERTした");
            GachaPeriodsTable.Insert(responseObjects.gacha_periods);
        }
        if (responseObjects.gacha_data != null)
        {
            Debug.Log("SQLiteへINSERTした");
            GachaDataTable.Insert(responseObjects.gacha_data);
        }
    }

    public void ExecutePaymentError(ResponseObjects responseObjects)
    {
        clientShop = FindAnyObjectByType<ClientShop>();
        clientGacha = FindAnyObjectByType<ClientGacha>();        

        if (responseObjects.errcode == int.Parse(GameUtility.Const.ERRCODE_NOT_PAYMENT))
        {
            Debug.Log("残高不足");
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
            clientShop.WarningMessage("");
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
            case GameUtility.Const.MASTER_DATA_CHECK_URL:
                ExecuteMasterDataCheck(responseObjects);
                break;
            case GameUtility.Const.MASTER_DATA_GET_URL:
                ExecuteMasterData(responseObjects);
                break;
            case GameUtility.Const.PAYMENT_URL:
                ExecuteHome(responseObjects);
                ExecutePaymentError(responseObjects);
                break;
            case GameUtility.Const.GACHA_EXECUTE_URL:
                ExecuteHome(responseObjects);
                ExecuteGacha(responseObjects);
                ExecutePaymentError(responseObjects);
                break;
        }
    }
}