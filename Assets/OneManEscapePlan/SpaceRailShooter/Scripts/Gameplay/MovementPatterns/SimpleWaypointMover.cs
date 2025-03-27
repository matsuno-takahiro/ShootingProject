/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	/// <summary>
	/// Causes the AI waypoint to move at a fixed velocity
	/// </summary>
	/// COMPLEXITY: BEGINNER
	public class SimpleWaypointMover : MovementPattern {

		[Tooltip("Movement speed/direction, in units per second")]
		public Vector3 speed = new Vector3(0, 0, 1);
		public Space movementSpace = Space.Self;

		/// <summary>
		/// Move the GameObject each physics tick
		/// </summary>
		protected void FixedUpdate() {
			if (movementSpace == Space.Self) {
				Vector3 relativeWaypoint = transform.InverseTransformPoint(waypointComponent.waypoint);
				relativeWaypoint += speed * Time.fixedDeltaTime;
				waypointComponent.waypoint = transform.TransformPoint(relativeWaypoint);
			} else {
				waypointComponent.waypoint += speed * Time.fixedDeltaTime;
			}
			
		}

	}
}
