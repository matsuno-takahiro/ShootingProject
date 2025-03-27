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
	/// Defines a power-up that will give the player a shield when collected
	/// </summary>
	/// COMPLEXITY: Intermediate
	/// CONCEPTS: Inventory, Power-ups
	public class ShieldPowerUp : PowerUpBase {

		[SerializeField] protected GameObject shieldPrefab;

		virtual protected void Start() {
			Assert.IsNotNull(shieldPrefab);
		}

		/// <summary>
		/// Indicate this PowerUp was collected by the given collector and should activate.
		/// Spawns the shield from the prefab
		/// </summary>
		/// <param name="collector">The object that collected this PowerUp</param>
		public override void Activate(PowerUpCollector collector) {
			Instantiate(shieldPrefab, collector.transform, false);
			base.Activate(collector);
		}

	}
}
