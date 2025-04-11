using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 절구 미니게임 (리듬게임)에 쓰이는 노트
/// </summary>
public class Note : MonoBehaviour
{
    public GameObject note;
    
    //[SerializeField][field: Range(0f, 100f)] private float noteSpeed = 700f;

    public Vector3 startPos;
    public Vector3 endPos;
    public float noteSpawnTime; // 노트 생성 시각
    public float noteJudgeTime; // 노트가 판정선에 도달하는 정확한 시각
    public float noteMissTime; // 노트가 판정 범위를 벗어나는 시각

    public Coroutine noteCoroutine;


    public void Init(Vector3 start, Vector3 end, float travelTime)
    {
        startPos = start;
        endPos = end;
        noteSpawnTime = Time.time;
        noteJudgeTime = Time.time + travelTime;
        noteMissTime = noteJudgeTime + 0.5f;
    }

    private void Update()
    {
        float progress = Mathf.InverseLerp(noteSpawnTime, noteJudgeTime, Time.time);
        transform.localPosition = Vector3.Lerp(startPos, endPos, progress);
    }
}
