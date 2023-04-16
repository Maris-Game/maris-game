using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableDropdown : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Interactable interactable = (Interactable)target;

        GUIContent arrayLabelInteractable = new GUIContent("Interaction Sort");
        interactable.arrayIndex = EditorGUILayout.Popup(arrayLabelInteractable, interactable.arrayIndex, interactable.interactSort);
    }
    
}
