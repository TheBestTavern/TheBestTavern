using System.Collections;
using System.Collections.Generic;
using EzySlice;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CookingKnife : MonoBehaviour
{
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private CookingKnife knife;

    [SerializeField] private Material cutMaterial; // 잘린 단면의 메테리얼

    private bool isSlicing = false;

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

    public void TrySliceObject()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);

        foreach ( var hit in hits )
        {
            ISlicable sliceObject = hit.GetComponent<ISlicable>();

            if (sliceObject != null)
            {

                SliceObject(hit.gameObject, cutMaterial);

                Destroy(hit.gameObject);

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
            piece.transform.position = obj.transform.position + Vector3.right * 0.1f;

            Rigidbody pieceRb = piece.AddComponent<Rigidbody>();
            pieceRb.isKinematic = false;
            pieceRb.mass = 1f;
            //pieceRb.useGravity = true;
            pieceRb.AddForce((piece.transform.position - transform.position).normalized *0.01f , ForceMode.Impulse);

            // 잘리고 남은 조각
            upper.transform.position = obj.transform.position;
            upper.transform.rotation = obj.transform.rotation;
            upper.transform.localScale = obj.transform.localScale;

            Rigidbody upperRb = upper.AddComponent<Rigidbody>();
            upperRb.useGravity = false;
            upperRb.isKinematic = true;

        }
        return slicedObj;
    }

    // 마우스에 카메라 달기
    public void MouseWithKnife()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.7f;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0.3f;

        transform.position = worldPos;
    }

    #region
    //private async UniTask SliceDelay(Collider collider)
    // {
    //     Collider knifeCollider = GetComponent<Collider>();
    //     knifeCollider.enabled = false;
    //     isSlicing = true;
    //     float sliceTimer = 0f;
    //     await UniTask.WaitUntil(() =>
    //     {
    //         sliceTimer += Time.deltaTime;
    //         if (collider == null) return true;
    //         if (sliceTimer > 0.1f) return true;
    //         return !knifeCollider.bounds.Intersects(collider.bounds);
    //     });
    //     if (knifeCollider != null)
    //         knifeCollider.enabled = true;

    //     knifeCollider.enabled = true;
    //     isSlicing = false;
    // }
    #endregion

    private void Update()
    {
        knife.MouseWithKnife();

        if (Input.GetMouseButtonDown(0))
        {
            TrySliceObject();
        }
    }
}
