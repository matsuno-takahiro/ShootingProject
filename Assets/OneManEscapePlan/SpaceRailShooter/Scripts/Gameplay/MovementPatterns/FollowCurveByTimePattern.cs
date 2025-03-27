/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Animation.Curves;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	/// <summary>
	/// A movement pattern in which the waypoint follows a SplineCurve over a fixed period
	/// of time. Note that the waypoint's speed is not necessarily constant; it will move
	/// slower when travelling between two points of the curve that are closer together 
	/// and faster when travelling between two points that are further apart.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Spline Curves, Coroutines, Inheritance
	public class FollowCurveByTimePattern : FollowCurvePatternBase {

		[SerializeField] private float curveDuration = 1f;

		protected Vector3 lastPoint;

		override protected IEnumerator Fly() {
			yield return new WaitForEndOfFrame(); //give us time to position the curve properly

			float t = 0;
			lastPoint = curve.Evaluate(0);

			float speed = 1 / curveDuration;

			while (t <= 1) {
				//Evaluate a new point on the curve. Move the AI waypoint the same distance and direction
				//that we've moved along the curve
				Vector3 point = curve.Evaluate(t);
				Vector3 difference = point - lastPoint;
				lastPoint = point;
				waypointComponent.waypoint += difference;

				t += Time.deltaTime * speed;
				yield return new WaitForEndOfFrame();
			}

			if (loop) {
				StartCoroutine(Fly());
			}
		}
	}
}
