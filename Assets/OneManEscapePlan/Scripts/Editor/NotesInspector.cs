/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Notes))]
public class NotesInspector : UnityEditor.Editor {

	public override void OnInspectorGUI() {
		Notes notes = (Notes)target;

		var style = new GUIStyle(EditorStyles.textField);
		style.wordWrap = true;
		Undo.RecordObject(notes, "Notes");
		notes.notes = EditorGUILayout.TextArea(notes.notes, style);
	}
}