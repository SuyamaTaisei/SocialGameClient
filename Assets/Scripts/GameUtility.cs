#define ENV_LOCAL
#define ENV_SERVER

namespace GameUtility
{
    //上から順に、エンドポイント、DB、マスタデータ、エラー系、内部表記系、数値系、外部表記系
    public static class Const
    {
        #if ENV_LOCAL
            private const string BASE_URL = "http://localhost/";
        #elif ENV_SERVER
            private const string BASE_URL = "http://example.com/";
        #endif

        //エンドポイント
        public const string REGISTER_URL          = BASE_URL + "api/register";
        public const string LOGIN_URL             = BASE_URL + "api/login";
        public const string HOME_URL              = BASE_URL + "api/home";
        public const string MASTER_DATA_CHECK_URL = BASE_URL + "api/check_master_data";
        public const string MASTER_DATA_GET_URL   = BASE_URL + "api/get_master_data";
        public const string PAYMENT_URL           = BASE_URL + "api/payment";
        public const string GACHA_EXECUTE_URL     = BASE_URL + "api/gacha_execute";
        public const string CHARACTER_ENHANCE_URL = BASE_URL + "api/enhance_character";
        public const string STAMINA_DECREASE_URL  = BASE_URL + "api/stamina_decrease";
        public const string STAMINA_INCREASE_URL  = BASE_URL + "api/stamina_increase";
        public const string STAMINA_AUTO_INCREASE_URL = BASE_URL + "api/stamina_auto_increase";

        //DB
        public const string SQLITE_DB_NAME = "SocialGameServer.db";

        //マスタデータ
        public const string MASTER_DATA_VERSION = "1";
        public const string SHOW_MASTER_TEXT_1 = "ゲームを更新中...";
        public const string SHOW_MASTER_TEXT_2 = "ゲームを更新しました";

        //エラーコード
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
        public const string SHOW_COIN = "コイン";
        public const string SHOW_GEM = "ジェム";

        //ショップ表記
        public const string SHOW_PRODUCT_NAME = "商品名";
        public const string SHOW_GEM_WALLET = "現在のウォレット残高\n";
        public const string SHOW_COIN_WALLET = "コイン残高 ";
        public const string SHOW_PAID_GEM = "有償";
        public const string SHOW_FREE_GEM = "無償";
        public const string SHOW_BUY = "で購入しますか？";
        public const string SHOW_AMOUNT = "個";
        public const string SHOW_GET = "獲得";

        //ガチャ表記
        public const string SHOW_GACHA_CONFIRM_TEXT = "ジェムでガチャを実行しますか？";
        public const string SHOW_GACHA_PERIOD_START = "期限 : ";
        public const string SHOW_GACHA_PERIOD_END = " まで";
        public const string SHOW_GACHA_PERIOD_NOTHING = "期限なし";
        public const string SHOW_GACHA_COUNT = "回引く";
        public const string SHOW_GACHA_NEW = "NEW\nCHARACTER";
        public const string SHOW_GACHA_LOG_NOTHING = "ガチャ履歴がありません";
        public const string SHOW_GACHA_RARITY_N = "N : ";
        public const string SHOW_GACHA_RARITY_R = "R : ";
        public const string SHOW_GACHA_RARITY_SR = "SR : ";
        public const string SHOW_GACHA_RARITY_SSR = "SSR : ";
        public const string SHOW_GACHA_RATE_DECIMAL = "0.###";
        public const string SHOW_GACHA_RATE_PERCENT = "%";

        //インスタンス表記
        public const string SHOW_INSTANCE_LEVEL = "Lv.";
        public const string SHOW_INSTANCE_LEVEL_MAX = "100";
        public const string SHOW_INSTANCE_AMOUNT_MAX = "99";
        public const string SHOW_INSTANCE_CHARA_NOTHING = "キャラクターを所持していません";
        public const string SHOW_INSTANCE_ITEM_NOTHING = "アイテムを所持していません";
        public const string SHOW_INSTANCE_ENHANCE_ITEM_NOTHING = "強化アイテムを所持していません";

        //スタミナ数値
        public const int STAMINA_MAX_VALUE = 199;
        public const int STAMINA_MOST_VALUE = 100;
        public const int STAMINA_DECREASE_VALUE = 5;
        public const int STAMINA_EVERY_MINUTE = 60;
        public const int STAMINA_GEM_VALUE = 50;

        //ショップカテゴリ数値
        public const int SHOP_GEMS = 1001;
        public const int SHOP_ITEMS = 1002;

        //ショップ購入最小数値・最大数値
        public const int SHOP_AMOUNT_MIN = 1;
        public const int SHOP_AMOUNT_MAX = 99;

        //ショップ販売アイテムID数値
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

        //アイテムカテゴリ数値
        public const int ITEM_CATEGORY_ENHANCES = 1001;

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