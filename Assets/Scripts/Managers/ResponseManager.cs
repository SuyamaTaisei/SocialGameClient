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
    public PresentInstancesModel[] present_instances;
    public MissionInstancesModel[] mission_instances;

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

    public PresentCategoriesModel[] present_categories;

    public MissionCategoriesModel[] mission_categories;
    public MissionDataModel[] mission_data;

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
    private GachaFixedView gachaFixedView;
    private ClientGacha clientGacha;
    private GachaResultList gachaResultList;
    private GachaRewardList gachaRewardList;
    private InstancePresentFixedView instancePresentFixedView;
    public static ResponseManager Instance { get; private set; }

    private void Awake()
    {
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
            UsersTable.Insert(responseObjects.users);
            WalletsTable.Insert(responseObjects.wallets);
        }
    }

    public void ExecuteLogin(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            UsersTable.Insert(responseObjects.users);
            GachaLogsTable.Insert(responseObjects.gacha_logs);
        }
    }

    public void ExecuteHome(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
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
                ItemInstancesTable.Insert(responseObjects.item_instances);
            }
            if (responseObjects.character_instances != null)
            {
                CharacterInstancesTable.Insert(responseObjects.character_instances);
            }
            if (responseObjects.present_instances != null)
            {
                PresentInstancesTable.InsertFromDelete(responseObjects.users.manage_id, responseObjects.present_instances);
            }
            if (responseObjects.mission_instances != null)
            {
                MissionInstancesTable.Insert(responseObjects.mission_instances);
            }
        }
    }

    public async void ExecuteGacha(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
            CharacterInstancesTable.Insert(responseObjects.character_instances);
            ItemInstancesTable.Insert(responseObjects.item_instances);

            //ガチャ結果の表示(非アクティブ状態でも取得)
            gachaResultList = FindAnyObjectByType<GachaResultList>(FindObjectsInactive.Include);
            gachaRewardList = FindAnyObjectByType<GachaRewardList>(FindObjectsInactive.Include);
            if(gachaResultList != null && responseObjects.gacha_results != null)
            {
                await gachaResultList.DataList(responseObjects.gacha_results, responseObjects.new_characters, responseObjects.single_exchange_items);
            }
            //変換されたガチャ報酬の表示
            if(gachaRewardList != null && responseObjects.total_exchange_items != null)
            {
                gachaRewardList.DataList(responseObjects.total_exchange_items);
            }
            //ガチャログ実行
            if (responseObjects.gacha_logs != null)
            {
                GachaLogsTable.Insert(responseObjects.gacha_logs);
            }
        }
    }

    public void ExecuteEnhance(ResponseObjects responseObjects)
    {
        if (!string.IsNullOrEmpty(responseObjects.users.id))
        {
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
                ItemInstancesTable.InsertFromDelete(responseObjects.users.manage_id, responseObjects.item_instances);
            }
            if (responseObjects.character_instances != null)
            {
                CharacterInstancesTable.Insert(responseObjects.character_instances);
            }
            if (responseObjects.mission_instances != null)
            {
                MissionInstancesTable.Insert(responseObjects.mission_instances);
            }
        }
    }

    public void ExecuteMasterData(ResponseObjects responseObjects)
    {
        if (responseObjects.shop_categories != null)
        {
            ShopCategoriesTable.Insert(responseObjects.shop_categories);
        }
        if (responseObjects.shop_data != null)
        {
            ShopDataTable.Insert(responseObjects.shop_data);
        }
        if (responseObjects.shop_rewards != null)
        {
            ShopRewardsTable.Insert(responseObjects.shop_rewards);
        }

        if (responseObjects.character_categories != null)
        {
            CharacterCategoriesTable.Insert(responseObjects.character_categories);
        }
        if (responseObjects.character_data != null)
        {
            CharacterDataTable.Insert(responseObjects.character_data);
        }
        if (responseObjects.character_rarities != null)
        {
            CharacterRaritiesTable.Insert(responseObjects.character_rarities);
        }

        if (responseObjects.item_categories != null)
        {
            ItemCategoriesTable.Insert(responseObjects.item_categories);
        }
        if (responseObjects.item_data != null)
        {
            ItemDataTable.Insert(responseObjects.item_data);
        }
        if (responseObjects.item_rarities != null)
        {
            ItemRaritiesTable.Insert(responseObjects.item_rarities);
        }

        if (responseObjects.gacha_periods != null)
        {
            GachaPeriodsTable.Insert(responseObjects.gacha_periods);
        }
        if (responseObjects.gacha_data != null)
        {
            GachaDataTable.Insert(responseObjects.gacha_data);
        }

        if (responseObjects.present_categories != null)
        {
            PresentCategoriseTable.Insert(responseObjects.present_categories);
        }

        if (responseObjects.mission_categories != null)
        {
            MissionCategoriesTable.Insert(responseObjects.mission_categories);
        }
        if (responseObjects.mission_data != null)
        {
            MissionDataTable.Insert(responseObjects.mission_data);
        }
    }

    public void ExecutePayment(ResponseObjects responseObjects)
    {
        clientShop = FindAnyObjectByType<ClientShop>();
        clientGacha = FindAnyObjectByType<ClientGacha>();        
        shopConfirmFixedView = FindAnyObjectByType<ShopDetailFixedView>(FindObjectsInactive.Include);
        gachaFixedView = FindFirstObjectByType<GachaFixedView>(FindObjectsInactive.Include);

        if (responseObjects.errcode == int.Parse(GameUtility.Const.ERRCODE_NOT_PAYMENT))
        {
            clientShop.WarningMessage(GameUtility.Const.ERROR_PAYMENT_1);
            clientGacha.WarningMessage(GameUtility.Const.ERROR_PAYMENT_1);
        }
        else if (responseObjects.errcode == int.Parse(GameUtility.Const.ERRCODE_LIMIT_WALLETS))
        {
            clientShop.WarningMessage(GameUtility.Const.ERROR_PAYMENT_2);
            clientGacha.WarningMessage(GameUtility.Const.ERROR_PAYMENT_2);
        }
        else
        {
            clientShop.WarningMessage("");
            shopConfirmFixedView.SetShopDetailClose();
            shopConfirmFixedView.SetBuyConfirmClose();
            shopConfirmFixedView.SetPaymentComplete(true);
            clientGacha.WarningMessage("");
            gachaFixedView.SetGachaConfirmClose();
        }
    }

    public void ExecutePresent(ResponseObjects responseObjects)
    {
        instancePresentFixedView = FindFirstObjectByType<InstancePresentFixedView>(FindObjectsInactive.Include);

        if (responseObjects.errcode == int.Parse(GameUtility.Const.ERRCODE_PRESENT_RECEIVED))
        {
            instancePresentFixedView.SetCompleteText(GameUtility.Const.ERROR_PRESENT_RECEIVED);
        }
        else
        {
            instancePresentFixedView.SetCompleteText(GameUtility.Const.SHOW_PRESENT_RECEIVED);
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
                ExecuteEnhance(responseObjects);
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
            case GameUtility.Const.PRESENT_RECEIVED_URL:
                ExecuteHome(responseObjects);
                ExecutePresent(responseObjects);
                break;
            case GameUtility.Const.MISSION_RECEIVED_URL:
                ExecuteHome(responseObjects);
                break;
        }
    }
}