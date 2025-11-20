using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientTitle : MonoBehaviour
{
    [SerializeField] GameObject startView;
    [SerializeField] GameObject registerView;
    [SerializeField] GameObject registerCompleteView;

    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI idText;

    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI warningText;

    [SerializeField] ApiConnect apiConnect;
    [SerializeField] ClientMasterData clientMasterData;

    public GameObject StartView => startView;

    private const string column_UserName = "user_name";
    private const string column_id       = "id";

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
        CreateTables();
    }

    void Start()
    {
        StartView.SetActive(true);
        registerView.SetActive(false);
        registerCompleteView.SetActive(false);

        ShowUserInfo();
    }

    private void Update()
    {
        ShowUserInfo();
    }

    //アカウント登録ボタン
    public void RegisterButton()
    {
        if (string.IsNullOrEmpty(inputUserName.text))
        {
            //ユーザ名未入力
            warningText.text = GameUtility.Const.ERROR_VALIDATE_1;
        }
        else if (inputUserName.text.Length <= 3)
        {
            //ユーザ名が指定文字数以上の場合
            warningText.text = GameUtility.Const.ERROR_VALIDATE_2;
        }
        else
        {
            registerView.SetActive(false);
            Action action = new(() => RegisterComplete(true));

            string userName = inputUserName.text;
            List<IMultipartFormSection> form = new() //POST送信用のフォームを作成
            {
                new MultipartFormDataSection(column_UserName, userName)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.REGISTER_URL, form, action));
        }
    }

    //スタートボタン
    public void StartButton()
    {
        //ユーザー情報の取得
        userModel = UsersTable.Select();
        if (!string.IsNullOrEmpty(userModel.id))
        {
            Action action = new(() => clientMasterData.MasterDataCheck());

            //そのままログイン
            string id = userModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.LOGIN_URL, form, action));

            StartView.SetActive(true);
            registerView.SetActive(false);
        }
        else
        {
            //アカウント登録画面表示
            StartView.SetActive(false);
            registerView.SetActive(true);
        }
    }

    //アカウント登録完了ボタン
    public void RegisterCompleteButton()
    {
        StartView.SetActive(true);
        RegisterComplete(false);
    }

    //アカウント登録完了画面
    public void RegisterComplete(bool enabled) => registerCompleteView.SetActive(enabled);

    //ユーザー情報表示
    public void ShowUserInfo()
    {
        userModel = UsersTable.Select();
        //既にユーザー情報があれば情報表示
        if (!string.IsNullOrEmpty(userModel.id))
        {
            userNameText.text = GameUtility.Const.SHOW_USER + userModel.user_name;
            idText.text = GameUtility.Const.SHOW_ID + userModel.id;
        }
        //無ければ表示しない
        else
        {
            userNameText.text = GameUtility.Const.SHOW_USER;
            idText.text = GameUtility.Const.SHOW_ID;
        }
    }

    //テーブル作成
    public void CreateTables()
    {
        UsersTable.CreateTable();
        WalletsTable.CreateTable();
        CharacterInstancesTable.CreateTable();
        ItemInstacesTable.CreateTable();

        ShopCategoriesTable.CreateTable();
        ShopDataTable.CreateTable();
        
        CharacterCategoriesTable.CreateTable();
        CharacterDataTable.CreateTable();
        CharacterRaritiesTable.CreateTable();

        ItemCategoriesTable.CreateTable();
        ItemDataTable.CreateTable();
        ItemRaritiesTable.CreateTable();
    }
}