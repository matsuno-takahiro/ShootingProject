/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OneManEscapePlan.Scripts.Gameplay.Spawning;

namespace OneManEscapePlan.Scripts.Editor {

	[CustomEditor(typeof(SpawnPool)), CanEditMultipleObjects]
	public class SpawnPoolInspector : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			// Show default inspector property editor
			DrawDefaultInspector();

			if (Application.isPlaying) {
				SpawnPool pool = (SpawnPool)target;
				EditorGUILayout.LabelField("Pool size", pool.Size.ToString());
				EditorGUILayout.LabelField("Inactive", pool.NumInactive.ToString());
				EditorGUILayout.LabelField("Active", pool.NumActive.ToString());
			}
		}
	}
}
