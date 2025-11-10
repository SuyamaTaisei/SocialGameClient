using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ResponseObjects
{
    public UsersModel users;
    public WalletsModel wallets;
}

public class ApiConnect : MonoBehaviour
{
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
        }
    }

    public IEnumerator Send(string endPoint, List<IMultipartFormSection> form, Action action = null, int timeOut = 10)
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
                    case GameUtility.Const.ERROR_DB_UPDATE:
                        Debug.LogError("サーバーでエラーが発生しました。[データベース更新エラー]");
                        break;
                    default:
                        Debug.LogError("サーバーでエラーが発生しました。[システムエラー]");
                        break;
                }
                yield break;
            }

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