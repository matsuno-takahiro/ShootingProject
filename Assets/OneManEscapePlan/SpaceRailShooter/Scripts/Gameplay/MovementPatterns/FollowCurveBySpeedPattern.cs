/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Animation.Curves;
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
	public class FollowCurveBySpeedPattern : FollowCurvePatternBase {

		[SerializeField] protected float speed = 5f;
		//TODO: closed loop (return to start point)

		protected Vector3 lastPoint;

		override protected IEnumerator Fly() {
			yield return new WaitForEndOfFrame(); //give us time to position the curve properly

			float t = 0;
			lastPoint = curve.Evaluate(0);
			waypointComponent.waypoint = waypointComponent.transform.position;

			//continue moving until we've reached the end of the curve
			while (t < 1) {
				float distance = 0;
				float targetDistanceThisFrame = speed * Time.deltaTime;
				Vector3 point = Vector3.zero;
				//move in very small increments until we've moved the target distance
				//this is imperfect but keeps our speed roughly linear.
				//TODO: there's probably a better way to do this
				while (distance < targetDistanceThisFrame && t < 1) {
					t += .001f;
					if (t > 1) t = 1;
					point = curve.Evaluate(t);
					distance = Vector3.Distance(point, lastPoint);
				}

				//move the waypoint equal to the distance traveled along the curve
				Vector3 difference = point - lastPoint;
				lastPoint = point;
				waypointComponent.waypoint += difference;

				yield return new WaitForEndOfFrame();
			}

			if (loop) {
				StartCoroutine(Fly());
			}
		}
	}
}
