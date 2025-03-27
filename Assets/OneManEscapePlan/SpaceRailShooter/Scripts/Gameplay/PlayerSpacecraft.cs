/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Events;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	[System.Serializable]
	public class PlayerSpacecraftEvent : UnityEvent<PlayerSpacecraft> { }

	/// <summary>
	/// Defines a Spacecraft that belongs to a Player
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inheritance, Interfaces
	[DisallowMultipleComponent]
	public class PlayerSpacecraft : Spacecraft, IReceivePoints {

		[SerializeField] private IntEvent gainPointsEvent = new IntEvent();
		public IntEvent GainPointsEvent { get { return gainPointsEvent; } }

		virtual public void AddPoints(int points) {
			gainPointsEvent.Invoke(points);
		}

	}
}