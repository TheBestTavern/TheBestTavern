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

/* Priority : commands
0 : CommandManager : command 시작 
200 : Questmanager : 오늘 완료한 퀘스트 체크
500 : NpcArea : 표시된 NPC 숨기기
1000 : TimerManager : 하루가 지나감.
1500 : QuestData : 진행중인 퀘스트 트리거 확인, 오늘의 퀘스트 받아오기
1900 : MailBoxContentBase : isReadyTodaySlot false로 돌리기
1800 : Calendarmanager : 오늘이 무슨날인지 체크.
2000 : CommandManager : command끝 
*/

public class CommandManager : MonoSingleton<CommandManager>
{
    List<IDayCommand> commands = new(); // 우선순위 큐로 전환 고려.
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
        //Debug.Log("하루 명령 끝");

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
        //Debug.Log("하루 명령 시작");
        DayAndNightManager.Instance.TriggerTimeProcess(1);
        return Task.CompletedTask;
    }

    public bool isValid()
    {
        return true;
    }
}

