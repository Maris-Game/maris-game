using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enable))]
public class DropDownEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Enable enable = (Enable)target;

        GUIContent arrayLabelEnable = new GUIContent("Setting Sort");
        enable.arrayIndex = EditorGUILayout.Popup(arrayLabelEnable, enable.arrayIndex, enable.settingSort);

        
    }
}
