using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移用簡易フェードクラス
/// </summary>
public class LoadingManager : MonoBehaviour
{
    private static Canvas canvas;
    private static Image image;

    private static LoadingManager instance;
    public static LoadingManager Instance
    {
        get
        {
            if (instance == null) { Init(); }
            return instance;
        }
    }

    IEnumerator fadeCoroutine = null;

    private LoadingManager() { }

    private static void Init()
    {
        // Canvas作成
        GameObject canvasObject = new GameObject("CanvasFade");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = short.MaxValue; //最上層に配置
        canvasObject.AddComponent<GraphicRaycaster>();

        // Image作成 (画面ブロック用の全面にストレッチ)
        image = new GameObject("ImageFade").AddComponent<Image>();
        var imageObject = image.rectTransform;
        imageObject.SetParent(canvas.transform, false);
        imageObject.anchorMin = Vector2.zero;
        imageObject.anchorMax = Vector2.one;
        imageObject.offsetMin = Vector2.zero;
        imageObject.offsetMax = Vector2.zero;

        image.raycastTarget = false;

        // 遷移先シーンでもオブジェクトを破棄しない
        DontDestroyOnLoad(canvas.gameObject);

        // シングルトンオブジェクトを保持
        canvasObject.AddComponent<LoadingManager>();
        instance = canvasObject.GetComponent<LoadingManager>();
    }

    // フェード付きシーン遷移を行う
    public void LoadScene(string sceneName, float interval = 1f)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = Fade(sceneName, interval);
        StartCoroutine(fadeCoroutine);
    }
    private IEnumerator Fade(string sceneName, float interval)
    {
        float time = 0f;
        canvas.enabled = true;

        image.transform.SetAsLastSibling(); //常に最前面に出す
        image.raycastTarget = true;         //フェード中は入力ブロック

        // フェードアウト
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            image.color = new Color(0.0f, 0f, 0f, fadeAlpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        // シーン非同期ロード
        yield return SceneManager.LoadSceneAsync(sceneName);

        // フェードイン
        time = 0f;
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            image.color = new Color(0f, 0f, 0f, fadeAlpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        image.raycastTarget = false; //終了後にブロック解除

        // 描画を更新しない
        canvas.enabled = false;
    }
}