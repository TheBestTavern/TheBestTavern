using System.Collections;
using System.Collections.Generic;
using EzySlice;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using Cinemachine;

/// <summary>
/// 칼 자동으로 움직이는 버전 스크립트
/// </summary>
public class CookingKnife_Test : MonoBehaviour
{
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private Transform knife;

    [SerializeField] private Material cutMaterial; // 잘린 단면의 메테리얼

    [SerializeField] private bool isSlicing = false;

    private bool canSlice = true;

    private List<GameObject> slicePieces = new();

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (isSlicing) return;

    //    ISlicable sliceObject = other.GetComponent<ISlicable>();

    //    if (sliceObject != null)
    //    {
    //        SliceObject(other.gameObject, cutMaterial);
    //        SliceDelay(other).Forget();
    //    }
    //}

    float maxX, minX, y, z;

    // 바운드 계산
    public void GetBounds()
    {

    }
    public void TrySliceObject()
    {
        if (!canSlice) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (var hit in hits)
        {
            ISlicable sliceObject = hit.GetComponent<ISlicable>();

            if (sliceObject != null)
            {
                isSlicing = true;

                
                StartSlicing();
                SliceObject(hit.gameObject, cutMaterial);
                Destroy(hit.gameObject);

                //isSlicing = false;
                break;
            }
        }
    }

    public SlicedHull SliceObject(GameObject obj, Material material)
    {
        var slicedObj = obj.Slice(transform.position, Vector3.right, material);

        if (slicedObj != null)
        {
            GameObject upper = slicedObj.CreateUpperHull(obj, material);
            GameObject lower = slicedObj.CreateLowerHull(obj, material);

            var upperCollider = upper.GetComponent<Collider>();
            if (upperCollider == null)
            {
                upper.AddComponent<BoxCollider>();
            }

            var lowerCollider = lower.GetComponent<Collider>();
            if (lowerCollider == null) lower.AddComponent<BoxCollider>();

            if (upper.GetComponent<ISlicable>() == null)
            {
                upper.AddComponent<CookingSlice>();
            }

            // 잘린 조각
            GameObject piece = lower;
            slicePieces.Add(piece);
            piece.transform.position = obj.transform.position + Vector3.right * 0.1f;

            Rigidbody pieceRb = piece.AddComponent<Rigidbody>();
            pieceRb.isKinematic = false;
            pieceRb.mass = 1f;
            //pieceRb.useGravity = true;
            pieceRb.AddForce((piece.transform.position - transform.position).normalized * 0.01f, ForceMode.Impulse);

            // 잘리고 남은 조각
            upper.transform.position = obj.transform.position;
            upper.transform.rotation = obj.transform.rotation;
            upper.transform.localScale = obj.transform.localScale;

            Rigidbody upperRb = upper.AddComponent<Rigidbody>();
            upperRb.useGravity = true;
            upperRb.isKinematic = true;

            if(upper == null)
            {
                CookingMiniGameManager.Instance.InstantGameOver();
            }
        }
        return slicedObj;
    }

    // 마우스에 카메라 달기
    //public void MouseWithKnife()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = 0.7f;

    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
    //    worldPos.z = 0.3f;

    //    transform.position = worldPos;
    //}

    // 칼 좌우로 이동
    public void MoveKnife()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            CookingSlice sliceobject = hit.GetComponent<CookingSlice>();
            //Debug.Log(sliceobject.name);
            if (sliceobject != null)
            {
                Renderer renderer = sliceobject.GetComponent<Renderer>();

                if (renderer != null)
                {

                    Bounds bounds = renderer.bounds;

                    if (bounds.size.x < 0.1f)
                    {
                        if (canSlice)
                        {
                            canSlice = false;

                            //Debug.Log(bounds.size.x);

                            CookingMiniGameManager.Instance.InstantGameOver();
                            Debug.Log("바로종료");
                        }
                        return;
                    }

                    if (isSlicing) return;

                    maxX = bounds.max.x;
                    minX = bounds.min.x;
                    y = bounds.center.y + 0.1f;
                    z = bounds.center.z - 0.2f;

                    float moveSpeed = 0.4f;
                    float x = Mathf.PingPong(Time.time * moveSpeed, maxX - minX) + minX;
                    transform.position = new Vector3(x, y, z);
                }
            }
        }
    }

    private async void StartSlicing()
    {
        //yield return KnifeAnimation().WaitForCompletion();
        //yield return knife.DOLocalMoveY(0.5f, 0.3f).OnComplete(() => { knife.DOLocalMoveY(0.3f, 0.3f).OnComplete(() => knife.DOLocalMoveY(0.43f, 0.5f)); });
        try
        {
            await DOTween.Sequence().Append(knife.DOLocalMoveY(0.5f, 0.3f))
            .Append(knife.DOLocalMoveY(0.3f, 0.3f))
            .Append(knife.DOLocalMoveY(0.43f, 0.5f))
            .AsyncWaitForCompletion();
        }
        catch
        {
            Debug.Log("에러");
        }
        finally
        {
            isSlicing = false;
        }
    }
    public void KnifeAnimation()
    {
        knife.DOLocalMoveY(0.5f, 0.3f).OnComplete(() => { knife.DOLocalMoveY(0.3f, 0.3f).OnComplete(() => knife.DOLocalMoveY(0.43f, 0.5f)); });
    }
    

    private void Update()
    {
        
        if (isSlicing) return;

        MoveKnife();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            TrySliceObject();
        }
    }
    #region
    private async UniTask SliceDelay(Collider collider)
     {
         Collider knifeCollider = GetComponent<Collider>();
         knifeCollider.enabled = false;
         isSlicing = true;
         float sliceTimer = 0f;
         await UniTask.WaitUntil(() =>
         {
             sliceTimer += Time.deltaTime;
             if (collider == null) return true;
             if (sliceTimer > 0.1f) return true;
             return !knifeCollider.bounds.Intersects(collider.bounds);
         });
         if (knifeCollider != null)
             knifeCollider.enabled = true;

         knifeCollider.enabled = true;
         isSlicing = false;
     }
    #endregion

    public float GetPiecesRatio()
    {
        if (slicePieces.Count < 2) return 0f;
        List<float> size = new();

        foreach (var piece in slicePieces)
        {
            Renderer renderer = piece.GetComponent<Renderer>();
            if (renderer != null) size.Add(renderer.bounds.size.x);
        }

        float minX = size.Min();
        float maxX = size.Max();
        float ratio = minX / maxX;
        return ratio;

        //if(ratio >= 0.95f)
        //{
            
        //}
    }
}
