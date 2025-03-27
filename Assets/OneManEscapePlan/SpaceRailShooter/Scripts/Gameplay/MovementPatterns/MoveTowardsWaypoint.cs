/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	/// <summary>
	/// Causes the GameObject to turn and move towards the AI waypoint
	/// </summary>
	/// COMPLEXITY: BEGINNER
	public class MoveTowardsWaypoint : MovementPattern {

		[SerializeField] private float speed = 5f;
		[Tooltip("Speed of rotation in degrees per second")]
		[SerializeField] private float turnSpeed = 360;

		private void FixedUpdate() {
			transform.RotateTowards(waypointComponent.waypoint, turnSpeed * Time.fixedDeltaTime);
			//move forward
			transform.Translate(new Vector3(0, 0, speed * Time.fixedDeltaTime), Space.Self);
		}

#if UNITY_EDITOR
		private void OnDrawGizmosSelected() {
			if (Application.isPlaying) {
				Gizmos.color = Color.blue;
				Gizmos.DrawSphere(waypointComponent.waypoint, .25f);
			}
		}
#endif
	}
}
