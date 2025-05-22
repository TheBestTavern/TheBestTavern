//#if UNITY_EDITOR
//using System.Diagnostics;
//using UnityEditor;
//using UnityEngine;

///// <summary>
///// 연습삼아 만들어봄
///// </summary>
//[CustomEditor(typeof(DayAndNightManager))]
//public class DayAndNightEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DayAndNightManager script = (DayAndNightManager)target;

//        script.nightMat = (Material)EditorGUILayout.ObjectField("Night Material", script.nightMat, typeof(Material), false);
//        script.process = EditorGUILayout.Slider("Process", script.process, 0f, 1f);
//        script.duration = EditorGUILayout.Slider("Duration", script.duration, 0f, 3f);
//        script.saturationCurve = (AnimationCurve)EditorGUILayout.CurveField("saturationCurve", script.saturationCurve);
//        script.lightnessCurve = (AnimationCurve)EditorGUILayout.CurveField("LightnessCurve", script.lightnessCurve);

//        if (Application.isEditor && script.nightMat != null)
//        {
//            script.nightMat.SetFloat("_Saturation", script.saturationCurve.Evaluate(script.process));
//            script.nightMat.SetFloat("_Lightness", script.lightnessCurve.Evaluate(script.process));
//        }

//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(target); // 씬에 *가 뜨게함. 즉 저장할 수 있는 상태로 만듦
//        }
//    }
//}
//#endif
