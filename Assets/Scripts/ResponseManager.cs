using System;
using UnityEngine;

[Serializable]
public class ResponseObjects
{
    public int master_data_version;
    public UsersModel users;
    public WalletsModel wallets;
    public ShopCategoryModel[] shop_category;
    public ShopDataModel[] shop_data;
}

public class ResponseManager : MonoBehaviour
{
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
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
        }
    }

    public void ExecuteMasterDataCheck(ResponseObjects responseObjects)
    {
        Debug.Log(responseObjects.master_data_version);
        int error_number = int.Parse(GameUtility.Const.ERROR_MASTER_DATA_UPDATE);
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

    public void ExecuteShopCategory(ResponseObjects responseObjects)
    {
        if (responseObjects.shop_category != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ShopCategoriesTable.Insert(responseObjects.shop_category);
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
        }
    }

    public void ExecuteShopData(ResponseObjects responseObjects)
    {
        if (responseObjects.shop_data != null)
        {
            Debug.Log("SQLiteへINSERTした");
            ShopDataTable.Insert(responseObjects.shop_data);
        }
        else
        {
            Debug.Log("SQLiteへINSERTできなかった");
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
                ExecuteShopCategory(responseObjects);
                ExecuteShopData(responseObjects);
                break;
            case GameUtility.Const.PAYMENT_URL:
                ExecuteHome(responseObjects);
                break;
        }
    }
}