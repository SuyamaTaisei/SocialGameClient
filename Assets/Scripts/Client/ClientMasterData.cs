using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientMasterData : MonoBehaviour
{
    [SerializeField] GameObject masterCheckView;
    [SerializeField] TextMeshProUGUI masterCheckText;
    [SerializeField] GameObject masterCheckButton;
    [SerializeField] ApiConnect apiConnect;

    private const string masterData_key = "client_master_version";

    public static ClientMasterData Instance { get; private set; }

    private void Awake()
    {
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
        masterCheckView.SetActive(false);
        masterCheckButton.SetActive(false);
    }

    //マスタデータアップデート警告
    public void MasterDataWarningUpdate(string message)
    {
        masterCheckText.text = message;
        masterCheckView.SetActive(true);
        masterCheckButton.SetActive(true);
    }

    //マスタデータバージョン確認処理
    public void MasterDataCheck()
    {
        Action action = new(() => MasterDataGet());

        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection(masterData_key, GameUtility.Const.MASTER_DATA_VERSION)
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.MASTER_DATA_CHECK_URL, form, action));
    }

    //マスタデータ取得処理
    public void MasterDataGet()
    {
        masterCheckView.SetActive(true);
        masterCheckText.text = "ゲームを更新中...";
        Action action = () =>
        {
            masterCheckText.text = "更新しました。";
            masterCheckButton.SetActive(true);
        };
        StartCoroutine(apiConnect.Send(GameUtility.Const.MASTER_DATA_GET_URL, null, action));
    }

    //マスタデータ更新完了ボタン
    public void MasterDataUpdateComplete()
    {
        masterCheckView.SetActive(false);
        LoadingManager.Instance.LoadScene("HomeScene");
    }
}