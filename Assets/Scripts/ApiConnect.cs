using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ApiConnect : MonoBehaviour
{
    private ResponseManager responseManager;
    private ClientMasterData clientMasterData;
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

    private void Start()
    {
        responseManager = ResponseManager.Instance;
        clientMasterData = FindFirstObjectByType<ClientMasterData>();
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
            Debug.Log("レスポンス完了");

            //サーバーエラーチェック
            string serverData = request.downloadHandler.text;
            if (serverData.All(char.IsNumber))
            {
                switch (serverData)
                {
                    case GameUtility.Const.ERRCODE_MASTER_DATA_UPDATE:
                        Debug.LogError("ゲームをアップデートしてください。");
                        clientMasterData.MasterDataWarningUpdate(GameUtility.Const.ERROR_MASTER_DATA_VERSION_TEXT);
                        break;
                    case GameUtility.Const.ERRCODE_DB_UPDATE:
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

            //ここでもう一度確実に取得
            responseManager = ResponseManager.Instance;
            responseManager.ExecuteObjects(endPoint, responseObjects);

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