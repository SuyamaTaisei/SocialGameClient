using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientTitle : MonoBehaviour
{
    [SerializeField] GameObject StartView;
    [SerializeField] GameObject RegisterView;
    [SerializeField] GameObject RegisterCompleteView;

    [SerializeField] TextMeshProUGUI showUserName;
    [SerializeField] TextMeshProUGUI showId;

    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] ApiConnect apiConnect;

    private const string column_UserName   = "user_name";
    private const string column_id         = "id";

    //DBモデル
    private UsersModel userModel;

    private void Awake()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.persistentDataPath + "/" + GameUtility.Const.SQLITE_DB_NAME;
        if (!File.Exists(DBPath))
        {
            Debug.Log(DBPath);
            File.Create(DBPath).Close();
        }
        // テーブル作成処理
        UsersTable.CreateTable();
        WalletsTable.CreateTable();
    }

    void Start()
    {
        StartView.SetActive(true);
        RegisterView.SetActive(false);
        RegisterCompleteView.SetActive(false);

        userModel = UsersTable.Select();
        //既にユーザー情報があれば情報表示
        if (!string.IsNullOrEmpty(userModel.id))
        {
            showUserName.text = "ユーザー：" + userModel.user_name;
            showId.text = "ID : " + userModel.id;
        }
        //無ければ表示しない
        else
        {
            showUserName.text = "ユーザー：";
            showId.text = "ID : ";
        }
    }

    private void Update()
    {
        userModel = UsersTable.Select();
        //既にユーザー情報があれば情報表示
        if (!string.IsNullOrEmpty(userModel.id))
        {
            showUserName.text = "ユーザー：" + userModel.user_name;
            showId.text = "ID : " + userModel.id;
        }
        //無ければ表示しない
        else
        {
            showUserName.text = "ユーザー：";
            showId.text = "ID : ";
        }
    }

    //登録ボタン押下
    public void RegisterButton()
    {
        if (string.IsNullOrEmpty(inputUserName.text))
        {
            //ユーザ名未入力
            warningText.text = "正しく入力してください";
        }
        else if (inputUserName.text.Length <= 3)
        {
            //ユーザ名が指定文字数以上の場合
            warningText.text = "4文字以上で入力してください";
        }
        else
        {
            RegisterView.SetActive(false);
            Action action = new(() => RegisterComplete(true));

            string userName = inputUserName.text;
            List<IMultipartFormSection> form = new() //POST送信用のフォームを作成
            {
                new MultipartFormDataSection(column_UserName, userName)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.REGISTER_URL, form, action));
        }
    }

    //スタートボタン押下
    public void StartButton()
    {
        //ユーザー情報の取得
        userModel = UsersTable.Select();
        if (!string.IsNullOrEmpty(userModel.id))
        {
            Action action = new(() => SceneTransition());

            //そのままログイン
            string id = userModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.LOGIN_URL, form, action));

            StartView.SetActive(true);
            RegisterView.SetActive(false);
        }
        else
        {
            //アカウント登録画面表示
            StartView.SetActive(false);
            RegisterView.SetActive(true);
        }
    }

    //アカウント登録完了画面
    public void RegisterComplete(bool enabled) => RegisterCompleteView.SetActive(enabled);

    //アカウント登録完了ボタン
    public void RegisterCompleteButton()
    {
        StartView.SetActive(true);
        RegisterComplete(false);
    }

    //シーン遷移
    public void SceneTransition() => LoadingManager.Instance.LoadScene("HomeScene");
}