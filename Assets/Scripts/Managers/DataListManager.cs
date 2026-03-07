using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DataListManager : MonoBehaviour
{
    //リスト生成処理
    public (T, Button) CreateDataListSync<T>(T template, Transform content, bool isItem = true, bool isButton = false) where T : Component
    {
        var item   = Instantiate(template, content);
        var view   = isItem ? item.GetComponent<T>() : null;
        var button = isButton ? item.GetComponentInChildren<Button>() : null;
        return (view, button);
    }

    //リスト生成処理(非同期)
    public async Task<(T, Button)> CreateDataListAsync<T>(T template, Transform content, bool isItem = true, bool isButton = false) where T : Component
    {
        var data = InstantiateAsync(template, content);
        await data;
        var item = data.Result[0];
        var view   = isItem ? item.GetComponent<T>() : null;
        var button = isButton ? item.GetComponentInChildren<Button>() : null;
        return (view, button);
    }
}
