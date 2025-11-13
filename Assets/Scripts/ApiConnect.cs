using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ResponseObjects
{
    public int master_data_version;
    public UsersModel users;
    public WalletsModel wallets;
    public ShopCategoryModel[] shop_category;
    public ShopDataModel[] shop_data;
}

public class ApiConnect : MonoBehaviour
{
    [SerializeField] ClientMasterData clientMasterData;
    public static ApiConnect Instance { get; private set; }

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
        switch(endPoint)
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
        }
    }

    public IEnumerator Send(string endPoint, List<IMultipartFormSection> form = null, Action action = null, int timeOut = 10)
    {
        //POSTでデータを送信
        UnityWebRequest request = UnityWebRequest.Post(endPoint, form);
        request.timeout = timeOut;
        yield return request.SendWebRequest();

        //レスポンスが成功したら
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("レスポンス成功");

            //サーバーエラーチェック
            string serverData = request.downloadHandler.text;
            if (serverData.All(char.IsNumber))
            {
                switch (serverData)
                {
                    case GameUtility.Const.ERROR_MASTER_DATA_UPDATE:
                        Debug.LogError("ゲームをアップデートしてください。");
                        clientMasterData.MasterDataWarningUpdate("ゲームをアップデートしてください。");
                        break;
                    case GameUtility.Const.ERROR_DB_UPDATE:
                        Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
                        break;
                    default:
                        Debug.LogError("サーバーでエラーが発生しました。[システムエラー]");
                        break;
                }
                yield break;
            }
            Debug.Log(serverData);

            //SQLiteへ保存。JSONデータをオブジェクトに変換
            ResponseObjects responseObjects = JsonUtility.FromJson<ResponseObjects>(serverData);
            ExecuteObjects(endPoint, responseObjects);

            //レスポンス成功時に、関数があれば実行
            if (action != null)
            {
                action();
                action = null;
            }
        }
        //失敗したら
        else
        {
            //エラーの場合
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
                yield break;
            }
        }
    }
}