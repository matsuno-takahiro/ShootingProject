/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {
	/// <summary>
	/// Sets the position of the scene view camera when the scene is loaded
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Scene View, 3D transformations
	[ExecuteInEditMode]
	public class SetSceneViewCameraPosition : MonoBehaviour {

		public Vector3 position;
		public Vector3 rotation;
		public float size = 115;

		void Awake() {
#if UNITY_EDITOR
			if (!Application.isPlaying) {
				Camera sceneCamera = SceneView.lastActiveSceneView.camera;
				sceneCamera.transform.position = position;
				SceneView.lastActiveSceneView.pivot = position;
				SceneView.lastActiveSceneView.rotation = Quaternion.Euler(rotation);
				SceneView.lastActiveSceneView.size = size;
				SceneView.lastActiveSceneView.Repaint();
			}
#endif
		}

	}
}
