/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {

	/// <summary>
	/// Static helper functions for working with Unity's Gizmos class
	/// </summary>
	/// COMPLEXITY: Advanced
	/// CONCEPTS: Gizmos, Colliders, Bounds
	public static class GizmoUtils {
		/// <summary>
		/// Draw shapes to represent the volume of the given Collider
		/// </summary>
		/// <param name="collider"></param>
		public static void DrawCollider(Collider collider) {
			if (collider == null) return;

			Transform transform = collider.transform;
			if (collider is BoxCollider) {
				Gizmos.DrawCube(transform.TransformPoint((collider as BoxCollider).center), collider.bounds.size);
			} else if (collider is SphereCollider) {
				Gizmos.DrawSphere(transform.position + collider.bounds.center, (collider as SphereCollider).radius * (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3f);
			} else if (collider is MeshCollider) {
				Gizmos.DrawMesh((collider as MeshCollider).sharedMesh, transform.position, transform.rotation, transform.lossyScale);
			} else if (collider is CapsuleCollider) {
				//TODO
			}
		}

		/// <summary>
		/// Draw shapes to represent the volume of the given Collider2D
		/// </summary>
		/// <param name="collider"></param>
		public static void DrawCollider2D(Collider2D collider) {
			Transform transform = collider.transform;

			if (collider is BoxCollider2D) {
				Gizmos.DrawCube(transform.TransformPoint((collider as BoxCollider2D).offset), collider.bounds.size);
			} else if (collider is CircleCollider2D) {
				Gizmos.DrawSphere(transform.position + collider.bounds.center, (collider as CircleCollider2D).radius * (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3f);
			}
		}
	}
}
