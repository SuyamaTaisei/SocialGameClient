using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    //ボタン押下有無に応じた、ボタンとテキストの透明度変更汎用メソッド
    public void ButtonTextOpacityEffect(bool enabled, Button button)
    {
        button.interactable = enabled;
        var color = button.image.color;
        color.a = enabled ? 1 : 0.07f;
        button.image.color = color;

        var text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.color = color;
    }
}
