/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// Represents an AI with a waypoint. Used by movement patterns.
	/// </summary>
	public class WaypointComponent : MonoBehaviour {

		public Vector3 waypoint;

		protected void Awake() {
			waypoint = transform.position;
		}

	}
}
