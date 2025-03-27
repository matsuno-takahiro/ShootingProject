/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem {

	[System.Serializable]
	public class InventoryItemEvent : UnityEvent<InventoryItemAsset> { }

	/// <summary>
	/// Defines an asset representing a type of inventory item. For example, we could create an asset
	/// representing a missile, allowing us to place missiles in our inventory.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: ScriptableObjects
	/// <seealso cref="InventoryItemQuantity"/>
	/// <seealso cref="Inventory"/>
	[CreateAssetMenu(fileName ="InventoryItem", menuName = "Inventory Item", order = 1)]
	public class InventoryItemAsset : ScriptableObject {

		[Tooltip("The display name of this item type")]
		[SerializeField] new private string name = "";
		[Tooltip("A sprite used to represent this item in the HUD/menus")]
		[SerializeField] private Sprite icon = null;

		public string Name {
			get {
				return name;
			}
		}

		public Sprite Icon {
			get {
				return icon;
			}
		}
	}
}