/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Editor {

	[CustomEditor(typeof(EventTimer))]
	public class EventTimerInspector : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			// Show default inspector property editor
			DrawDefaultInspector();

			if (Application.isPlaying) {
				EventTimer timer = (EventTimer)target;
				EditorGUILayout.LabelField("Active", timer.IsActive.ToString().ToString());
				EditorGUILayout.LabelField("Repetitions remaining", timer.RepetitionsRemaining.ToString());
			}
		}
	}

}