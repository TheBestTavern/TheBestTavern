using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;

public interface IDayCommand
{
    public int Prior {get;}
    public void Execute();
}

public class DayManager : MonoSingleton<DayManager>
{
    List<IDayCommand> commands = new();
    bool isReady;

    public void AddCommand(IDayCommand command)
    {
        commands.Add(command);
        
    }

    public void ExecuteCommands()
    {
        if(!isReady)
        {
            commands.Sort((a,b) => a.Prior.CompareTo(b.Prior)); // 퀵정렬 내부에 비교로직을 매개변수로 대입
            isReady = true;
        }

        foreach (var command in commands)
        {
            command.Execute();
        }
    }
}