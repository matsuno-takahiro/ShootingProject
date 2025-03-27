/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem {
	/// <summary>
	/// Interface for something that collects points
	/// </summary>
	public interface ICollectPoints {
		IReceivePoints PointsReceiver { get; set; }

		void CheckForPoints(GameObject target);
		void CheckForPoints(IDamage damage, IHealth health);
	}
}