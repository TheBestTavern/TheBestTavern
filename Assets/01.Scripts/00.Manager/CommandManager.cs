using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using System.Threading.Tasks;
using UnityEngine;

public interface IDayCommand
{
    public int Priority {get;}
    public Task Execute();
    public bool isValid();
}

public class CommandManager : MonoSingleton<CommandManager>
{
    List<IDayCommand> commands = new();
    bool isReady;

    DayAndNightManager dayAndNightManager;
    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);

        OnCommandEnd endCommand = new();
        OnCommandStart startCommand = new();
        AddCommand(startCommand);
        AddCommand(endCommand);
    }

    private void Start()
    {
        dayAndNightManager = DayAndNightManager.Instance;
    }

    public void AddCommand(IDayCommand command)
    {
        commands.Add(command);
        isReady = false;
    }

    public async Task ExecuteCommands(int from = 0)
    {
        if(!isReady)
        {
            commands.Sort((a,b) => a.Priority.CompareTo(b.Priority)); // 퀵정렬 내부에 비교로직을 매개변수로 대입
            isReady = true;
        }

        commands.RemoveAll(x => !x.isValid());

        foreach (var command in commands)
        {
            if (command.Priority < from) continue;
            dayAndNightManager.limitProcess = command.Priority / 2000f;
            await command.Execute();
        }
    }
}

public class OnCommandEnd : IDayCommand
{
    public int Priority => 2000;

    public Task Execute()
    {
        Debug.Log("하루 명령 끝");

        return Task.CompletedTask;
    }

    public bool isValid()
    {
        return true;
    }
}

public class OnCommandStart : IDayCommand
{
    public int Priority => 0;

    public Task Execute()
    {
        Debug.Log("하루 명령 시작");
        DayAndNightManager.Instance.TriggerTimeProcess(1);
        return Task.CompletedTask;
    }

    public bool isValid()
    {
        return true;
    }
}

