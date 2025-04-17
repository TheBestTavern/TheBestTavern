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

    [SerializeField] private double noteElapsedTime = 0d; // 노트 생성 후 누적 시간

    public Image timerImage;

    private float noteTravelTime = 1.5f; // 노트가 도착까지 걸리는 시간 

    private float noteRespwanTime = 2f; // 2초마다 노트 생성

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


    private void Awake()
    {
        CookingMiniGameManager.Instance.GetCurrentMiniGame(this);
    }


    private IEnumerator Delay(GameObject note, float delay)
    {
        yield return new WaitForSeconds(delay);
        note.SetActive(false);
    }


    // 타이밍 판정
    public void JudgeTiming(float time)
    {
        // 허용오차
        // perfect += 0.15
        // good 0.3
        // bad 0.5
        // miss 오차초과 || 미입력

        foreach (var note in notePool)
        {

        }

    }

    // 등급 판정
    public void JudgeGrade()
    {
        // 상 : perfect >=4
        // 중 : Good >= 4
        // 하 : Bad >= 4
        // 실패 : Miss >= 4
    }


    private Note GetNotePool()
    {
        return notePool.FirstOrDefault(x => !x.NoteImage.activeSelf);
    }

    private Note SpawnNote()
    {
        Note note = Instantiate(notePrefab, noteAppear.position, Quaternion.identity, parentTransform).GetComponent<Note>();
        note.Init(noteAppear.localPosition, judgeButton.transform.localPosition, noteTravelTime);

        notePool.Add(note); // notePool에 생성된 노트를 추가

        return note;
    }

    private void NoteHitEffect()
    {
        animator.SetTrigger("NoteHit");
    }

    protected override void UpdateGamePlay()
    {
        noteElapsedTime += Time.deltaTime;

        timerImage.fillAmount = (float)Time.time / 15f;

        // 노트 총 7회 , 2초 간격으로 내려옴
        if (noteElapsedTime >= noteRespwanTime)
        {
            Note note = GetNotePool();

            if (note != null)
            {
                note.Init(noteAppear.localPosition, judgeButton.transform.localPosition, noteTravelTime);
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

                if (diff <= 0.15f)
                {
                    activeNote.Hide();
                    // activeNote.gameObject.SetActive(false);
                    Debug.Log($"Perfect {diff}");
                    // note hit 애니메이션
                    NoteHitEffect();


                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(0);
                    break;
                }
                if (diff <= 0.3f)
                {
                    activeNote.Hide();

                    // activeNote.gameObject.SetActive(false);
                    Debug.Log($"Good {diff}");

                    // note hit 애니메이션
                    NoteHitEffect();

                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(1);

                    break;
                }
                if (diff <= 0.5f)
                {
                    activeNote.Hide();
                    // activeNote.gameObject.SetActive(false);
                    Debug.Log($"Bad {diff}");

                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(2);

                    break;
                }
                if (diff <= 0.6f)
                {
                    Debug.Log("Miss");
                    StartCoroutine(Delay(activeNote.gameObject, 0.2f));

                    // 판정 텍스트 이미지 애니메이션
                    //effect.JudgeEffect(3);

                    break;
                }
            }

            // 입력이 0.7f안에도 들어오지 않았다면 그냥 무효 처리되는 것
        }


        // 이펙트, 판정텍스트 등 연출


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
    }

    public override void StopGame()
    {
        
    }


}
