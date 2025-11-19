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

        //マスタデータ
        public const string MASTER_DATA_VERSION = "1";
        public const string SHOW_MASTER_TEXT_1 = "ゲームを更新中...";
        public const string SHOW_MASTER_TEXT_2 = "ゲームを更新しました";

        //システムエラー
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "0";
        public const string ERROR_MASTER_DATA_VERSION_TEXT = "ゲームをアップデートしてください";

        //アカウント登録時エラー
        public const string ERROR_VALIDATE_1 = "正しく入力してください";
        public const string ERROR_VALIDATE_2 = "4文字以上で入力してください";

        //ログイン表記
        public const string SHOW_USER = "ユーザー：";
        public const string SHOW_ID = "ID : ";

        //通貨表記
        public const string SHOW_YEN = "円";

        //シーン名
        public const string SCENE_NAME_HOMESCENE = "HomeScene";
    }
}