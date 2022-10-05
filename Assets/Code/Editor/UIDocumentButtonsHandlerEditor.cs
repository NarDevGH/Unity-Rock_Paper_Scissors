using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIDocumentButtonsHandler))]
public class UIDocumentButtonsHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIDocumentButtonsHandler data = target as UIDocumentButtonsHandler;

        base.OnInspectorGUI();

        if (GUILayout.Button("Refresh"))
        {
            data.SetButtonsEvents();
        }
    }
}
