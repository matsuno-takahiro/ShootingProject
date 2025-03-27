/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	/// <summary>
	/// Base class for movement patterns used by AIs
	/// </summary>
	public class MovementPattern : MonoBehaviour {

		[FormerlySerializedAs("ai")]
		[SerializeField] protected WaypointComponent waypointComponent;

		virtual protected void Awake() {
			Assert.IsNotNull<WaypointComponent>(waypointComponent, "You forgot to link the movement pattern to a WaypointComponent");
		}
	}
}
