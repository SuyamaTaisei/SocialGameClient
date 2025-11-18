namespace GameUtility
{
    public static class Const
    {
        //エンドポイント
        public const string REGISTER_URL          = "http://localhost/api/register";
        public const string LOGIN_URL             = "http://localhost/api/login";
        public const string HOME_URL              = "http://localhost/api/home";
        public const string MASTER_DATA_CHECK_URL = "http://localhost/api/check_master_data";
        public const string MASTER_DATA_GET_URL   = "http://localhost/api/get_master_data";
        public const string PAYMENT_URL           = "http://localhost/api/payment";

        //DB
        public const string SQLITE_DB_NAME = "SocialGameServer.db";

        //マスタデータバージョン
        public const string MASTER_DATA_VERSION = "1";

        //エラーID
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "0";
    }
}