/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.PowerUps {
	
	/// <summary>
	/// Defines a power-up that will give the player health when collected
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inventory, Power-ups
	public class HealthPowerUp : PowerUpBase {

		[SerializeField] protected int healthValue;

		/// <summary>
		/// Indicate this PowerUp was collected by the given collector and should activate.
		/// Gives health to an IHealth attached to the collector
		/// </summary>
		/// <param name="collector">The object that collected this PowerUp</param>
		public override void Activate(PowerUpCollector collector) {
			IHealth health = collector.GetComponent<IHealth>();
			if (health != null) {
				health.HP += healthValue;
				base.Activate(collector);
			}
		}

	}
}
