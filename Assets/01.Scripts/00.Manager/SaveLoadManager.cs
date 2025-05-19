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
        playerGameData.SetData();
        string json = JsonConvert.SerializeObject(playerGameData, Formatting.Indented);
        string path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(path, json);
        Debug.Log(path + " 데이터 세이브");
    }

    public PlayerGameData LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PlayerGameData>(json);
        }
        return null;
    }

}
public class OnNewDay : IDayCommand
{
    SaveLoadManager saveLoadManager;

    public OnNewDay(SaveLoadManager saveLoadManager)
    {
        this.saveLoadManager = saveLoadManager;
    }

    public int Priority => 1200;

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