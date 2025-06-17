# TheBestTavern

## 🎮 프로젝트 소개

[사진]

전래동화 속 인물들이 살아 숨 쉬는 **이세계 조선**.

마개조된 트랙터 사고로 전이된 청년이 시골 주막을 운영하며

**요리로 인연을 쌓고, 따뜻한 이야기를 완성**해 나갑니다.

- 장르: 서사형 감성 요리 시뮬레이션 + 비주얼 노벨
- 핵심 콘텐츠: 도감 완성, 채집·가공 미니게임, 전래 NPC 퀘스트
- 목표: 전통 요리 도감을 완성하고, 선택의 엔딩을 맞이하라!
- 플레이 타겟: 전래동화, 요리, 힐링, 도감 게임을 좋아하는 누구나!
- 플랫폼: PC, WebGL

“**사람 냄새 나는 요리**, 당신도 한번 만들어보시겠어요?”

---

『**최고의 주막**』은 조선 전통 설화와 전래동화를 바탕으로 한 **감성 요리 생활형 플레이 게임**입니다.

현대 청년이 이세계 조선으로 전이되어 작은 주막을 운영하며,

다양한 동화 속 인물들과의 **요리와 인연을 중심으로 한 스토리텔링**을 풀어냅니다.

본 프로젝트는 전통 소재의 친근한 해석과 가볍지만 몰입감 있는 플레이 흐름을 구현하며,

**전래 문화와 감성 힐링 콘텐츠의 융합**이라는 목표 아래 시작되었습니다.

**🎯 목표**

- 전통 요리/식문화 기반의 세계관 구축
- 간단하지만 몰입감 있는 **마우스 기반 미니게임** 시스템 구현
- **스토리 중심 NPC 의뢰 시스템**과 **도감 수집 요소** 완성
- 누구나 즐길 수 있는 **전 연령 친화적 콘텐츠** 개발
- 도감 100% 달성 시 분기 엔딩으로 몰입감 있는 클로징 제공

---

## 🎞️ 게임 Trailer

[![Watch the video](https://img.youtube.com/vi/WF1JYeQF-Ms/0.jpg)](https://youtu.be/WF1JYeQF-Ms)

---

## 🎮 주요 게임 기능


<details>
<summary><strong>📜전래 NPC 퀘스트</strong></summary>
1. 의뢰함에서 NPC 편지를 통해 퀘스트 수락

![20250529152205](https://github.com/user-attachments/assets/8a68aa98-a2ba-4502-ad5e-184efcec1018)

2. 지정한 날짜에 방문하는 NPC에게 아이템 제출



3. 퀘스트 보상: 상위 요리 재료 및 호감도 상승 ⇒ 일정 호감도 달성 시 상위 퀘스트 해금

</details>

<details>
<summary><strong>🧺채집(🌲숲, 🌊바다)</strong></summary>
플레이어는 퀘스트에 필요한 요리를 위해 재료를 모으러 지역을 선택할 수 있습니다. 

재료들은 지역뿐만 아니라 계절에 따라서도 다르게 채집할 수 있습니다

[GatheringVideo.mp4](attachment:08eb5c7d-91e5-462c-ac0a-b642c419f34b:GatheringVideo.mp4)

[SeaGatheringVideo.mp4](attachment:39ece88e-1242-4b30-bb26-2c73c825cac6:SeaGatheringVideo.mp4)

맵에 있는 요소들을 클릭하면 확률과 계절 지역에 따라 다른 재료들을 채집할 수 있습니다.

위에 있는 채집 인벤토리가 가득 차면 더 이상 채집을 할 수 없습니다.

채집이 끝난 후 주막으로 돌아가면 자동으로 아이템이 저장됩니다.

산에서는 채집 중 확률적으로 동물 포획 미니게임이 등장합니다.

바다에서는 맵 중앙에 있는 바다를 클릭하면 낚시 미니게임을 시작할 수 있습니다

</details>

<details>
<summary><strong>🧩채집 미니게임</strong></summary>
- 동물 포획 미니게임
    - 동물의 종류에 따라 미끼를 사용하여 포획 또는 도망가기를 할 수 있습니다.
    - 조작 : `Space` - 돌 던지기 , `Right Mouse` - 미끼 사용

[GatheringMiniGameM.mp4](attachment:0064ea82-50bb-4841-a40d-7ec0eb5b72f8:GatheringMiniGameM.mp4)

- 낚시 미니게임
    - 낚시를 통해 여러 종류의 물고기를 포획할 수 있습니다.
    - 조작 : `F` - 낚시 시작 , `Space` - 끌어 당기기

</details>

<details>
<summary><strong>👨‍🍳요리/요리미니게임</strong></summary>
# 요리 / 요리 미니 게임

- 재료를 조합해서 요리를 완성할 수 있습니다.

![요리 결과 예시](attachment:b9750bb5-0693-4196-9a63-0bb03ff5e3ce:image.png)

요리 결과 예시

---

- 도마
    - 왼쪽→오른쪽으로 재료를 잘라나갑니다.
    - 조작 : `Space`

[16조 최고의 주막](https://www.notion.so/16-2022dc3ef5148164ae90c398d100fd39?pvs=21)

[CuttingMiniGame.mp4](attachment:3142ee16-cd4f-4d0e-ba0c-7a2fdf912477:CuttingMiniGame.mp4)

- 절구
    - 노트 타이밍에 맞춰 재료를 빻습니다.
    - 조작 : `Space`

[GrindMiniGame.mp4](attachment:60060a72-0e39-4c5e-8f31-ce014ea7ea3c:GrindMiniGame.mp4)

- 믹싱볼
    - 숟가락으로 재료 2가지 이상을 섞습니다.
    - 조작 : `Left Mouse`

[MinxingBowlMiniGame.mp4](attachment:9b4a7aaf-7a05-4bb3-86b5-73c6ebc92a31:MinxingBowlMiniGame.mp4)

- 가마솥 (굽기)
    - 숫자쌍을 맞춰 재료를 굽습니다.
    - 조작 : `Left Mouse`

[GrillMiniGame.mp4](attachment:060b4763-eb90-49a5-bb0c-fcc28eadf17e:GrillMiniGame.mp4)

- 가마솥 (끓이기)
    - 화살표를 따라 그려 재료를 끓입니다.
    - 조작 : L`eft mouse`

[BoilMiniGame.mp4](attachment:8775b7c4-7abb-4ad6-9c33-699c07cf8a99:BoilMiniGame.mp4)

- 맷돌
    - 손잡이를 잡고 돌려 재료를 갑니다.
    - 조작 : `Left Mouse`
    
    [MillMiniGame.mp4](attachment:b4626cef-6f45-4745-bf77-dca8479387c4:MillMiniGame.mp4)


</details>

---

## 🕹️ 플레이 사이클

![PlayCycle-페이지-2 drawio (2)](https://github.com/user-attachments/assets/639850aa-f16c-4db0-a234-4452b3156b40)


---

## ✏️ 기술 스택

**언어 및 프레임워크**

![c-sharp-c.228x256.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/83c75a39-3aba-4ba4-a792-7aefe4b07895/ce060a18-ef9c-41d6-b8cb-5a022946efd4/c-sharp-c.228x256.png)

                                          **C#**

![website.png](attachment:77ad5a7b-b86c-4c0b-a66d-20a0c2cc911c:website.png)

        **.Net Standard 2.1 (유니티 2022 기준)**

---

**개발 환경**

![unity.png](attachment:d8fc8e7f-2eba-4cb3-bd72-05f44356cdac:unity.png)

Unity 2022.3.17f1 (LTS)

![logo.png](attachment:916c3f29-4f47-49dc-a322-e59513ae83ac:logo.png)

 Visual Studio 2022

![windows.png](attachment:e65d34ab-b2ed-4e0b-be2a-df1b58aed9ec:windows.png)

Windows 10 / 11

![mac-os-logo.png](attachment:21852c8d-7e4b-4b79-93a6-430b1372ef59:mac-os-logo.png)

Windows 10 / 11

**데이터 및 협업 도구**

![sheets.png](attachment:ddf12b32-4b9d-46ca-9eed-450707fc51ce:sheets.png)

             Google Sheets

![github.png](attachment:18d5da67-1644-4ce6-b5ab-75af51a0c32e:github.png)

                    GitHub

![pngwing.com.png](attachment:2279b9cc-5286-4ae6-a2ce-97ae44193fcf:pngwing.com.png)

                     Notion

**사용 라이브러리 및 유틸리티**

- UniTask
- Unity Addressables
- Unity Analytics
- Unity Input System
- Newtonsoft.Json
- EzyCutSlice

**그래픽 환경**

- 2D 기반 개발 (3D 요소 일부 활용)

---

## 사용 에셋
Bamao Pack Fantasy GUI : https://assetstore.unity.com/packages/2d/gui/bamao-pack-fantasy-gui-299336

Epic RPG World Grass Land 2.0 : https://assetstore.unity.com/packages/2d/environments/epic-rpg-world-grass-land-2-0-267533

도봉옛길체 : https://gongu.copyright.or.kr/gongu/wrt/wrt/view.do?wrtSn=13333564&menuNo=200023 

메이플스토리 서체 : https://maplestory.nexon.com/Media/Font

배민체 : https://www.woowahan.com/fonts

---

## 🗓️ 개발 타임 라인 및 기획서

**📌 1~5주: 핵심 시스템 집중 개발**

| **주차** | **주요 작업** |
| --- | --- |
| **1주차** | **프로젝트 SA 작성**, 시스템 플로우 설계, 게임 콘셉트 정리, UI/UX 시안 및 요리 제작 구조 설계, 의뢰 시스템 틀 구성 |
| **2~4주차** | 요리, 의뢰, 채집, 미니게임, 인벤토리 시스템 개발 / 리소스 제작 ( 배경, 채집지, 요리도구 )  |
| **5주차** | 시연용 대표 퀘스트 3종 완성, UI 연동 및 기본 테스트, **MVP 버전 시연** |

**🛠️ 6~8주: 추가 기능 구현 · 보완 · 통합**

| **주차** | **주요 작업** |
| --- | --- |
| **6주차** | 도감 구현, 미니게임 추가 구현, 바다 채집 구현, 계절 변화, 저장 시스템 구현, 사운드 시스템 구현, 튜토리얼 구현,  |
| **7주차** | **알파 버전 테스트 진행**, 버그 Fix, WebGL 빌드 |
| **8주차** | **베타 버전(데모) 빌드 배포**, 유저 테스트 결과 대응, 피칭/시연 자료 제작 |

---

## 🔧 기술적인 도전 과제

<details>
<summary><strong>📊 Analytics</strong></summary>

- **도입 배경**
  - 설문 조사의 단점(주관적, 동기 부족)을 보완하기 위한 객관적 피드백 확보 필요
- **구현 방법**
  - 퀘스트 클리어, 아이템 해금 등 주요 분기점마다 필요한 데이터를 Unity Cloud에 전송하여 플레이어 진행 상황을 실시간으로 관리하고 분석할 수 있도록 구성했습니다.
- **개선 사항**
  - Analytics 도입을 통해 플레이어의 정량적, 구체적 행동 데이터 확보
  - UX 개선, 난이도 조정, 콘텐츠 리텐션 분석 등에 사용

</details>

<details>
<summary><strong>📦 Addressables</strong></summary>

- **도입 배경**
  - ResourcesLoad의 단점:
    1. 모든 에셋이 메모리에 동기적 로드 → 최적화 및 메모리 관리 한계
    2. 빌드에 전체 리소스가 포함 → 빌드 경량화 위협
  - Addressables의 장점:
    1. 필요시에만 Address 기반의 비동기 로드 → 로딩 시간 단축
    2. 그룹 및 Label 단위의 유연한 에셋 관리 → 빌드 최적화 및 효율적 메모리 제어
    3. 원격 콘텐츠 서버를 통한 부분 업데이트 → 운영 효율성 및 확장성 향상
- **구현 방법**
  - Addressables는 통합 매니저에서 관리되며, 각 씬과 콘텐츠에서 필요한 리소스를 동적으로 로드/언로드하도록 설계했습니다.
- **개선 사항**
  - 초기 빌드 크기 및 메모리 부담 감소

</details>

<details>
<summary><strong>🧩 Newtonsoft.Json</strong></summary>

- **도입 배경**
  - JsonUtility의 한계를 보완하고 복잡한 구조 직렬화/역직렬화를 위해 Newtonsoft.Json 도입
- **구현 방법**
  - 플랫폼(WebGL/Standalone) 별 저장 방식 분기
  - 커맨드 시스템에 OnNewDay 이벤트 등록하여 자동 저장 구현
- **개선 사항**
  - Dictionary, 중첩 클래스, 리스트 등도 유연하게 처리
  - JSON 출력 커스터마이징 가능

</details>

<details>
<summary><strong>🎵 Scriptable Object (Sound Library)</strong></summary>

- **도입 배경**
  - 사운드 리소스를 통합 관리하기 위해 Sound Library 구조 필요
- **구현 방법**
  - ScriptableObject 기반 SoundLibrary로 BGM, SFX, Ambience를 분류 저장
  - Dictionary 기반 검색 및 재생
- **개선 사항**
  - 사운드 관리의 효율성, 유지보수성, 확장성 향상

</details>

<details>
<summary><strong>🍳 Scriptable Object + Addressables (Cooking)</strong></summary>

- **도입 배경**
  - 미니게임마다 타이머, 판정 범위 등 커스터마이징 필요
  - 리소스를 필요 시점에 불러오는 동적 로딩 요구
- **구현 방법**
  - CookingMiniGameSO, CookingEffectSO로 규칙과 효과 분리
  - Addressables로 프리팹 비동기 로드 및 배치
- **개선 사항**
  - 유지보수성, 확장성, 초기 메모리 효율 향상

</details>

<details>
<summary><strong>🧬 Template Method Pattern</strong></summary>

- **도입 배경**
  - 공통된 흐름 + 각기 다른 미니게임 로직 구조화 필요
- **구현 방법**
  - 추상 클래스(CookingMiniGameBase)로 공통 로직 정의
  - UpdateGamePlay() 등 오버라이딩 방식으로 개별 게임 로직 구현
- **개선 사항**
  - 코드 중복 감소, 유지보수 및 확장 용이

</details>

<details>
<summary><strong>🧠 Strategy Pattern</strong></summary>

- **도입 배경**
  - 미니게임마다 실행 방식이 달라 유연한 전략 구조 필요
- **구현 방법**
  - ICookingMiniGameHandler 인터페이스 도입
  - Manager에서 전략 객체 주입 방식으로 처리
- **개선 사항**
  - 동적 전략 변경 가능, 확장성 확보

</details>

<details>
<summary><strong>📡 EventBus Pattern</strong></summary>

- **도입 배경**
  - 이벤트가 여러 시스템에 영향을 줄 때 의존성 문제 발생
- **구현 방식**
  - `EventBus` 클래스를 통해 Dictionary<Type, Delegate> 기반으로 이벤트 처리
  - 이벤트마다 고유 클래스 생성
- **개선 효과**
  - 느슨한 연결성 확보, 테스트 및 유지보수성 향상
- **적용 사례**
  - 아이템 획득 시 도감 갱신, NPC 조우 시 관계도 업데이트 등

</details>

<details>
<summary><strong>🎒 Inventory System</strong></summary>

- **도입 배경**
  - 다양한 인벤토리 유형과 UI 구조로 인한 복잡성 해결
- **적용 기술**
  - 팩토리 패턴, MVC 패턴
- **구현 방식**
  - `InventoryManager`: Enum 기반 인벤토리 유형 관리
  - `ItemStackManager`: 팩토리 인터페이스 기반 생성
  - UI 분리 및 재사용 가능한 MVC 구조 적용
- **개선 효과**
  - 다양한 인벤토리/UI 대응 가능한 추상화 구조 확보
  - UI 간 일관된 데이터 흐름 유지

</details>

---

## ⌨️ 코드 샘플 및 주석

<details> <summary><strong>📊 Analytics</strong></summary>

if (GameManager.Instance.isAnalyticsAgreed)
{
    var CookingMiniGameData = new AnalyticsCookingMiniGame("CookingMiniGameData")
    {
        miniGameName = CookingMiniGameManager.Instance.selectedCookingTool
    };
    AnalyticsService.Instance.RecordEvent(CookingMiniGameData);
}
플레이어가 개인 정보 제공에 동의했다면 실행되는 기능으로
플레이어의 행동에 따라 적절한 데이터를 유니티 클라우드에 전송합니다.

</details>
<details> <summary><strong>📦 Addressables</strong></summary>

public async Task<T> AddressablesLoadAsync<T>(string address, bool fallback = false)
{
    if (cache.TryGetValue(address, out var cacheHandle))
    {
        if (cacheHandle.IsValid())
        {
            return (T)cacheHandle.Result;
        }

        cache.Remove(address);
    }

    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
    await handle.Task;

    if (handle.Status == AsyncOperationStatus.Succeeded)
    {
        cache[address] = handle;
        return handle.Result;
    }

    Debug.LogError($"에셋 로드 실패: {address}");
    Addressables.Release(handle);

    if (fallback)
    {
        return await AddressablesLoadAsync<T>("Default." + typeof(T).Name);
    }
    else
    {
        return default(T);
    }
}
이 함수는 Addressables로 에셋을 비동기로 불러오며,
성공 시 캐시에 저장 후 반환하고, 실패 시 선택적으로 기본 에셋을 다시 로드합니다.
이미 로드된 에셋은 캐시에서 즉시 반환해 성능을 최적화합니다.

</details>
<details> <summary><strong>📢 EventBus</strong></summary>

public static class EventBus
{
    static Dictionary<Type, Delegate> eventsTable = new();

    public static void Subscribe<T>(Action<T> a)
    {
        if (eventsTable.TryGetValue(typeof(T), out var del))
            eventsTable[typeof(T)] = Delegate.Combine(a, del);
        else 
            eventsTable[typeof(T)] = a;
    }

    public static void UnSubscribe<T>(Action<T> a)
    {
        if (eventsTable.TryGetValue(typeof(T), out var del))
        {
            var cur = Delegate.Remove(del, a);
            if (cur == null) eventsTable.Remove(typeof(T));
            else eventsTable[typeof(T)] = cur;
        }
    }

    public static void Publish<T>(T evt)
    {
        if(eventsTable.TryGetValue(typeof(T),out var del))
        {
            (del as Action<T>)?.Invoke(evt);
        }
    }   
}

public class NPCFirstMetEvent
{
    public NPC npc;
    public NPCFirstMetEvent(NPC npc)
    {
        this.npc = npc;
    }
}
이벤트를 타입 기반으로 구독하고 발행할 수 있는 간단하고 유연한 이벤트 버스입니다.

</details>
<details> <summary><strong>💬 SmartPopup</strong></summary>

public class ConfirmPopUp : BasePopUp
{
    public Action confirmAction;
    public Func<string, Task<ResultOfInputAction>> inputAction;

    async void OnClickYesButton()
    {
        if (inputField.IsActive())
        {
            ResultOfInputAction resultType = await inputAction?.Invoke(inputField.text);
            ...
        }
        else
        {
            confirmAction?.Invoke();
            OnClickCloseButton();
        }
    }

    public void SetConfirm(string text)
    {
        alarmText.text = text;
    }

    public void SetConfirm(string text, Action action)
    {
        ...
        confirmAction = action;
    }

    public void SetConfirm<T>(string text, Func<T, bool> action)
    {
        ...
        inputAction = async (input) =>
        {
            T cast;
            try
            {
                cast = (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception e)
            {
                Debug.Log($"잘못된 입력 값 변환 실패{e.Message}");
                return ResultOfInputAction.WrongValueType;
            }

            try
            {
                bool success = (bool)(action?.Invoke(cast));
                return success ? ResultOfInputAction.Success : ResultOfInputAction.OutOfValue;
            }
            catch(Exception e)
            {
                Debug.Log($"실행 중 오류 발생{e.Message}");
                return ResultOfInputAction.ModifiedCollection;
            }
        };
    }
}
다양한 상황(알림/예아니오/입력)에 대응하는 스마트 팝업 시스템입니다.

</details>
<details> <summary><strong>📐 Mathf, Vector3 관련 수학 함수</strong></summary>

// 도마 미니게임 - 칼의 좌우 왕복 이동
float moveSpeed = 0.2f;
float x = Mathf.PingPong(Time.time * moveSpeed, range - margin * 2) + minX + margin;
transform.position = new Vector3(x, y, z);

// 끓이기 미니게임 - 선의 방향 비교 (벡터 내적)
float similarity = Vector3.Dot(userLine.normalized, targetLine.normalized);

// 맷돌 미니게임 - 회전 방향 및 속도 계산
Vector2 dir = mousePos - centerPos;
float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
6종의 미니게임에서 거리, 각도, 방향, 속도 등의 물리 기반 요소를 정량화하여
정밀하고 직관적인 조작 체감을 구현했습니다.

</details>

---

## 🗂️ 클라이언트 구조


![ClientStructure](https://github.com/user-attachments/assets/566cf0f5-8f5a-499d-9e23-f6a47bab79e4)

<details>
<summary><strong>📦 Extra Client Feat</strong></summary>
  
- 렌더링처리
    - SortingLayer: 백그라운드 < UI < Popup
    - PopupManager: 팝업이 등장할 때마다 새로운 SortingOrder 부여
      
- 사운드 시스템
    
    SoundManager (Singleton)
    ├─ (BGM, 효과음, 환경음) 오디오 종류를 구분해서 재생
    ├─ 사운드 데이터를 SoundLibrary라는 ScriptableObject에서 로드
    └─ SFX 재생 시 오디오 풀을 활용하여 중복 재생 방지
    
- 로컬 저장소 처리
    
    플랫폼(WebGL↔PC)별 저장 처리 이분화
    SaveLoadManager
    ├─ Save: PlayerGameData.SetSave();    
    ├─├─ SaveDataWeb (WebGL) : GameData → string → playerPref
    │   └─ SaveDataBasic (PC, 에디터) : GameData → string → Json
    └─ Load
    └─├─ LoadDataWeb(WebGL) : playerPref → string → GameData 
          └─ LoadDataBasic(PC, 에디터) : Json → string → GameData 
    
- UI 처리
    - UI를 상주 UI, Popup UI으로 이분화
    - 상주 UI:
        - UIManager에서 관리
        - Clickable Object(요리 도구, 편지함, 채집 나무 등)
    - Popup:
        - PopupManager에서 관리
        - Menu Popup, 의뢰함 Popup, 알림 Popup, 결과 Popup 등
</details>

---

## 💁‍♂️사용자 피드백 대응 및 개선 사항

<details>
<summary><strong>📝 설문조사 분석 보고서</strong></summary>
    
## ⏱️ 1. 플레이 시간 분석

- **설문 응답자 평균**: 30~40분
- **실제 애널리틱스 데이터**: 평균 12분

> 🔍 해석: 진입장벽 존재 — 실제 플레이 시간이 짧은 것으로 보아 게임 초반 흡입력 강화 필요
> 

---

## ✅ 2. 장점 (Pros)

| 항목 | 설명 | 기획 의도 반영 여부 |
| --- | --- | --- |
| 채집과 계절 요소 | 다양한 계절/지역 자원 수집이 신선하고 몰입감 있음 | ✅ |
| 재료 가공 미니게임 | 손맛과 조작의 재미 있다는 의견 다수 | ✅ |
| 시작 시 영상/스토리 | 시각·청각적 연출에 대한 만족도 높음 | ✅ |

> 🎯 요약: 기획 의도가 사용자에게 명확히 전달됨
> 

---

## ⚠️ 3. 단점 (Cons) 및 대응 방안

> 정렬 기준: 언급 빈도순
> 

| 문제점 | 설명 | 개선 방향 |
| --- | --- | --- |
| 레시피 추리 어려움 | 힌트 부족, 비직관적 설계 | 의뢰 편지 힌트 강조 |
| 미니게임 반복성/난이도 | 반복이 지루, 난이도 과도 | 난이도 완화 및 다양화 |
| 낚시 | 시청각적 효과 부족 | 찌 타이밍 시각/청각 피드백 강화 |
| 채집 정보 부족 | 계절/지역 채집 정보 부족 | 맵 선택창에 툴팁 제공 |
| 초반 안내 부족 | 튜토리얼 미흡 | 튜토리얼 UX 개선 |
| 닭, 멧돼지 차이 불명확 | 구분 어려움 | 미니게임 결과창에 수치 표시 |
| 자원 수급 어려움 | 쓸모없는 아이템 다수 | 자원 수급 지역 제한/집중화 |
| 게임 루프 단순함 | 반복성 높음 | 레벨 디자인 통한 지역 점진적 확장 |
| 도감 내 호감도 표시 미흡 | 수치 의미 불명확 | 도감에 ‘호감도’ 명시 필요 |
| 일부 조리도구 재료 파괴 | 일부 조리도구만 재료가 파괴 | 정상적 게임 의도인데 설명 부족. |
| 퀘스트 완료 보상 미지급 | 퀘스트 완료시 보상이 미지급 | 성공정도에 따른 보상 차등 지급 설명 |

---

## 🐛 4. 버그 리포트

### 🔴 상위 결함

- **UI 이동 버그** (송원석)
    
    더블클릭 시 집으로 복귀 불가 (지도 → 지역 더블클릭 시 발생)
    
- **요리 반복 버그** (강영준, 송원석)
    
    접시 이후 접시 재선택 시 이전 재료가 남아있음
    
- **다진 고추 무한 루프** (김아연)
    
    고추 양념장 클릭 시 게임 멈춤
    
- **퀘스트 중단 버그** (강영준)
    
    1448년 이후 퀘스트 미등장
    
- **대량 아이템 아이템 삭제 로직 미작동** (강영준)
    
    한번에 열개 이상의 아이템 삭제 시에 제대로 작동하지 않는 버그
    
- **저장/로드 이후 아이템 증감 오류** (강영준)
    
    저장 로드 이후 아이템이 제대로 증감하지 않는 버그
    

---

### 🟠 중위 결함

- **인트로 더블클릭 무한 로딩 및 세이브 없는 이어하기 오류** (송원석)
- **사냥 미끼 재료 이슈** (배리안) : "사냥 미끼 재료 더블클릭시 재료 소진: 큰 동물을 만나고 재료를 더블 클릭하면 던지지 않았는데도 재료가 소진되는 버그 존재”
- **인벤토리 재료 선택 오류** (강영준) : “접시 선택하고 A 재료 넣고 B 재료 넣었을 때 B를 접시에서 빼려고 인벤에서 B 재료를 선택하면 A 재료가 빠집니다. 인벤에서 선택한 재료를 접시에서 뺄 수 있도록 수정이 필요해 보입니다.”
- **멧돌 애니메이션 글리치** (김소연) : “멧돌 돌릴때 마우스를 원 기준으로 돌리면 마우스가 180도 부근에서 멧돌의 애니메이션이 훼까닥 해버리는 버그 존재.”
- **사냥 설명창 닫고 다시 안열림** (배리안)
- **전복 손질 다중 이미지 표시 오류** (김소연)
- **관계탭 슬롯 중복** (강영준)
- **밤 효과가 미표시** (강영준)
- **게임 결과 팝업이 오류**  (김소연)
- **메뉴 팝업 키(tab)가 오작동** (송원석)

---

### 🟡 하위 결함

- **창 닫기 시 UI 잔상** (송원석) : “바다와 산을 선택하지 않고 창을 닫으면 바다와 산 선택창이 남아 있는 버그 존재”
- **절구 사용 버그 (다진 고추)** (김아연)
- **퀘스트 설명글에 굴 강조 미표시** (김아연)

---

### ⚙️ 유지 항목

| 항목 | 설명 |
| --- | --- |
| 미니게임 중 도움말 시 시간 흐름 | 의도적 디자인 |
| 요리도구 클릭 반복 시 저글링 현상 | 재미 요소로 유지 |
| 일부 조리도구 사용시 재료 미반환 | 시스템상 정상 동작 |

---

</details>

<details>
<summary><strong>📊 애널리틱스 분석 보고서</strong></summary>

 - **레시피 추리에 대한 어려움**
    
    ![스크린샷 2025-05-28 오후 8.08.12.png](attachment:a7236036-69ba-4411-b898-0d02b6a823de:스크린샷_2025-05-28_오후_8.08.12.png)
    
    ![스크린샷 2025-05-28 오후 8.08.22.png](attachment:82834892-5d3c-49cd-9e55-0b45f0e92515:스크린샷_2025-05-28_오후_8.08.22.png)
    
    ![스크린샷 2025-05-28 오후 8.09.21.png](attachment:7181a5e4-a8be-498a-890b-c049d3c547f9:스크린샷_2025-05-28_오후_8.09.21.png)
    
    유저 피드백 중 가장 빈번하게 언급된 문제는 레시피 추리의 난이도입니다.
    미니게임 플레이 수는 상당하지만, 퀘스트 완료 횟수는 매우 낮고, 특히 초기 튜토리얼 퀘스트만 반복 완료되는 현상이 강하게 나타납니다.
    이는 대부분의 퀘스트가 요구하는 요리를 어떻게 만드는지 직관적으로 알 수 없는 설계 때문으로 분석됩니다.
    
    **개선 방향**
    
    - 퀘스트 편지에 명확한 힌트 제공
    - 레시피 해금 구조 개선
    - 실패 경험을 통한 학습 유도
- **튜토리얼 안내 부족**
    
    ![스크린샷 2025-05-28 오후 8.10.05.png](attachment:eec6119b-1a0c-4a38-93f6-10588d2786df:스크린샷_2025-05-28_오후_8.10.05.png)
    
    ![image.png](attachment:71800352-1318-4d2d-b846-9ff906db0db9:image.png)
    
    애널리틱스에 따르면 대부분의 유저가 튜토리얼을 시청하고 있음에도 불구하고,
    피드백에서는 초반 안내 부족이 반복적으로 지적되었습니다.
    즉, 튜토리얼이 존재는 하지만 핵심 시스템 설명이 부족하여 유저가 게임의 구조를 이해하지 못한 채 진입장벽을 느끼는 상황입니다.
    
    **개선 방향**
    
    - 튜토리얼에서 요리 흐름(채집 → 요리 → 퀘스트)을 구체적으로 시각적으로 설명
    - 초반 퀘스트를 튜토리얼과 유기적으로 연계
- **요리 미니게임 사용 분포**
    
    ![스크린샷 2025-05-28 오후 8.11.18.png](attachment:913e98c5-115e-49de-879a-a5b61f3ebee3:스크린샷_2025-05-28_오후_8.11.18.png)
    
    가장 많이 사용된 미니게임은 자르기(Cutting)이며,
    이는 요리 방법을 몰라 플레이어들이 가장 기본적인 조작부터 시도하는 경향을 반영합니다.
    반면, 굽기(Grill) 미니게임은 가장 적은 사용량을 보였는데,
    이는 초기 퀘스트에서 굽기 도구를 요구하지 않기 때문으로 분석됩니다.
    
    **개선 방향**
    
    - 특정 미니게임이 사용되지 않는 현상 완화
    - 굽기 미니게임을 자연스럽게 포함한 튜토리얼 또는 퀘스트 추가
    - 레시피 구조를 재조정하여 다양한 도구 활용을 유도
- **아이템 해금의 편중 현상**
    
    ![스크린샷 2025-05-28 오후 8.12.07.png](attachment:42df2514-e69a-471f-857f-3d84600ad2e8:스크린샷_2025-05-28_오후_8.12.07.png)
    
    획득 가능한 아이템 수량을 보면, 채집으로 직접 얻을 수 있는 아이템은 해금 빈도가 높고,
    가공(요리)을 통해 얻어야 하는 아이템은 해금 빈도가 낮습니다.
    
    ![스크린샷 2025-05-28 오후 8.12.24.png](attachment:50dd4304-e32f-46e4-9a5e-789c397525e0:스크린샷_2025-05-28_오후_8.12.24.png)
    
    ![스크린샷 2025-05-28 오후 8.13.46.png](attachment:57468ace-5fd2-41d1-ad87-d79665635254:스크린샷_2025-05-28_오후_8.13.46.png)
    
    ![스크린샷 2025-05-28 오후 8.12.32.png](attachment:ffe5d3de-22c8-48ed-b847-929bf00c249e:스크린샷_2025-05-28_오후_8.12.32.png)
    
    예시로 ‘멸치’는 바다에서 바로 채집 가능해 31회 해금되었지만,
    이를 활용한 ‘구운 멸치’나 ‘손질된 멸치’는 각각 2회, 7회로 현저히 적습니다.
    
    **개선 방향**
    
    - 요리 결과물의 가치를 높이는 보상 구조 설계
    - 채집 아이템과 가공 아이템 간의 해금 빈도 차이 완화
    - 퀘스트에서 완성 요리를 명확히 요구함으로써 플레이어 유도
- **시간 흐름에 따른 이탈**
    
    ![스크린샷 2025-05-28 오후 8.14.37.png](attachment:d02838c7-0a36-4648-898e-0ec434931845:스크린샷_2025-05-28_오후_8.14.37.png)
    
    플레이 데이터는 시간이 지날수록 급격히 감소하고 있습니다.
    이는 유저 피드백에서도 언급된 반복적인 게임 루프 구조와도 일치하며,
    뒤로 갈수록 신규 콘텐츠 부족, 도전욕구 부족이 주된 원인으로 보입니다.
    
    **개선 방향**
    
    - 지역 확장, 계절 변화, 재료 다양화 등 단계별 콘텐츠 해금
    - 퀘스트와 아이템 도감 연동 강화
    - 유저가 성장을 체감할 수 있는 장기 루프 설계

</details>

---

## 🏁 추가 개선 가능 사항 설정

- **Bug Fix**
    - 설문에 제보된 버그 Fix
      
- **단점 개선**
    - UX 향상
        - 찌 타이밍 시각/청각 피드백 강화
        - 맵 선택창에서 루트 아이템 정보 툴팁 제공
        - 튜토리얼 UX 개선 ( 퀘스트 호환 방식 )
        - 사냥 미니게임 결과창에 수치 표시
        - 도감에 ‘호감도’ 명시 필요
        - 일부 요리도구만 재료 파괴 → 정상적 게임 의도인데 설명 부족.
        - 인벤토리 아이템 합치기 기능 추가
        - 전체 화면 지원 (해상비가 맞지 않는 경우 전체화면시 화면 잘림 문제)
    - 힌트 부족
        - NPC 퀘스트 완료시 → 도감 일부 공개, 힌트 편지 공개, 의뢰 편지 힌트 전달 개선
    - 단계적 컨텐츠 해금 구현
        - 레벨디자인 - 기획 테이블 대폭 수정
        - 아이템 루트 지역 제한/집중
        - 단계적 지역 해금

- 미구현 기능
    - 엔딩 구현

- 기술적 개선
    - 씬별 필요 에셋 그룹화
        - 씬(채집, 요리, 메인) 및 공통 에셋으로 그룹화

- 정식출시 염두 추가 기능
    - UI 개편
    - NPC 관계성 강화 ( 이벤트 발생 )
    - 다국어 지원

---

## 🖨️ 패치 로그

- 05.23 베타 버전(데모) 배포
- 05.27 HotFix
    - 밤 효과가 제대로 표시되지 않는 문제 fix
    - 게임 결과 팝업이 올바르게 나타나지 않는 문제 fix
    - 아이템 삭제가 제대로 이루어지지 않는 문제 fix
    - 저장/로드 이후 아이템 증감이 제대로 이루어지지 않는 문제 fix
    - 메뉴 팝업 키(tab)가 제대로 작동하지 않는 문제 fix
    - 의뢰 편지 가독성 향상
    - 도감 가독성 향상
    - 미니게임 밸런스 조정(굽기 게임 시간 증가, 썰기 게임 난이도 완화)
- 0.2 patch (예정)
    
  

---

## 🚨  트러블 슈팅

<details>
<summary><strong>📦 Unity WebGL 빌드에서 2D 이미지 Material 수정 미적용 문제</strong></summary>
### 문제 상황

인게임에서 밤이 되면, 커스텀 셰이더 Material의 수정을 통해 2D 이미지의 채도와 밝기를 낮추는 동작을 수행하려합니다. 에디터에서는 잘 작동하지만, 빌드(WebGL)시 해당 기능이 작동하지 않는 문제가 있었습니다. 해당 Material은 ScriptableObject에 저장되어 Addressable을 통해 불러오고 있습니다.

### 원인 규명 과정

WebGL에서의 동작에 대한 이해가 적었기에 우선 가능한 경우의 수를 추린 다음 Debug.Log를 통해 가능성을 좁히는 방식으로 접근했습니다.

**가능성 정리**

1. 커스텀 쉐이더가 WebGL에서 작동하지 않는 코드임
2. ScriptableObject가 Addressable에 의해 제대로 로드되지 않음
3. ScriptableObject에 저장된 Material이 적절히 참조되지 않음

**1차 테스트 - 셰이더 내부 작동 확인**

```csharp

csharp
Debug.Log($"saturationCurve값: {nightMat.GetFloat("_Saturation")}, lightnessCurve값: {nightMat.GetFloat("_Lightness")}");

```

⇒ 셰이더 내부 변수 값은 정상 작동하는 것 확인. 또한, GPT에게도 문의하여 WebGL 빌드환경에서 작동하지 않을 가능성은 적다고 판단.

**2차 테스트 - Addressable 로드 및 메터리얼 확인**

```csharp

csharp
ManagerContainer so = await AddressablesLoader.Instance.AddressablesLoadAsync<SOContainer>("NightContainer.SO");

if (so != null && so.nightMaterial != null && so.saturationCurve != null && so.lightnessCurve != null)
{
    Debug.Log("SO 로드 성공 및 내부 메터리얼이 잘 채워져있음");
}

```

⇒ Addressable을 통한 SO 및 메터리얼이 잘 로드된 것 확인.

**테스트 결과**: 기존 추론 가능성은 문제가 아니었으므로 다른 가능성 추론. 혹시 **WebGL 과정에서 Image 컴포넌트에 할당된 메터리얼과 Addressable로 불러온 메터리얼이 서로 다른 메터리얼로 인식되는 것은 아닐까?**

**3차 테스트 - 이미지 컴포넌트에 메터리얼을 동적으로 할당 (참조 연결 재설정)**

```csharp
csharp
foreach (var image in images)
{
    image.material = DayAndNightManager.Instance.nightMat;
}
```

⇒ 결과: 메터리얼 값 수정이 이미지에 정상적으로 반영됨.

### 문제 해결 방식과 효과

- **원인**: 어드레서블에 저장된 메터리얼과, 에디터에서 컴포넌트에 할당된 메터리얼이 서로 다른 객체로 인식됨
- **근본적 원인**: 빌드 과정에서 하나의 에셋에 대한 양쪽(어드레서블, 컴포넌트)에서의 참조가 분리된 것으로 추측. 보다 정확한 원인은 추가적인 테스트가 필요할 것 같음
- **해결 방법**: 이미지 컴포넌트에 메터리얼을 동적 할당하여 정상적으로 메터리얼 변화를 이미지에 반영함

</details>

<details>
<summary><strong>📦 DOTween 시퀀스 재활용 문제</strong></summary>
### 문제 상황

Tween(애니메이션)을 시퀀스로 엮어 저장해두고 재활용함으로써 객체 생성/파괴 비용을 절감하고자 하였습니다. 그러나 한번 실행된 트윈이 재실행되지 않는 문제가 있었습니다.

### 원인 규명 과정

**1차 문제 - 시퀀스 재실행 불가**

- **문제 상황**: 디버그 모드를 통해 시퀀스가 두번째로 호출될 시 실행되지 않는 문제를 식별
- **기존 코드**:

```csharp
csharp
Init()
{
    showSeq = DOTween.Sequence();
    showSeq.Pause();
    showSeq.AppendCallback(() =>
    {
        gameObject.SetActive(true);
        transform.position = originPos;
    });// 초기값을 설정해주려는 의도.
    showSeq.Join(transform.DOMove(originPos + new Vector3(-10, -10, 0), duration));
}
```

- **원인**: 시퀀스는 재생이 끝나면 자동으로 Kill() 메서드가 실행되고, 시퀀스에 저장된 애니메이션은 메모리에서 삭제됩니다. 이는 SetAutoKill 프로퍼티가 기본값으로 활성화(true)되어 있기 때문입니다.
- **해결**: `showSeq.SetAutoKill(false);`를 추가하여 해결

**2차 문제 - 애니메이션 시작 위치 오류**

- **문제 상황**: 테스트 시 시퀀스가 실행은 되지만, 애니메이션이 의도한 위치가 아닌 다른 곳에서 시작되는 문제 발견
- **수정된 코드**:

```csharp
csharp
private void Init(NPCArea area)
{
    showSeq = DOTween.Sequence();
    showSeq.Pause();
    showSeq.SetAutoKill(false);
    showSeq.AppendCallback(() =>
    {
        gameObject.SetActive(true);
        transform.position = originPos;
    });
    showSeq.Join(transform.DOMove(originPos + new Vector3(-10, -10, 0), duration));
}
```

- **원인**: 초기값 설정은 AppendCallback으로 할 수 없습니다. AppendCallback으로 초기값을 설정하면 잠깐동안 gameObject가 해당 위치로 이동하지만, 동시에 실행되는 트윈(transform.DOMove)은 애니메이션 객체이고 연속된 값 설정을 하기 때문에 Callback에서의 위치 설정을 무시합니다. 초기값은 트윈이 생성된 시점의 오브젝트 상태로 고정되며, 이를 명시적으로 지정하려면 `Tween.From(startValue)`를 사용해야 합니다.

### 문제 해결 방식과 효과

**최종 해결 코드**:

```csharp
csharp
showSeq = DOTween.Sequence();
showSeq.Pause();
showSeq.SetAutoKill(false);
showSeq.AppendCallback(() =>
{
    gameObject.SetActive(true);
});
showSeq.Join(transform.DOMove(originPos + new Vector3(-10, -10, 0), duration).From(originPos));
```

- AppendCallback에서 초기값 설정을 제거하고, From()에서 시작 위치를 명시적으로 지정
- SetAutoKill(false)로 시퀀스 재활용 가능하도록 설정
- 결과적으로 시퀀스가 정상적으로 재실행되며, 의도한 위치에서 애니메이션이 시작됨

</details>

<details>
<summary><strong>📦 3D 미니게임 씬 로드 최적화 문제</strong></summary>

### 문제 상황

3D로 개발한 요리 미니게임 씬의 로딩이 다소 오래 걸리는 현상을 발견했고, 이에 관한 최적화의 필요성을 느꼈습니다.

### 원인 규명 과정

**문제 -** 

- **문제 상황**: 요리 미니게임을 최대한 가볍게 설계하려고 했으나, Profiler 상 검사했을 때 다소 긴 LoadSceneOperation이 나타났고, 이는 플레이 경험에 불쾌함을 줄 여지가 있었습니다.
- **원인**:  미니게임은 별도의 전환 효과나 로딩 UI 없이, SceneManager에서 Additive 방식으로 즉각적으로 불러오는 구조이기 때문에 초기 렌더링 비용을 최소화해야 빠른 진입 속도를 확보할 수 있습니다.
- **해결**: 정적인 오브젝트에 Static Batching을 적용하고 리얼타임으로 설정된 light를 미리 베이크하는 방식으로 바꿨습니다. 또한 불필요한 그림자를 제거할 수 있는 No Shadow Mode를 사용하였습니다. 미니게임에 쓰이는 리소스의 텍스처를 압축하고, Main Camera의 Clipping을 최소화해서 렌더링 비용을 절감했습니다. 마지막으로 Update문 내 무거운 연산이 있는지 체크했습니다.

### 문제 해결 방식과 효과

- 미니게임 씬 로딩 시간이 확연히 단축
- 불필요한 실시간 계산이 없으므로 WebGL에서의 성능도 개선
</details>

---

## 📊 프로젝트 결과 및 성과

**🚩 게임적 성과**

- 동화 기반 캐릭터 10종 이상 완성 및 퀘스트 연동 스토리 구현
- 총 5종의 요리 가공 미니게임 + 조합 시스템 완성
- 감성적이면서도 직관적인 2D 캐쥬얼 UI/그래픽 스타일 적용
- 실제 유저 피드백 기반 튜토리얼, 로딩 화면, 엔딩 연출 등 콘텐츠 확장
- 완결된 루프에 이야기가 곁들여진 한국형 감성-도감 시뮬레이션 완성
- PC 전용 -> 웹빌드 전환 배포 성공: 웹빌드 전환 과정에서 다수의 트러블 슈팅 발생했지만 극복

**🧾 유저 테스트**

- 유저 테스트 수행 → Analytics와 설문 병행을 통한 객관적 피드백 반영

**🤝 협업 및 커뮤니케이션 경험**

- 기획자와 개발자 간의 커뮤니케이션:
    - 공유 기획 테이블을 통한 기획/개발 친화적인 아이템 매핑 테이블 작성
- 팀 전체 커뮤니케이션:
    - 정기적 오전 스크럼을 통해 진행 상황 공유.
    - 프로젝트 막바지에, 최우선 개발 과제들을 설정함으로써 자율적, 효율적 업무 분담

**🗣️ 프로젝트 회고**

- 아쉬운 점
    - 아트나 레벨 디자인 같은 다양한 파트의 사람들과의 협업을 경험하지 못 함
- 향후 계획
    - 0.2 patch 예정(엔딩 구현, UX향상, 버그 Fix 등)
    - 스팀 출시 준비
