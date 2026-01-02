using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceCharacterDetailFixedView : MonoBehaviour
{
    [SerializeField] Image characterDetailImage;
    [SerializeField] TextMeshProUGUI characterDetailNameText;
    [SerializeField] TextMeshProUGUI characterDetailRarityText;
    [SerializeField] TextMeshProUGUI characterDetailLevelBeforeText;
    [SerializeField] TextMeshProUGUI characterDetailLevelAfterText;
    [SerializeField] Button enhanceButton;
    [SerializeField] Button executeEnhanceButton;
    [SerializeField] Button cancelEnhanceButton;
    [SerializeField] Button enhanceCompleteButton;

    [SerializeField] TextMeshProUGUI enhanceButtonText;

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
        executeEnhanceButton.onClick.AddListener(() => clientInstance.ExecuteEnhance()); //強化リクエスト送信
        cancelEnhanceButton.onClick.AddListener(() => SetEnhanceConfirmView(false));
        enhanceCompleteButton.onClick.AddListener(() => SetEnhanceCompleteView(false));
    }

    public void Set(CharacterDataModel data1, CharacterRaritiesModel data2, CharacterInstancesModel data3, string imagePath)
    {
        if (characterDetailImage) characterDetailImage.sprite = Resources.Load<Sprite>(imagePath);
        if (characterDetailNameText) characterDetailNameText.text = data1.name;
        if (characterDetailRarityText) characterDetailRarityText.text = data2.name;

        //強化対象となるキャラの更新
        clientInstance.SetEnhanceCharacterId(data3.character_id);
        clientInstance.ClearSelectEnhanceItems();

        beforeLevel = data3.level;
        if (characterDetailLevelBeforeText) characterDetailLevelBeforeText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + data3.level.ToString();

        afterLevel = beforeLevel;
        if (characterDetailLevelAfterText) characterDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + afterLevel;

        SetCtrlEnhanceButton();
    }

    //レベルアップ表記のプレビュー更新処理
    public void SetAddAfterLevel(int value)
    {
        afterLevel += value;
        characterDetailLevelAfterText.text = GameUtility.Const.SHOW_INSTANCE_LEVEL + afterLevel;

        SetCtrlEnhanceButton();
    }

    //強化後に最新レベルで更新
    public void SetLatestLevel(int value)
    {
        beforeLevel = value;
        afterLevel = value;

        characterDetailLevelBeforeText.text = $"{GameUtility.Const.SHOW_INSTANCE_LEVEL}{beforeLevel}";
        characterDetailLevelAfterText.text = $"{GameUtility.Const.SHOW_INSTANCE_LEVEL}{afterLevel}";

        SetCtrlEnhanceButton(); //強化直後は、強化ボタンを押せない
    }

    //強化ボタン押下制御
    private void SetCtrlEnhanceButton()
    {
        enhanceButton.interactable = (afterLevel != beforeLevel) && (afterLevel <= int.Parse(GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX));

        bool isMaxLevel = beforeLevel >= int.Parse(GameUtility.Const.SHOW_INSTANCE_LEVEL_MAX);
        enhanceButtonText.text = isMaxLevel ? "最大レベルです" : "強化する";
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
