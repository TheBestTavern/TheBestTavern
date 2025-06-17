using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(ItemRecordManager))]
public class EndingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("엔딩 진입"))
        {
            var tracker = (ItemRecordManager)target;

            var prop = typeof(ItemRecordManager).GetProperty("itemRecords", BindingFlags.Instance | BindingFlags.Public);
            var dict = prop.GetValue(tracker) as Dictionary<int, ItemRecord>;

            if (dict != null)
            {
                foreach (var record in dict.Values)
                {
                    var hasDiscoveredProp = typeof(ItemRecord).GetProperty("HasDiscovered", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    
                        hasDiscoveredProp.SetValue(record, true);
                    
                }
            }
        }
    }
}
#endif
