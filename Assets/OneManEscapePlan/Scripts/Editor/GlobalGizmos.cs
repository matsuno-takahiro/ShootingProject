/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

/// <summary>
/// A class for defining general-purpose Gizmo-drawing logic, particularly the 
/// "DrawConnections" logic used to draw colored lines between related objects 
/// in the Editor.
/// 
/// COMPLEXITY: Expert
/// CONCEPTS: Reflection, Attributes, Bitmasks, Gizmos
/// 
/// </summary>
public class GlobalGizmos {

#if DRAW_CONNECTIONS

	/// <summary>
	/// Draws Gizmos for any selected MonoBehaviour, if the conditions are met
	/// </summary>
	/// <param name="behaviour"></param>
	/// <param name="gizmoType"></param>
	[DrawGizmo(GizmoType.Active | GizmoType.InSelectionHierarchy)]
	static void DrawGizmos(MonoBehaviour behaviour, GizmoType gizmoType) {
		//get all public and private fields belonging to the given behaviour
		var fields = behaviour.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		foreach (var field in fields) {
			//check if the field has the DrawConnectionsAttribute
			var attributes = field.GetCustomAttributes(typeof(DrawConnectionsAttribute), true);
			if (attributes.Length > 0) {
				Color color = (attributes[0] as DrawConnectionsAttribute).GetColor();
				//draw connections between events and their listeners
				if (attributes.Length > 0 && field.FieldType.IsSubclassOf(typeof(UnityEventBase))) {
					UnityEventBase ueb = field.GetValue(behaviour) as UnityEventBase;
					behaviour.DrawLinesToListeners(ueb, color);
				} 
				//draw connections to GameObjects and behaviours
				else if (attributes.Length > 0 && (field.FieldType == typeof(GameObject) || field.FieldType.IsSubclassOf(typeof(GameObject)))) {
					GameObject target = field.GetValue(behaviour) as GameObject;
					if (target == null || target.IsPrefab()) return;
					Gizmos.color = color;
					Gizmos.DrawLine(behaviour.transform.position, target.transform.position);
				} else if (attributes.Length > 0 && field.FieldType.IsSubclassOf(typeof(MonoBehaviour))) {
					MonoBehaviour target = field.GetValue(behaviour) as MonoBehaviour;
					if (target == null || target.gameObject.IsPrefab()) return;
					Gizmos.color = color;
					Gizmos.DrawLine(behaviour.transform.position, target.transform.position);
				} else if (attributes.Length > 0 && (field.FieldType == typeof(Transform) || field.FieldType.IsSubclassOf(typeof(Transform)))) {
					Transform target = field.GetValue(behaviour) as Transform;
					if (target == null || target.gameObject.IsPrefab()) return;
					Gizmos.color = color;
					Gizmos.DrawLine(behaviour.transform.position, target.transform.position);
				}
			}
		}
	}
#endif
}