/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem {

	[System.Serializable]
	public class InventoryItemQuantityEvent : UnityEvent<InventoryItemQuantity> { }

	/// <summary>
	/// Represents a quantity of a particular InventoryItemAsset. We allow negative quantities for special cases
	/// (for example, when defining an inventory's capacity, a negative quantity indicates that the capacity is
	/// unlimited).
	/// </summary>
	/// <seealso cref="Inventory"/>
	/// <seealso cref="InventoryItemAsset"/>
	/// COMPLEXITY: Beginner
	/// CONCEPTS:
	[Serializable]
	public class InventoryItemQuantity {
		[Tooltip("The type of item")]
		[SerializeField] protected InventoryItemAsset item;
		[Tooltip("The quantity")]
		[SerializeField] protected int quantity;

		public InventoryItemQuantity(InventoryItemAsset item, int quantity) {
			if (item == null) throw new ArgumentNullException("item", "Item cannot be null");
			this.item = item;
			this.quantity = quantity;
		}

		public InventoryItemAsset Item {
			get {
				return item;
			}
		}

		virtual public int Quantity {
			get {
				return quantity;
			}

			set {
				quantity = value;
			}
		}

		public InventoryItemQuantity Clone() {
			return new InventoryItemQuantity(item, quantity);
		}
	}
}
