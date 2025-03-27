/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Events;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Unity extension methods. These add new functions to existing classes.
/// See https://unity3d.com/learn/tutorials/topics/scripting/extension-methods
/// </summary>
/// COMPLEXITY: Advanced
/// CONCEPTS: Extension methods, Generics, Pooling, UnityEvents
public static class ExtensionMethods {

	public static bool IsPrefab(this GameObject go) {
		if (go == null) return false;
		return go.scene.rootCount == 0;
	}

	public static T GetRandom<T>(this List<T> list) {
		if (list.Count == 0) return default(T);
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	public static T GetRandom<T>(this T[] array) {
		if (array.Length == 0) return default(T);
		int index = UnityEngine.Random.Range(0, array.Length);
		return array[index];
	}

	/// <summary>
	/// Return this GameObject to the pool, if it is pooled. Otherwise, destroy it
	/// </summary>
	/// <returns><c>True</c> if this object is pooled, <c>false</c> if it is destroyed</returns>
	public static bool ReturnToPoolOrDestroy(this GameObject go) {
		PooledObject po = go.GetComponent<PooledObject>();
		if (po != null) {
			po.ReturnToPool();
			return true;
		}
		GameObject.Destroy(go);
		return false;
	}
	
	/// <summary>
	/// Rotate the given number of degrees towards the given position
	/// </summary>
	/// <param name="target">A position in world coordinates</param>
	/// <param name="degrees">We will rotate up to this many degrees towards the target</param>
	public static void RotateTowards(this Transform transform, Vector3 target, float degrees) {
		//if we are not extremely close to our target, rotate towards it
		if (Vector3.Distance(transform.position, target) > .15f) {
			//calculate the angle that would be looking directly at the target
			Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
			//rotate towards the target angle
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degrees);
		}
	}

	/// <summary>
	/// Illustrate event connections defined in the editor by drawing lines from this behaviour to every 
	/// GameObject that listens to the given event (which presumably belongs to this behaviour). Note that
	/// this only detects listeners that have been selected in the inspector
	/// </summary>
	/// <param name="event">A event presumably belonging to this behaviour</param>
	/// <param name="color">The color used when drawing the connections</param>
	public static void DrawLinesToListeners(this MonoBehaviour behaviour, UnityEventBase @event, Color color) {
		Gizmos.color = color;

		for (int i = 0; i < @event.GetPersistentEventCount(); i++) {
			UnityEngine.Object target = @event.GetPersistentTarget(i);
			if (target != null) {
				try {
					if (target is GameObject) {
						GameObject go = target as GameObject;
						if (!go.IsPrefab()) Gizmos.DrawLine(behaviour.transform.position, go.transform.position);
					} else if (target is MonoBehaviour) {
						MonoBehaviour mb = target as MonoBehaviour;
						if (!mb.gameObject.IsPrefab()) Gizmos.DrawLine(behaviour.transform.position, mb.gameObject.transform.position);
					}
				}
				catch {
					if (target is GameObject) {
						GameObject go = target as GameObject;
						if (!go.IsPrefab()) Debug.DrawLine(behaviour.transform.position, go.transform.position);
					} else if (target is MonoBehaviour) {
						MonoBehaviour mb = target as MonoBehaviour;
						if (!mb.gameObject.IsPrefab()) Debug.DrawLine(behaviour.transform.position, mb.gameObject.transform.position);
					}
				}
			}
		}
	}

	/// <summary>
	/// Illustrate event connections defined in the editor by drawing lines from this behaviour to every 
	/// GameObject that listens to the given event (which presumably belongs to this behaviour). Note that
	/// this only detects listeners that have been selected in the inspector
	/// </summary>
	/// <param name="event">A event presumably belonging to this behaviour</param>
	/// <param name="color">The color used when drawing the connections</param>
	public static void DrawLinesToListeners<T>(this MonoBehaviour behaviour, UnityEvent<T> @event, Color color) {
		Gizmos.color = color;
		
		for (int i = 0; i < @event.GetPersistentEventCount(); i++) {
			UnityEngine.Object target = @event.GetPersistentTarget(i);
			if (target != null) {
				try {
					if (target is GameObject) {
						Gizmos.DrawLine(behaviour.transform.position, (target as GameObject).transform.position);
					} else if (target is MonoBehaviour) {
						Gizmos.DrawLine(behaviour.transform.position, (target as MonoBehaviour).gameObject.transform.position);
					}
				} catch {
					if (target is GameObject) {
						Debug.DrawLine(behaviour.transform.position, (target as GameObject).transform.position);
					} else if (target is MonoBehaviour) {
						Debug.DrawLine(behaviour.transform.position, (target as MonoBehaviour).gameObject.transform.position);
					}
				}
			}
		}
	}
}
