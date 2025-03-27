/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ValidationInspector : Editor {

	protected void DrawErrorInspector() {
		Color defaultBackgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = Color.red;
		DrawDefaultInspector();
		GUI.backgroundColor = defaultBackgroundColor;
	}

}

[CustomEditor(typeof(SimpleFactory))]
public class SimpleFactoryInspector : ValidationInspector {

	public override void OnInspectorGUI() {
		SimpleFactory factory = target as SimpleFactory;

		if (factory.Prefab == null) DrawErrorInspector();
		else DrawDefaultInspector();
	}
}