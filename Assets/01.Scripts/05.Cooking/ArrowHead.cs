//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ArrowHead : MonoBehaviour
//{
//   void Start()
//    {
//        Mesh mesh = new Mesh();
//        Vector3[] vertices = new Vector3[3]
//        {
//            Vector3.zero,
//            new Vector3 (-0.2f, -0.4f, 0f),
//            new Vector3(0.2f, -0.4f, 0f)

//        };

//        int[] triangels = new int[3] { 0, 1, 2 };

//        mesh.vertices = vertices;
//        mesh.triangles = triangels;
//        mesh.RecalculateNormals();

//        GetComponent<MeshFilter>().mesh = mesh;
//    }
//}
