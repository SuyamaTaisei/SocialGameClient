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

    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] ApiConnect apiConnect;

    private const string columnUserName = "user_name";
    private const string endPoint = "register";

    //DBモデル
    private UsersModel userModel;

    private void Awake()
    {
        // SQLiteのDBファイル作成
        string DBPath = Application.dataPath + "/StreamingAssets/" + GameUtility.Const.SQLITE_DB_NAME;
        if (!File.Exists(DBPath))
        {
            File.Create(DBPath);
        }
        // テーブル作成処理
        UsersTable.CreateTable();
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
        }
        //無ければ表示しない
        else
        {
            showUserName.text = "ユーザー：";
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
            string userName = inputUserName.text;
            Action action = new(() => RegisterComplete(true));
            //POST送信用のフォームを作成
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(columnUserName, userName)
            };
            StartCoroutine(apiConnect.Send(endPoint, form, action));
        }
    }

    //スタートボタン押下
    public void StartButton()
    {
        //ユーザー情報の取得
        userModel = UsersTable.Select();
        if (!string.IsNullOrEmpty(userModel.id))
        {
            //そのままログイン
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
}