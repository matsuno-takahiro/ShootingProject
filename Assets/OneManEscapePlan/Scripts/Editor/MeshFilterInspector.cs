/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshFilter))]
public class MeshFilterInspector : UnityEditor.Editor {

	public override void OnInspectorGUI() {
		// Show default inspector property editor
		DrawDefaultInspector();

		//MeshInfo meshInfo = (MeshInfo)target;
		//MeshFilter filter = meshInfo.GetComponent<MeshFilter>();
		MeshFilter filter = (MeshFilter)target;

		if (filter != null && filter.sharedMesh != null) EditorGUILayout.LabelField("Submeshes: ", filter.sharedMesh.subMeshCount.ToString());
	}
}