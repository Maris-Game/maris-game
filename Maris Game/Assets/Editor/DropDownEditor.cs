using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enable))]
public class DropDownEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Enable script = (Enable)target;

        GUIContent arrayLabel = new GUIContent("Setting Sort");
        script.arrayIndex = EditorGUILayout.Popup(arrayLabel, script.arrayIndex, script.settingSort);
    }
}
