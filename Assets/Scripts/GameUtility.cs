namespace GameUtility
{
    //上から順に、エンドポイント、DB、マスタデータ、エラー系、内部表記系、数値系、外部表記系
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

        //エラーコード
        public const string ERRCODE_DB_UPDATE = "1";
        public const string ERRCODE_MASTER_DATA_UPDATE = "0";
        public const string ERRCODE_NOT_PAYMENT = "510";
        public const string ERRCODE_LIMIT_WALLETS = "511";

        //アカウント登録時バリデーションエラー
        public const string ERROR_VALIDATE_1 = "正しく入力してください";
        public const string ERROR_VALIDATE_2 = "4文字以上で入力してください";
        public const int NUMBER_VALIDATE_1 = 3;

        //支払い時エラー
        public const string ERROR_PAYMENT_1 = "残高が不足しています";
        public const string ERROR_PAYMENT_2 = "ウォレット上限に達しました";

        //ログイン表記
        public const string SHOW_USER = "ユーザー：";
        public const string SHOW_ID = "ID : ";

        //通貨表記
        public const string SHOW_YEN = "円";
        public const string SHOW_GEM = "ジェム";

        //ショップ表記
        public const string SHOW_AFTER_WALLET = "購入後のウォレット残高\n\n";
        public const string SHOW_PAID_GEM = "有償ジェム";
        public const string SHOW_FREE_GEM = "\n無償ジェム";

        //ガチャ表記
        public const string SHOW_GACHA_CONFIRM_TEXT = "ジェムでガチャを実行しますか？";
        public const string SHOW_GACHA_PERIOD_START = "期限 : ";
        public const string SHOW_GACHA_PERIOD_END = " まで";
        public const string SHOW_GACHA_PERIOD_NOTHING = "期限なし";
        public const string SHOW_GACHA_COUNT = "回引く";
        public const string SHOW_GACHA_NEW = "NEW\nCHARACTER";
        public const string SHOW_GACHA_REWARD_NOTHING = "ガチャ報酬がありません";
        public const string SHOW_GACHA_LOG_NOTHING = "ガチャ履歴がありません";
        public const string SHOW_GACHA_RARITY_N = "N : ";
        public const string SHOW_GACHA_RARITY_R = "R : ";
        public const string SHOW_GACHA_RARITY_SR = "SR : ";
        public const string SHOW_GACHA_RARITY_SSR = "SSR : ";
        public const string SHOW_GACHA_RATE_DECIMAL = "0.###";
        public const string SHOW_GACHA_RATE_PERCENT = "%";

        //ショップカテゴリ数値
        public const int SHOP_GEMS = 1001;
        public const int SHOP_ITEMS = 1002;

        //ショップ販売アイテムID
        public const int SHOP_ITEM_ID = 1001;

        //ガチャ数値
        public const float GACHA_COLOR_NEW = 1;
        public const float GACHA_COLOR_EXIST = 0.15f;
        public const int GACHA_PERIOD_DEFAULT = 1001;
        public const float GACHA_TOTAL_RATE = 1000f;
        public const int GACHA_1000_NUMBER = 1000;
        public const int GACHA_1999_NUMBER = 1999;
        public const int GACHA_2000_NUMBER = 2000;
        public const int GACHA_2999_NUMBER = 2999;
        public const int GACHA_3000_NUMBER = 3000;
        public const int GACHA_3999_NUMBER = 3999;
        public const int GACHA_4000_NUMBER = 4000;
        public const int GACHA_4999_NUMBER = 4999;

        //ログ数値
        public const int LOG_GACHA_LIMIT = 150;

        //フォルダ名
        public const string FOLDER_NAME_IMAGES = "Images";
        public const string FOLDER_NAME_CHARACTERS = "Characters";
        public const string FOLDER_NAME_GEMS = "Gems";
        public const string FOLDER_NAME_ITEMS = "Items";

        //シーン名
        public const string SCENE_NAME_HOMESCENE = "HomeScene";
    }
}