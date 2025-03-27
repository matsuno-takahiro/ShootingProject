/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Animation.Curves;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	/// <summary>
	/// A movement pattern in which the waypoint follows a SplineCurve at a linear speed
	/// (the distance that the waypoint moves each frame is not affected by the spacing
	/// of the curve points). Contrast this with <seealso cref="FollowCurveByTimePattern"/>
	/// </summary>
	/// COMPLEXITY: (Moderate + Advanced) / 2f
	/// CONCEPTS: Spline Curves, Coroutines, Inheritance
	abstract public class FollowCurvePatternBase : MovementPattern {

		[SerializeField] protected bool loop = true;
		//TODO: closed loop (return to start point)

		[SerializeField] protected SplineCurve curve;

		/// <summary>
		/// If true, the curve will be destroyed when this GameObject is destroyed
		/// </summary>
		protected bool destroyCurve = false;

		override protected void Awake() {
			base.Awake();
			Assert.IsNotNull<SplineCurve>(curve);

			//if the curve is a child of this GameObject, unparent the curve, so that it doesn't move when this GameObject moves
			if (curve.transform.IsChildOf(transform)) {
				destroyCurve = true;
				curve.transform.SetParent(null, false);
				curve.transform.position = new Vector3();
			}
		}

		/// <summary>
		/// When this GameObject is enabled, start flying
		/// NOTE: OnEnable happens too quickly after Awake and doesn't give the curve enough time to be repositioned,
		/// so we must wait one frame before we start following it (using 'yield return new WaitForEndOfFrame()' in fly() routine of child classes)
		/// </summary>
		virtual protected void OnEnable() {
			StartCoroutine(Fly());
		}

		abstract protected IEnumerator Fly();

		virtual protected void OnDestroy() {
			if (destroyCurve && curve != null) {
				Destroy(curve.gameObject);
			}
		}

		public bool Loop {
			get {
				return loop;
			}

			set {
				loop = value;
			}
		}


		#region Editor
#if UNITY_EDITOR
		private void OnDrawGizmosSelected() {
			if (Application.isPlaying && waypointComponent != null) {
				Gizmos.color = Color.magenta;
				Gizmos.DrawSphere(waypointComponent.waypoint, .5f);
			}
		}
#endif
		#endregion
	}
}
