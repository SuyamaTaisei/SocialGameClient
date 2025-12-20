using UnityEngine;
using UnityEngine.UI;

public class ClickEffect : MonoBehaviour
{
    private Button button;
    private static ClickEffect lastPressed;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        //押されたボタンがある かつ 他ボタンが押されたら元画像に戻す
        if (lastPressed != null && lastPressed != this)
        {
            lastPressed.button.image.sprite = button.image.sprite;
        }

        //ボタンが押されたら画像を切り替えて、最後に押されたボタンとして設定
        button.image.sprite = button.spriteState.pressedSprite;
        lastPressed = this;
    }
}
