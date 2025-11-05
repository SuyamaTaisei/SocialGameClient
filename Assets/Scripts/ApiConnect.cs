using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ApiConnect : MonoBehaviour
{
    private const int timeOut = 10;

    public IEnumerator Send(string endPoint, List<IMultipartFormSection> form, Action action = null)
    {
        //POSTでデータを送信
        UnityWebRequest request = UnityWebRequest.Post(GameUtility.Const.SERVER_URL + endPoint, form);
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
            UsersModel userModel = JsonUtility.FromJson<UsersModel>(serverData);
            if (!string.IsNullOrEmpty(userModel.id))
            {
                Debug.Log("SQLiteへINSERTした");
                UsersTable.Insert(userModel);
            }
            else
            {
                Debug.Log("SQLiteへINSERTできなかった");
            }

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