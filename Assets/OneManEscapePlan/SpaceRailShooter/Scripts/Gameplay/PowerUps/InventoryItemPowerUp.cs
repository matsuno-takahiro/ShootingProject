/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.PowerUps {
	
	/// <summary>
	/// Defines a power-up that will add something to the player's inventory when collected
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inventory, Power-ups
	public class InventoryItemPowerUp : PowerUpBase {

		[Tooltip("The type and quantity of item that will be added to the player's inventory")]
		[SerializeField] protected InventoryItemQuantity itemQuantity;

		virtual protected void Start() {
			Assert.IsNotNull<InventoryItemQuantity>(itemQuantity, "You forgot to select an item");
		}

		/// <summary>
		/// Indicate this PowerUp was collected by the given collector and should activate
		/// </summary>
		/// <param name="collector">The object that collected this PowerUp</param>
		public override void Activate(PowerUpCollector collector) {
			Inventory inventory = collector.GetComponent<Inventory>();
			Assert.IsNotNull<Inventory>(inventory, "InventoryItemPowerUp was collected by an object that has no inventory");

			//try to add the item quantity to the player's inventory
			if (inventory.AllowsItem(itemQuantity.Item)) {
				inventory.Add(itemQuantity);
			} else {
				Debug.Log("Inventory on " + inventory.gameObject.name + " cannot hold items of type " + itemQuantity.Item.Name);
			}

			base.Activate(collector);
		}

	}
}
