using Codice.CM.SEIDInfo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapData))]
[CanEditMultipleObjects]
public class MapDataEditor : Editor
{
    private bool editing = false;
    private int tempIndex = 0;
    private string[] options = null;
    public override void OnInspectorGUI()
    {
        MapData data = target as MapData;

        base.OnInspectorGUI();

        if (editing)
        {
            EditorGUILayout.BeginHorizontal();

            tempIndex = EditorGUILayout.Popup(tempIndex, options);
            if (GUILayout.Button("Save"))
            {
                editing = false;

                data.mapIndex = tempIndex;
                data.mapName = options.ElementAt(tempIndex);
            }
            else if (GUILayout.Button("Cancel"))
            {
                editing = false;
            }

            EditorGUILayout.EndHorizontal();
        }
        else 
        {
            if (data.mapName == string.Empty)
            {
                data.mapName = data.GetMapsInBuildSettingsArray().First();
            }

            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Map: "+data.mapName);
            if (GUILayout.Button("Edit"))
            {
                editing = true;

                tempIndex = data.mapIndex;
                options = data.GetMapsInBuildSettingsArray();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
