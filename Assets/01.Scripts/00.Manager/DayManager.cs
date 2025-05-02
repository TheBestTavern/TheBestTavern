using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;

public interface IDayCommand
{
    public int Priority {get;}
    public void Execute();
    public bool isValid();
}

public class DayManager : MonoSingleton<DayManager>
{
    List<IDayCommand> commands = new();
    bool isReady;

    public override void Init()
    {
        if (_isInitialized) return;
        base.Init();

        DontDestroyOnLoad(this);
    }

    public void AddCommand(IDayCommand command)
    {
        commands.Add(command);
        isReady = false;
    }

    public void ExecuteCommands(int from = 0)
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
            command.Execute();
        }
    }
}

