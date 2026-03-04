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
}
