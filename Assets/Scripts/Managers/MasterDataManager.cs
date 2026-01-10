public static class MasterDataManager
{
    public static void SetMasterDataVersion(int version)
    {
        SaveManager.Instance.SetMasterDataVersion(version);
    }

    public static int GetMasterDataVersion()
    {
        return SaveManager.Instance.GetMasterDataVersion();
    }
}