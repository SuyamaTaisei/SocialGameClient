using UnityEngine;

public static class MasterDataManager
{
    public static void SetMasterDataVersion(int version)
    {
        PlayerPrefs.SetInt(GameUtility.Const.MASTER_DATA_VERSION, version);
        //SaveManager.Instance.SetMasterDataVersion(version);
    }

    public static int GetMasterDataVersion()
    {
        return PlayerPrefs.GetInt(GameUtility.Const.MASTER_DATA_VERSION, 0);
        //return SaveManager.Instance.GetMasterDataVersion();
    }
}