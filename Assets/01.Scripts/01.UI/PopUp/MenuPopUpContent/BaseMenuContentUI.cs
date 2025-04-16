using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TAB 메뉴 컨텐츠들 베이스
/// </summary>
public class BaseMenuContentUI : MonoBehaviour
{
    // 컨텐츠 부모 트랜스폼
    public Transform contentParent;

    // 컨텐츠 프리펩
    public GameObject contentPrefab;

    // 컨텐츠 활성화
    public virtual void OnEnable()
    {
        // 컨텐츠 생성
        CreateContent();
    }

    // 컨텐츠 생성 함수
    public virtual void CreateContent()
    {

    }
}
