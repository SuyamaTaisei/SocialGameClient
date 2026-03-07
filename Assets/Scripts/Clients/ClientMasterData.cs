using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientMasterData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI connectingText;
    [SerializeField] Button masterCheckButton;
    [SerializeField] GameObject connectingView;

    [SerializeField] ClientTitle clientTitle;
    private ApiConnect apiConnect;

    private int serverVersion;
    private const string masterData_key = "client_master_version";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        masterCheckButton.gameObject.SetActive(false);
        ConnectingView(false, "");

        masterCheckButton.onClick.AddListener(() => MasterDataUpdateComplete());
    }

    //通信中表記用
    public void ConnectingView(bool enabled, string text)
    {
        connectingView.SetActive(enabled);
        connectingText.text = text;
    }

    //1.マスタデータバージョン確認処理
    public void MasterDataCheck()
    {
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(masterData_key, GameUtility.Const.MASTER_DATA_VERSION)
        };
        ConnectingView(true, GameUtility.Const.SHOW_MASTER_CONFIRMING);

        StartCoroutine(apiConnect.Send(GameUtility.Const.MASTER_DATA_CHECK_URL, form, (action) =>
        {
            ConnectingView(false, "");

            //現在のローカルバージョン、最新のサーバーバージョンを取得
            int localVersion = MasterDataManager.GetMasterDataVersion();
            serverVersion = action.master_data_version;

            //バージョンが一致していれば
            if (localVersion == serverVersion)
            {
                LoadingManager.Instance.LoadScene(GameUtility.Const.SCENE_NAME_HOMESCENE);
            }
            else
            {
                MasterDataGet();
            }
        }));
    }

    //2.マスタデータ取得処理(バージョンが一致していなければ自動でゲームアップデート)
    public void MasterDataGet()
    {
        clientTitle.StartView.SetActive(false);
        ConnectingView(true, GameUtility.Const.SHOW_MASTER_UPDATING);

        StartCoroutine(apiConnect.Send(GameUtility.Const.MASTER_DATA_GET_URL, null, (action) =>
        {
            //バージョンが一致していない場合は最新のバージョンを保存
            MasterDataManager.SetMasterDataVersion(serverVersion);
            ConnectingView(true, GameUtility.Const.SHOW_MASTER_UPDATE_COMPLETE);
            masterCheckButton.gameObject.SetActive(true);
        }));
    }

    //3.マスタデータ更新完了後のシーン遷移
    public void MasterDataUpdateComplete()
    {
        //ローカルバージョンを最新バージョンに更新済み
        int localVersion = MasterDataManager.GetMasterDataVersion();
        if (localVersion == serverVersion)
        {
            ConnectingView(false, "");
            LoadingManager.Instance.LoadScene(GameUtility.Const.SCENE_NAME_HOMESCENE);
        }
    }
}