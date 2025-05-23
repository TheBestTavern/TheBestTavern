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

        //IRunAlready[] runAlready = FindObjectsOfType<MonoBehaviour>().(where);
        foreach (var runAlready in FindObjectsOfType<MonoBehaviour>().OfType<IRunAlready>())
        {
            await runAlready.RunAlready();
        }

        if (GameManager.Instance.isAnalyticsAgreed)
        {
            var tutorialEvent = new AnalyticsTutorial("TutorialData")
            {
                watchTutorial = GameManager.Instance.doTutorial
            };
            AnalyticsService.Instance.RecordEvent(tutorialEvent);
        }
    }
}
