using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClientMasterData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI masterCheckText;
    [SerializeField] Button masterCheckButton;
    [SerializeField] GameObject masterCheckView;

    [SerializeField] ClientTitle clientTitle;
    private ApiConnect apiConnect;

    private int serverVersion;
    private const string masterData_key = "client_master_version";

    private void Start()
    {
        apiConnect = ApiConnect.Instance;

        masterCheckView.SetActive(false);
        masterCheckButton.gameObject.SetActive(false);

        masterCheckButton.onClick.AddListener(() => MasterDataUpdateComplete());
    }

    //マスタデータアップデート警告
    public void MasterDataWarningUpdate(string message)
    {
        masterCheckText.text = message;
        masterCheckView.SetActive(true);
        masterCheckButton.gameObject.SetActive(true);
    }

    //1.マスタデータバージョン確認処理
    public void MasterDataCheck()
    {
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(masterData_key, GameUtility.Const.MASTER_DATA_VERSION)
        };

        StartCoroutine(apiConnect.Send(GameUtility.Const.MASTER_DATA_CHECK_URL, form, (action) =>
        {
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

    //2.マスタデータ取得処理(ゲームアップデート)
    public void MasterDataGet()
    {
        clientTitle.StartView.SetActive(false);
        masterCheckView.SetActive(true);
        masterCheckText.text = GameUtility.Const.SHOW_MASTER_TEXT_1;

        StartCoroutine(apiConnect.Send(GameUtility.Const.MASTER_DATA_GET_URL, null, (action) =>
        {
            //バージョンが一致していない場合は最新のバージョンを保存
            MasterDataManager.SetMasterDataVersion(serverVersion);
            masterCheckText.text = GameUtility.Const.SHOW_MASTER_TEXT_2;
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
            masterCheckView.SetActive(false);
            LoadingManager.Instance.LoadScene(GameUtility.Const.SCENE_NAME_HOMESCENE);
        }
    }
}