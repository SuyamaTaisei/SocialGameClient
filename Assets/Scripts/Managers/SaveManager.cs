using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    //セーブファイルの名前
    const string FileName = "/savedata.dat";

    //セーブデータのデフォルト値
    static readonly int DefaultVersion = 0;

    const float DefaultVolumeMaster = 0.5f;
    const float DefaultVolumeBgm = 0.5f;
    const float DefaultVolumeSe = 0.5f;

    FileStream file;
    BinaryFormatter bf;
    string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        filePath = Application.persistentDataPath + FileName;

        //セーブデータ作成
        if (!SaveDataCheck())
        {
            CreateSaveData();
            Debug.Log(filePath);
        }
    }

    //ファイル更新共通準備
    void InitFileSave()
    {
        bf = new();
        if (filePath == null)
        {
            filePath = Application.persistentDataPath + FileName;
        }
        file = File.Create(filePath);
    }

    //ファイルロード共通準備
    void InitFileLoad()
    {
        bf = new();
        file = File.Open(filePath, FileMode.Open);
    }

    //ファイルクローズ処理
    void CloseFile()
    {
        file.Close();
        file = null;
    }

    //ファイル存在チェック
    public bool SaveDataCheck()
    {
        //ファイルがあればtrue
        if (File.Exists(filePath)) { return true; }
        return false;
    }

    //共通の全体ロード
    private SaveData LoadAllData()
    {
        SaveData data = null;
        if (SaveDataCheck())
        {
            try
            {
                InitFileLoad();
                data = bf.Deserialize(file) as SaveData;
            }
            catch (IOException)
            {
                Debug.LogError("failed to open file");
            }
            finally
            {
                if (file != null) { CloseFile(); }
            }
        }
        return data;
    }

    //新規データ生成
    public void CreateSaveData()
    {
        try
        {
            InitFileSave();

            //セーブデータを生成
            SaveData data = new();
            data.version = DefaultVersion;
            data.volumeMaster = DefaultVolumeMaster;
            data.volumeBgm = DefaultVolumeBgm;
            data.volumeSe = DefaultVolumeSe;

            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            //fileStreamを使用したら必ず最後にcloseする
            if (file != null) { file.Close(); }
        }
    }

    //バージョンセーブ
    public void SetMasterDataVersion(int version)
    {
        try
        {
            InitFileSave();

            SaveData data = new();
            data.version = version;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
    }

    //バージョンロード
    public int GetMasterDataVersion()
    {
        int version = DefaultVersion;
        try
        {
            InitFileLoad();

            //セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            version = data.version;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { file.Close(); }
        }
        return version;
    }

    //音量セーブ
    public void SaveSoundVolume(float vm, float vb, float vs)
    {
        try
        {
            SaveData data = LoadAllData();
            data.volumeMaster = vm;
            data.volumeBgm = vb;
            data.volumeSe = vs;

            //シリアライズ化のためのバイナリ形式を用意
            InitFileSave();
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { CloseFile(); }
        }
    }

    //音量ロード
    public void LoadSoundVolume(ref float vm, ref float vb, ref float vs)
    {
        try
        {
            InitFileLoad();

            // セーブデータを読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            vm = data.volumeMaster;
            vb = data.volumeBgm;
            vs = data.volumeSe;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            if (file != null) { CloseFile(); }
        }
    }
}