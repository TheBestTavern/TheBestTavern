using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class SaveLoadManager : MonoSingleton<SaveLoadManager>
{
    PlayerGameData playerGameData = new PlayerGameData();

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(gameObject);

        // 커맨드 등록
        OnNewDay command = new(this);
        CommandManager.Instance.AddCommand(command);
    }

    public void SaveData()
    {
        playerGameData.SetSaveData();
#if UNITY_WEBGL
SaveDataWeb();
#else
        SaveDataBasic();
#endif
    }

    public void SaveDataWeb()
    {
        string json = JsonConvert.SerializeObject(playerGameData);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    public void SaveDataBasic()
    {
        string json = JsonConvert.SerializeObject(playerGameData, Formatting.Indented);
        string path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(path, json);
        Debug.Log(path + " 데이터 세이브");
    }

    public bool LoadData(out PlayerGameData playerGameData)
    {
#if UNITY_WEBGL
    return LoadDataWeb(out playerGameData);
#else
        return LoadDataBasic(out playerGameData);
#endif
    }

    public bool LoadDataWeb(out PlayerGameData playerGameData)
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            playerGameData = JsonConvert.DeserializeObject<PlayerGameData>(json);
            return true;
        }
        else
        {
            playerGameData = null;
            return false;
        }
    }

    public bool LoadDataBasic(out PlayerGameData playerGameData)
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerGameData = JsonConvert.DeserializeObject<PlayerGameData>(json);
            return true;
        }
        playerGameData = null;
        return false;
    }


}
public class OnNewDay : IDayCommand
{
    SaveLoadManager saveLoadManager;

    public OnNewDay(SaveLoadManager saveLoadManager)
    {
        this.saveLoadManager = saveLoadManager;
    }

    public int Priority => 1001;

    public Task Execute()
    {
        Debug.Log("데이터 세이브");
        saveLoadManager.SaveData();
        return Task.CompletedTask;
    }
    public bool isValid()
    {
        return saveLoadManager != null;
    }
}