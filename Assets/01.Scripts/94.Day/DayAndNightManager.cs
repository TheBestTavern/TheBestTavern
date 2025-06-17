using System.Collections;
using System.Net;
using UnityEngine;
//using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class DayAndNightManager : MonoSingleton<DayAndNightManager>
{
    public Material nightMat;
    [Range(0f, 1f)] public float process = 0; // 현재 하루의 진행도(시간)
    [Range(0f, 1f)] public float limitProcess; // 시간이 흐를때 리미트
    [Range(0f, 3f)] public float duration = 1;

    public AnimationCurve saturationCurve;
    public AnimationCurve lightnessCurve;

    Coroutine coroutine;

    bool _isInitilizedAsync;

    public override void Init()
    {
        if (_isInitialized && _isInitilizedAsync) return;
        base.Init();
        DontDestroyOnLoad(gameObject);

        InitAsync();
    }

    public async Task InitAsync()
    {
        if (_isInitilizedAsync) return;
        ManagerContainer so = await AddressablesLoader.Instance.AddressablesLoadAsync<ManagerContainer>("DayAndNightManagerContainer.SO");

        if (so != null && so.nightMaterial != null && so.saturationCurve != null && so.lightnessCurve != null)
        {
            //Debug.Log("######Manager SO 내부에 메터리얼, 애니메이션 커브가 잘 채워져있음");
            if (nightMat == null)
            {
                nightMat = so.nightMaterial;

                //Debug.Log("######메터리얼 넣기");
            }

            if (saturationCurve == null)
            {
                saturationCurve = so.saturationCurve;
                //Debug.Log("######채도 커브 넣기");
            }

            if (lightnessCurve == null)
            {
                lightnessCurve = so.lightnessCurve;
                //Debug.Log("######밝기 커브 넣기");
            }
        }
        _isInitilizedAsync = true;
    }

    public void pass1hour() // 한시간(정확히는 하루의 1/10씩 밝기 변경. 2차 시간표현 구현할때 필요한 기능. 제대로 구현하려면 process에 제한 줘야함.
    {
        limitProcess = process + 0.1f;
        TriggerTimeProcess(limitProcess);
    }

    public void TriggerTimeProcess(float targetProcess)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(LerpProcess(targetProcess));

        EventBus.Publish<EnterNightUIBlockEvent>(new EnterNightUIBlockEvent());
    }

    IEnumerator LerpProcess(float targetProcess)
    {
        while (true)
        {
            if (process < limitProcess)
            {
                process += Time.deltaTime / duration;
                nightMat.SetFloat("_Saturation", saturationCurve.Evaluate(process));
                nightMat.SetFloat("_Lightness", lightnessCurve.Evaluate(process));
            }

            if (process >= 1)
            {
                process = 0;
                limitProcess = 0;
                EventBus.Publish<EndNightUIBlockEvent>(new EndNightUIBlockEvent());
                EventBus.Publish<NPCVisitEvent>(new NPCVisitEvent());
                yield break;
            }
            if (targetProcess < process)
            {
                EventBus.Publish<EndNightUIBlockEvent>(new EndNightUIBlockEvent());
                Debug.LogError("######이거 실행되면 안됨");
                yield break;
            }

            yield return null;
        }
    }
}