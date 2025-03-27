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
using UnityEngine.Animations;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Animation.Curves {

	public enum SplineCurveHandleType {
		PositionTool, Sphere
	}

	/// <summary>
	/// Uses continuous spline interpolation to create a curve through a set of points.
	/// </summary>
	/// COMPLEXITY: Advanced
	/// CONCEPTS: Gizmos, Handles, Custom Inspectors, Mathmatics
	public class SplineCurve : MonoBehaviour, I3DCurveFunction {

		//--------------------------------------------------------------------------------------------------------------------
		// Static
		//--------------------------------------------------------------------------------------------------------------------

		#region Static
		/// <summary>
		/// Evaluate <c>t</c> between two points
		/// </summary>
		/// <param name="p0">Start point</param>
		/// <param name="p1">End point</param>
		/// <param name="m0">Tangent point 1</param>
		/// <param name="m1">Tangent point 2</param>
		/// <param name="t">Ratio along the curve from <c>p0</c> to <c>p1</c></param>
		/// <returns></returns>
		public static Vector3 Evaluate(Vector3 p0, Vector3 p1, Vector3 m0, Vector3 m1, float t) {
			return (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0
					+ (t * t * t - 2.0f * t * t + t) * m0
					+ (-2.0f * t * t * t + 3.0f * t * t) * p1
					+ (t * t * t - t * t) * m1;
		}
		#endregion

		//--------------------------------------------------------------------------------------------------------------------
		// Instance
		//--------------------------------------------------------------------------------------------------------------------

		[SerializeField] private List<Vector3> points = new List<Vector3>();
		[SerializeField] private bool relativeToTransform = true;
		[Header("Editor")]
		[Range(3, 25)]
		[SerializeField] private int previewResolution = 10;
		[Range(.1f, 1f)]
		[SerializeField] private float handleSize = .35f;
		[SerializeField] private SplineCurveHandleType handleType = SplineCurveHandleType.Sphere;
		[SerializeField] private Color previewColor = Color.blue;

		/// <summary>
		/// Get the point at <c>t</c> along the curve
		/// </summary>
		/// <param name="t">Normalized position along the curve (range 0 - 1)</param>
		/// <returns></returns>
		public Vector3 Evaluate(float t) {
			Assert.IsTrue(points.Count >= 3, "Spline must contain at least 3 points to be valid");
			Assert.IsTrue(t >= 0f && t <= 1f, "Couldn't evaluate point on spline; t must be in range 0 - 1 inclusive");

			int startIndex = Mathf.FloorToInt((points.Count - 1) * t);
			if (startIndex >= points.Count - 1) {
				startIndex = points.Count - 2;
			}
			
			float u = t * (points.Count - 1) - startIndex; //ratio between startIndex and startIndex + 1
			Vector3 point = evaluate(startIndex, u);
			if (relativeToTransform) point = transform.TransformPoint(point);
			return point;
		}

		/// <summary>
		/// Evaluate <c>t</c> between points[index] and points[index + 1]
		/// </summary>
		/// <param name="index"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		private Vector3 evaluate(int index, float t) {
			Assert.IsTrue(index >= 0 && index < points.Count);

			Vector3 p0 = points[index];
			Vector3 p1 = points[index + 1];

			//automatically calculate tangent points
			Vector3 m0;
			if (index == 0) m0 = (points[index + 1] - points[index]) / 2;
			else m0 = (points[index + 1] - points[index - 1]) / 2;
			Vector3 m1;
			if (index < points.Count - 2) m1 = (points[index + 2] - points[index]) / 2;
			else m1 = (points[index + 1] - points[index]) / 2;

			return SplineCurve.Evaluate(p0, p1, m0, m1, t);
		}

		#region Editor
#if UNITY_EDITOR

		//Draw the curve in the Scene view
		private void OnDrawGizmos() {
			if (this.points.Count < 3) return;

			Gizmos.color = previewColor;
			for (int i = 0; i < points.Count - 1; i++) {
				float t = 0;
				Vector3 previous = points[i];
				if (relativeToTransform) previous = transform.TransformPoint(previous);
				for (int j = 0; j < previewResolution; j++) {
					t += 1f / (float)previewResolution;

					Vector3 next = evaluate(i, t);
					if (relativeToTransform) next = transform.TransformPoint(next);
					Gizmos.DrawLine(previous, next);
					previous = next;
				}
			}

			/*
			//For debugging; moves a point back and forth across the curve
			float u = Mathf.Abs(Mathf.Sin(Time.time));
			Vector3 point = Evaluate(u);
			Gizmos.DrawSphere(point, 1);
			point.y += 2;
			Handles.Label(point, u.ToString());
			*/
		}

		//If this curve is selected, use Handles to label its points in the Scene view
		private void OnDrawGizmosSelected() {
			for (int i = 0; i < points.Count; i++) {
				Vector3 point = points[i];
				if (relativeToTransform) {
					point = transform.TransformPoint(point);
				}
				Handles.Label(point + new Vector3(0, -.5f, 0), i.ToString());
			}
		}

		/// <summary>
		/// Our custom editor adds lots of functionality to make editing our curve easier
		/// </summary>
		[CustomEditor(typeof(SplineCurve)), CanEditMultipleObjects]
		class SplineCurveEditor : Editor {

			private int numPoints = 3;
			private Vector3 scale = Vector3.one;
			private Axis axis = Axis.X;

			private void OnSceneGUI() {
				SplineCurve curve = (SplineCurve)target;
				Handles.color = curve.previewColor;

				List<Vector3> points = new List<Vector3>(curve.points);

				//Draw handles which can be dragged around in the editor
				for (int i = 0; i < points.Count; i++) {
					//convert coordinate space if applicable
					if (curve.relativeToTransform) {
						points[i] = curve.transform.TransformPoint(points[i]);
					}
					//Create a drag handle that we can use to adjust this point in the Scene view
					Vector3 newPoint;
					if (curve.handleType == SplineCurveHandleType.PositionTool) newPoint = Handles.PositionHandle(points[i], Quaternion.identity);
					else { var fmh_176_56_638786722621639548 = Quaternion.identity; newPoint = Handles.FreeMoveHandle(points[i], curve.handleSize, new Vector3(), Handles.SphereHandleCap); }

					if (newPoint != points[i]) {
						Undo.RecordObject(curve, "Move curve control point");
						curve.points[i] = curve.transform.InverseTransformPoint(newPoint);
					}
				}
			}

			/// <summary>
			/// Add some utility functions to the inspector
			/// </summary>
			public override void OnInspectorGUI() {
				base.OnInspectorGUI();

				SplineCurve curve = (SplineCurve)target;

				EditorGUILayout.Space();
				GUIStyle style = new GUIStyle(EditorStyles.helpBox);
				GUIStyle bold = new GUIStyle(EditorStyles.boldLabel);

				//---- ADJUSTMENTS ----
				EditorGUILayout.LabelField("Adjustments", bold);

				//Scale
				EditorGUILayout.BeginVertical(style);
				scale = EditorGUILayout.Vector3Field("Scale", scale);
				if (GUILayout.Button("Apply Scale")) {
					Undo.RecordObject(curve, "Apply Scale");
					for (int i = 0; i < curve.points.Count; i++) {
						Vector3 point = curve.points[i];
						point.x *= scale.x;
						point.y *= scale.y;
						point.z *= scale.z;
						curve.points[i] = point;
					}
				}
				EditorGUILayout.EndVertical();

				//---- AUTO-GENERATION ----
				EditorGUILayout.LabelField("Auto-generation", bold);

				float oldLabelWidth = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = 40;
				//Quick spiral
				EditorGUILayout.BeginHorizontal(style);
				numPoints = EditorGUILayout.IntField("Points", numPoints);
				if (GUILayout.Button("Quick Spiral")) {
					Undo.RecordObject(curve, "Quick Spiral");
					curve.points.Clear();
					for (int i = 0; i < numPoints; i++) {
						curve.points.Add(new Vector3(Mathf.Cos(i), Mathf.Sin(i), i / 2.0f));
					}
				}
				EditorGUILayout.EndHorizontal();
				
				//Quick sine wave
				EditorGUILayout.BeginHorizontal(style);
				numPoints = EditorGUILayout.IntField("Points", numPoints, GUILayout.MaxWidth(70));
				GUILayout.Space(1);
				axis = (Axis)EditorGUILayout.EnumPopup(axis);
				if (GUILayout.Button("Quick Sine Wave")) {
					Undo.RecordObject(curve, "Quick Sin Wave");
					curve.points.Clear();
					for (int i = 0; i < numPoints; i++) {
						if (axis == Axis.X) curve.points.Add(new Vector3(Mathf.Sin(i), 0, i / 2.0f));
						else if (axis == Axis.Y) curve.points.Add(new Vector3(0, Mathf.Sin(i), i / 2.0f));
						else if (axis == Axis.Z) curve.points.Add(new Vector3(0, i / 2.0f, Mathf.Sin(i)));
					}
				}
				EditorGUILayout.EndHorizontal();

				EditorGUIUtility.labelWidth = oldLabelWidth;
			}
		}
#endif
		#endregion
	}
}

