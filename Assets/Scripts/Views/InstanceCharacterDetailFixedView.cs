using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterDetailFixedView : MonoBehaviour
{
    [SerializeField] Image charaDetailImage;
    [SerializeField] TextMeshProUGUI charaDetailNameText;
    [SerializeField] TextMeshProUGUI charaDetailRarityText;
    [SerializeField] TextMeshProUGUI charaDetailLevelBeforeText;
    [SerializeField] TextMeshProUGUI charaDetailLevelAfterText;
    [SerializeField] Button enhanceButton;
    [SerializeField] Button enhanceExecuteButton;
    [SerializeField] Button enhanceCancelButton;
    [SerializeField] Button enhanceCloseButton;

    [SerializeField] TextMeshProUGUI enhanceText;

    [SerializeField] GameObject enhanceConfirmView;
    [SerializeField] GameObject enhanceCompleteView;

    [SerializeField] ClientInstance clientInstance;

    private int beforeLevel;
    private int afterLevel;

    private void Start()
    {
        enhanceConfirmView.SetActive(false);
        enhanceCompleteView.SetActive(false);
        enhanceButton.onClick.AddListener(() => SetEnhanceConfirmView(true));
        enhanceExecuteButton.onClick.AddListener(() => clientInstance.ExecuteEnhance()); //強化リクエスト送信
        enhanceCancelButton.onClick.AddListener(() => SetEnhanceConfirmView(false));
        enhanceCloseButton.onClick.AddListener(() => SetEnhanceCompleteView(false));
    }

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, CharacterInstancesModel data3, string imagePath)
    {
        if (charaDetailImage) charaDetailImage.sprite = Resources.Load<Sprite>(imagePath);
        if (charaDetailNameText) charaDetailNameText.text = data1.name;
        if (charaDetailRarityText) charaDetailRarityText.text = data2.name;

        //強化対象となるキャラの更新
        clientInstance.SetEnhanceCharacterId(data3.character_id);
        clientInstance.ClearSelectEnhanceItems();

        beforeLevel = data3.level;
        if (charaDetailLevelBeforeText) charaDetailLevelBeforeText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + data3.level.ToString();

        afterLevel = beforeLevel;
        if (charaDetailLevelAfterText) charaDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + afterLevel;

        SetCtrlEnhanceButton();
    }

    //レベルアップ表記のプレビュー更新処理
    public void SetAddAfterLevel(int value)
    {
        afterLevel += value;
        charaDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + afterLevel;

        SetCtrlEnhanceButton();
    }

    //強化後に最新レベルで更新
    public void SetLatestLevel(int value)
    {
        beforeLevel = value;
        afterLevel = value;

        charaDetailLevelBeforeText.text = $"{GameUtility.Const.SHOW_INSTANCE_LEVEL}{beforeLevel}";
        charaDetailLevelAfterText.text = $"{GameUtility.Const.SHOW_INSTANCE_LEVEL}{afterLevel}";

        SetCtrlEnhanceButton(); //強化直後は、強化ボタンを押せない
    }

    //強化ボタン押下制御
    private void SetCtrlEnhanceButton()
    {
        enhanceButton.interactable = (afterLevel != beforeLevel) && (afterLevel <= int.Parse(GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX));

        bool isMaxLevel = beforeLevel >= int.Parse(GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX);
        enhanceText.text = isMaxLevel ? GameUtility.Const.SHOW_INSTANCE_MAX : GameUtility.Const.SHOW_INSTANCE_ENHANCE;
    }

    //強化確認画面
    public void SetEnhanceConfirmView(bool enabled)
    {
        enhanceConfirmView.SetActive(enabled);
    }

    //強化完了画面
    public void SetEnhanceCompleteView(bool enabled)
    {
        enhanceCompleteView.SetActive(enabled);
    }
}
