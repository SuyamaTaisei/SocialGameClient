using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientTitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI idText;

    [SerializeField] Button registerSendButton;
    [SerializeField] Button startButton;
    [SerializeField] Button registerCompleteButton;

    [SerializeField] GameObject startView;
    [SerializeField] GameObject registerView;
    [SerializeField] GameObject registerCompleteView;

    [SerializeField] TMP_InputField registerInputNameText;
    [SerializeField] TextMeshProUGUI registerWarningText;

    [SerializeField] ClientMasterData clientMasterData;
    private ApiConnect apiConnect;
    private UsersModel usersModel;

    private const string column_UserName = "user_name";
    private const string column_id = "id";

    public GameObject StartView => startView;

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
        apiConnect = ApiConnect.Instance;

        ShowUserInfo();

        StartView.SetActive(true);
        registerView.SetActive(false);
        registerCompleteView.SetActive(false);

        registerSendButton.onClick.AddListener(() => Register());
        startButton.onClick.AddListener(() => GameStart());
        registerCompleteButton.onClick.AddListener(() => RegisterCompleteExecute());
    }

    private void Update()
    {
        ShowUserInfo();
    }

    //アカウント登録ボタン
    public void Register()
    {
        if (string.IsNullOrEmpty(registerInputNameText.text))
        {
            //ユーザ名未入力
            registerWarningText.text = GameUtility.Const.ERROR_VALIDATE_1;
        }
        else if (registerInputNameText.text.Length <= GameUtility.Const.NUMBER_VALIDATE_1)
        {
            //ユーザ名が指定文字数以上の場合
            registerWarningText.text = GameUtility.Const.ERROR_VALIDATE_2;
        }
        else
        {
            registerView.SetActive(false);

            string userName = registerInputNameText.text;
            List<IMultipartFormSection> form = new() //POST送信用のフォームを作成
            {
                new MultipartFormDataSection(column_UserName, userName)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.REGISTER_URL, form, (action) =>
            {
                RegisterComplete(true);
            }));
        }
    }

    //スタートボタン
    public void GameStart()
    {
        //ユーザー情報の取得
        usersModel = UsersTable.Select();
        if (!string.IsNullOrEmpty(usersModel.id))
        {
            //そのままログイン
            string id = usersModel.id;
            List<IMultipartFormSection> form = new()
            {
                new MultipartFormDataSection(column_id, id)
            };
            StartCoroutine(apiConnect.Send(GameUtility.Const.LOGIN_URL, form, (action) =>
            {
                clientMasterData.MasterDataCheck();
            }));

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
    public void RegisterCompleteExecute()
    {
        StartView.SetActive(true);
        RegisterComplete(false);
    }

    //アカウント登録完了画面
    public void RegisterComplete(bool enabled) => registerCompleteView.SetActive(enabled);

    //ユーザー情報表示
    public void ShowUserInfo()
    {
        usersModel = UsersTable.Select();
        //既にユーザー情報があれば情報表示
        if (!string.IsNullOrEmpty(usersModel.id))
        {
            userNameText.text = GameUtility.Const.SHOW_USER + usersModel.user_name;
            idText.text = GameUtility.Const.SHOW_ID + usersModel.id;
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
        ShopRewardsTable.CreateTable();        

        CharacterCategoriesTable.CreateTable();
        CharacterDataTable.CreateTable();
        CharacterRaritiesTable.CreateTable();

        ItemCategoriesTable.CreateTable();
        ItemDataTable.CreateTable();
        ItemRaritiesTable.CreateTable();

        GachaPeriodsTable.CreateTable();
        GachaDataTable.CreateTable();

        GachaLogsTable.CreateTable();
    }
}