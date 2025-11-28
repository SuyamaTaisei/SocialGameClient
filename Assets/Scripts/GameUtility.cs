namespace GameUtility
{
    public static class Const
    {
        //エンドポイント
        public const string REGISTER_URL = "http://localhost/api/register";
        public const string LOGIN_URL = "http://localhost/api/login";
        public const string HOME_URL = "http://localhost/api/home";
        public const string MASTER_DATA_CHECK_URL = "http://localhost/api/check_master_data";
        public const string MASTER_DATA_GET_URL = "http://localhost/api/get_master_data";
        public const string PAYMENT_URL = "http://localhost/api/payment";
        public const string GACHA_EXECUTE_URL = "http://localhost/api/gacha_execute";

        //DB
        public const string SQLITE_DB_NAME = "SocialGameServer.db";

        //マスタデータ
        public const string MASTER_DATA_VERSION = "1";
        public const string SHOW_MASTER_TEXT_1 = "ゲームを更新中...";
        public const string SHOW_MASTER_TEXT_2 = "ゲームを更新しました";
        public const string ERROR_MASTER_DATA_VERSION_TEXT = "ゲームをアップデートしてください";

        //エラーID
        public const string ERRCODE_DB_UPDATE = "1";
        public const string ERRCODE_MASTER_DATA_UPDATE = "0";
        public const string ERRCODE_NOT_PAYMENT = "510";
        public const string ERRCODE_LIMIT_WALLETS = "511";

        //アカウント登録時バリデーション
        public const string ERROR_VALIDATE_1 = "正しく入力してください";
        public const string ERROR_VALIDATE_2 = "4文字以上で入力してください";
        public const int NUMBER_VALIDATE_1 = 3;

        //支払い時エラー
        public const string ERROR_PAYMENT_1 = "残高が不足しています";
        public const string ERROR_PAYMENT_2 = "ウォレット上限に達しました";

        //ガチャ回数
        public const int GACHA_SINGLE_COUNT = 1;
        public const int GACHA_MULTI_COUNT = 10;

        //スタート時ガチャ種類デフォルトリスト
        public const int GACHA_START_DEFAULT_LIST = 1001;

        //ガチャ画面テキスト
        public const string SHOW_GACHA_CONFIRM_TEXT = "ジェムでガチャを実行しますか？";
        public const string SHOW_GACHA_PERIOD_TEXT_1 = "期限 : ";
        public const string SHOW_GACHA_PERIOD_TEXT_2 = " まで";
        public const string SHOW_GACHA_COUNT = "回引く";

        //ログイン表記
        public const string SHOW_USER = "ユーザー：";
        public const string SHOW_ID = "ID : ";

        //通貨表記
        public const string SHOW_YEN = "円";
        public const string SHOW_GEM = "ジェム";

        //シーン名
        public const string SCENE_NAME_HOMESCENE = "HomeScene";
    }
}