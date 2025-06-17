using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.Services.Analytics;
using UnityEngine;

public class MainScene : BaseScene
{
    public async override UniTask OnEnterScene()
    {
        await base.OnEnterScene();
        SoundManager.Instance.PlayBGM("MainBGM1");
        SoundManager.Instance.StopLoop();
        EventBus.Publish<NPCVisitEvent>(new NPCVisitEvent());
        //IRunAlready[] runAlready = FindObjectsOfType<MonoBehaviour>().(where);
        foreach (var runAlready in Object.FindObjectsOfType<MonoBehaviour>().OfType<IRunOnEnter>())
        {
            await runAlready.RunAhead();
        }        
    }
}
