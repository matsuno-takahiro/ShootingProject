/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem {

	/// <summary>
	/// Used to store an inventory of items
	/// </summary>
	/// 
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Dictionaries
	/// 
	/// <seealso cref="InventoryItemAsset"/>
	/// <seealso cref="InventoryItemQuantity"/>
	public class Inventory : MonoBehaviour {

		[Tooltip("Defines what items our inventory can hold, and the maximum number of each type. Use a negative value to indicate unlimited capacity. Any changes made to this list while the game is running will have no effect.")]
		[SerializeField] private List<InventoryItemQuantity> storageCapacity = new List<InventoryItemQuantity>();

		[Tooltip("This event is invoked when the capacity of the inventory changes")]
		[DrawConnections(ColorName.Blue)]
		[SerializeField] private InventoryItemQuantityEvent capacityChangedEvent = new InventoryItemQuantityEvent();
		public InventoryItemQuantityEvent CapacityChangedEvent { get { return capacityChangedEvent; } }

		[Tooltip("This event is invoked when the quantity of items in the inventory changes")]
		[DrawConnections(ColorName.Cyan)]
		[SerializeField] private InventoryItemQuantityEvent quantityChangedEvent = new InventoryItemQuantityEvent();
		public InventoryItemQuantityEvent QuantityChangedEvent { get { return quantityChangedEvent; } }

		/// <summary>
		/// Maximum number of each item type that we can carry. Negative values indicate unlimited quantity
		/// </summary>
		protected Dictionary<InventoryItemAsset, int> capacity = new Dictionary<InventoryItemAsset, int>();
		/// <summary>
		/// Actual number of each item type currently in our inventory
		/// </summary>
		protected Dictionary<InventoryItemAsset, int> inventory = new Dictionary<InventoryItemAsset, int>();

		private bool isInitialized = false;

		// Use this for initialization
		void Awake() {
			if (!isInitialized) initialize();
		}

		virtual protected void initialize() {
			if (isInitialized) return;

			//parse capacities from the storageCapacity List into a Dictionary. A Dictionary may be overkill for small inventories, but is faster
			//than scanning a list for large inventories
			foreach (InventoryItemQuantity iiq in storageCapacity) {
				Assert.IsFalse(capacity.ContainsKey(iiq.Item), iiq.Item.Name + " appears more than once in the storage capacity list");
				capacity[iiq.Item] = iiq.Quantity;
			}
			//we no longer need the List once we've parsed it into the dictionary
#if !UNITY_EDITOR
			storageCapacity = null;
#endif

			isInitialized = true;
		}

		/// <summary>
		/// Check whether this inventory can carry items of the given type
		/// </summary>
		/// <param name="item"></param>
		/// <returns><c>true</c> if this inventory can carry the specified type of item, <c>false</c> otherwise</returns>
		public bool AllowsItem(InventoryItemAsset item) {
			if (!isInitialized) initialize();

			return capacity.ContainsKey(item);
		}

		/// <summary>
		/// Set the maximum number of the given type of item that we can carry. Use negative values to indicate that we
		/// can carry an unlimited number of this type of item
		/// </summary>
		/// <param name="item">The item type</param>
		/// <param name="quantity">The maximum number we can carry, or -1 for unlimited capacity</param>
		public void SetCapacity(InventoryItemAsset item, int quantity) {
			if (!isInitialized) initialize();

			capacity[item] = quantity;
			capacityChangedEvent.Invoke(new InventoryItemQuantity(item, quantity));

			if (quantity >= 0 && inventory.ContainsKey(item) && inventory[item] > quantity) {
				inventory[item] = quantity;
				quantityChangedEvent.Invoke(new InventoryItemQuantity(item, quantity));
			}
		}

		/// <summary>
		/// Set the maximum number of the given type of item that we can carry. Use negative values to indicate that we
		/// can carry an unlimited number of this type of item
		/// </summary>
		/// <param name="itemQuantity"></param>
		public void SetCapacity(InventoryItemQuantity itemQuantity) {
			SetCapacity(itemQuantity.Item, itemQuantity.Quantity);
		}

		/// <summary>
		/// Get the maximum number of the given type of item that we can carry. Negative values indicate unlimited
		/// capacity.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int GetCapacity(InventoryItemAsset item) {
			if (!isInitialized) initialize();

			int value = 0;
			capacity.TryGetValue(item, out value);
			return value;
		}

		/// <summary>
		/// Add the given quantity of the given item to the inventory, stopping at the maximum capacity for this item type
		/// if applicable. An error is thrown if the inventory does not accept this type of item.
		/// </summary>
		/// <param name="item">The type of item we are adding</param>
		/// <param name="quantity">The quantity of the item to add</param>
		/// <returns>The new quantity in the inventory</returns>
		public int Add(InventoryItemAsset item, int quantity) {
			if (!isInitialized) initialize();

			if (quantity < 0) throw new ArgumentOutOfRangeException("quantity", quantity, "Cannot add negative quantity");
			if (!capacity.ContainsKey(item)) throw new ArgumentException("This inventory does not accept items of type " + item);

			int max = GetCapacity(item);
			int current = GetQuantity(item);
			if (current == max) return current;

			int newValue = current + quantity;
			if (max >= 0) newValue = Math.Min(newValue, max);
			inventory[item] = newValue;

			if (newValue != current) quantityChangedEvent.Invoke(new InventoryItemQuantity(item, newValue));

			return newValue;
		}

		/// <summary>
		/// Add the given quantity of the given item to the inventory, stopping at the maximum capacity for this item type
		/// if applicable. An error is thrown if the inventory does not accept this type of item.
		/// </summary>
		/// <returns>The new quantity in the inventory</returns>
		public int Add(InventoryItemQuantity itemQuantity) {
			return Add(itemQuantity.Item, itemQuantity.Quantity);
		}

		/// <summary>
		/// Remove the given quantity of the given item from the inventory, or until the quantity in the inventory is 0. 
		/// An error is thrown if the inventory does not accept this type of item.
		/// </summary>
		/// <param name="item">The type of item we are removing</param>
		/// <param name="quantity">The quantity of the item to remove</param>
		/// <returns>The new quantity in the inventory</returns>
		public int Remove(InventoryItemAsset item, int quantity) {
			if (!isInitialized) initialize();

			if (quantity < 0) throw new ArgumentOutOfRangeException("quantity", quantity, "Cannot remove negative quantity");
			if (!capacity.ContainsKey(item)) throw new ArgumentException("This inventory does not accept items of type " + item);

			int current = GetQuantity(item);
			int newValue = Mathf.Max(current - quantity, 0);
			inventory[item] = newValue;

			if (newValue != current) quantityChangedEvent.Invoke(new InventoryItemQuantity(item, newValue));

			return newValue;
		}

		/// <summary>
		/// Remove the given quantity of the given item from the inventory, or until the quantity in the inventory is 0. 
		/// An error is thrown if the inventory does not accept this type of item.
		/// </summary>
		/// <returns>The new quantity in the inventory</returns>
		public int Remove(InventoryItemQuantity itemQuantity) {
			return Remove(itemQuantity.Item, itemQuantity.Quantity);
		}

		public int GetQuantity(InventoryItemAsset item) {
			if (!isInitialized) initialize();

			int value = 0;
			inventory.TryGetValue(item, out value);
			return value;
		}

		/// <summary>
		/// Remove all items from the inventory (capacity is not affected)
		/// </summary>
		public void Clear() {
			if (!isInitialized) initialize();

			inventory.Clear();
		}

		/// <summary>
		/// Get a collection representing the contents of this inventory. <b>Modifying the collection will not
		/// affect the inventory.</b> This function allocates a new List plus one new InventoryItemQuantity
		/// per item in the Inventory, so it should be used sparingly.
		/// </summary>
		/// <returns>An ICollection of InventoryItemQuantities that represents the contents of this Inventory.</returns>
		public ICollection<InventoryItemQuantity> GetContents() {
			List<InventoryItemQuantity> itemQuantities = new List<InventoryItemQuantity>();
			foreach (var item in inventory.Keys) {
				itemQuantities.Add(new InventoryItemQuantity(item, inventory[item]));
			}
			return itemQuantities;
		}

		public int Count {
			get {
				int count = 0;
				foreach (int c in inventory.Values) {
					count += c;
				}
				return count;
			}
		}
	}
}
