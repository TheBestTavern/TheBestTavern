using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 절구 미니게임 - 리듬게임형
/// (반복적으로 내려오는 리듬에 맞춰 정확한 타이밍 클릭)
/// </summary>
public class CookingGrindMiniGame : CookingMiniGameBase
{

    // 노트
    [SerializeField] GameObject notePrefab = null;
    [SerializeField] Transform noteAppear = null; // 노트 생성 위치
    [SerializeField] Transform noteDisappear = null; // 노트 파괴 위치

    public Transform parentTransform; // 부모

    // 버튼 (판정)
    public Button judgeButton = null;
    public List<Note> notePool = new();

    // 이펙트
    [SerializeField] private Animator animator;
    [SerializeField] private Effect effect;

    private int perfect, bad, good, miss;

    protected override float GetTimer()
    {
        return data.GrindTimer;
    }

    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }

    private IEnumerator Delay(GameObject note, float delay)
    {
        yield return new WaitForSeconds(delay);
        note.SetActive(false);
    }


    private Note GetNotePool()
    {
        return notePool.FirstOrDefault(x => !x.NoteImage.activeSelf);
    }

    private Note SpawnNote()
    {
        Note note = Instantiate(notePrefab, noteAppear.position, Quaternion.identity, parentTransform).GetComponent<Note>();
        note.Init(noteAppear.localPosition, judgeButton.transform.localPosition, data.NoteTravelTime);

        notePool.Add(note); // notePool에 생성된 노트를 추가

        return note;
    }

    private void NoteHitEffect()
    {
        animator.SetTrigger("NoteHit");
    }

    // 등급 판정
    public CookingResultGrade JudgeGrade()
    {
        
        if (perfect >= data.PerfectCount)
        { 
            return CookingResultGrade.Legendary; 
        }
        else if (good >= data.GoodCount)
        {
            return CookingResultGrade.Rare;

        }
        else if (bad >= data.BadCount)
        {
            return CookingResultGrade.Common;

        }
        else if (miss >= data.MissCount)
        {
            return CookingResultGrade.Failed;
        }

        return CookingResultGrade.Failed;
    }

    protected override void UpdateGamePlay()
    {
        if(isGameOver) return;

        double noteElapsedTime = 0d;
        noteElapsedTime += Time.deltaTime;

        // 노트 총 7회 , 2초 간격으로 내려옴
        if (noteElapsedTime >= data.NoteRespwanTime)
        {
            Note note = GetNotePool();

            if (note != null)
            {
                note.Init(noteAppear.localPosition, judgeButton.transform.localPosition, data.NoteTravelTime);
                note.Show();
            }
            else
            {
                SpawnNote();
            }

            noteElapsedTime = 0f;
        }

        // 플레이어가 버튼을 누르거나 스페이스를 누른 그 시간으로 비교를 한다
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float inputTiming = Time.time;
            //bool isHit = false; // 노트가 판정처리 되었는지 확인

            List<Note> activeNotes = notePool.Where(x => x.NoteImage.activeInHierarchy).ToList();

            foreach (var activeNote in activeNotes)
            {
                float diff = Mathf.Abs(activeNote.noteJudgeTime - inputTiming);

                if (diff <= data.PerfectDiff)
                {
                    activeNote.Hide();
                    // activeNote.gameObject.SetActive(false);
                    Debug.Log($"Perfect {diff}");
                    // note hit 애니메이션
                    NoteHitEffect();

                    perfect++;
                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(0);
                    break;
                }
                if (diff <= data.GoodDiff)
                {
                    activeNote.Hide();

                    // activeNote.gameObject.SetActive(false);
                    Debug.Log($"Good {diff}");

                    // note hit 애니메이션
                    NoteHitEffect();

                    good++;

                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(1);

                    break;
                }
                if (diff <= data.BadDiff)
                {
                    activeNote.Hide();
                    // activeNote.gameObject.SetActive(false);
                    Debug.Log($"Bad {diff}");

                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(2);

                    bad++;

                    break;
                }
                if (diff <= data.MissDiff)
                {
                    Debug.Log("Miss");
                    StartCoroutine(Delay(activeNote.gameObject, 0.3f));

                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(3);

                    miss++;

                    break;
                }
            }
        }

        // 노트 판정 시각 + 0.5f 까지 입력 없으면 무조건 Miss
        foreach (var note in notePool)
        {
            if (!note.NoteImage.activeInHierarchy) continue;

            if (Time.time > note.noteMissTime)
            {
                Debug.Log("Miss");
                note.Hide();
            }
        }

    }

    public override void StartGame()
    {
        //SpawnNote();
        //isGameOver = false;
        elapsedTimer = 0f;
        playTime = 0f;
        //timer = 15f;
    }

    public override void StopGame()
    {
        var grade = JudgeGrade();
        CookingMiniGameManager.Instance.SetMiniGameResult(grade);
        //PlayEffect(grade);
    }

}
