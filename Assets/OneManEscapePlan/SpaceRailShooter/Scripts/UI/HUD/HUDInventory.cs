/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.HUD {

	/// <summary>
	/// Represents the contents of an inventory in the HUD by displaying icons for each item 
	/// in the inventory. For example, if the player has three missiles, we display three 
	/// missile icons.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Factories, UI
	[RequireComponent(typeof(LayoutGroup))]
	public class HUDInventory : MonoBehaviour {

		[SerializeField] protected FactoryBase inventoryIconFactory;
		[SerializeField] protected Inventory inventory;

		protected List<Image> icons = new List<Image>();

		// Use this for initialization
		virtual protected void Awake() {
			Assert.IsNotNull<FactoryBase>(inventoryIconFactory);
			this.Inventory = inventory;
		}

		/// <summary>
		/// The Inventory that is displayed
		/// </summary>
		public Inventory Inventory {
			get {
				return inventory;
			}

			set {
				removeInventoryEvent();
				inventory = value;
				if (inventory != null) {
					inventory.QuantityChangedEvent.AddListener(onInventoryChanged);
				}
			}
		}
		
		/// <summary>
		/// When the contents of the Inventory change, update the displayed icons
		/// </summary>
		/// <param name="itemQuantity"></param>
		virtual protected void onInventoryChanged(InventoryItemQuantity itemQuantity) {
			int newItemCount = inventory.Count;
			//if we have too many icons, remove the extras
			while (icons.Count > newItemCount) {
				icons[0].gameObject.ReturnToPoolOrDestroy();
				icons.RemoveAt(0);
			}
			//if we don't have enough icons, add more
			while (icons.Count < newItemCount) {
				GameObject go = inventoryIconFactory.SpawnObject(transform);
				Image image = go.GetComponent<Image>();
				Assert.IsNotNull<Image>(image, "Inventory icon prefab must contain a SpriteRenderer");
				icons.Add(image);
			}

			//update the sprite displayed on each icon
			ICollection<InventoryItemQuantity> items = inventory.GetContents();
			int i = 0;
			foreach (InventoryItemQuantity iiq in items) {
				Sprite sprite = iiq.Item.Icon;
				for (int j = 0; j < iiq.Quantity; j++) {
					icons[i].sprite = sprite;
					i++;
				}
			}
		}

		virtual protected void removeInventoryEvent() {
			if (inventory != null) {
				inventory.QuantityChangedEvent.RemoveListener(onInventoryChanged);
			}
		}

		virtual protected void OnDestroy() {
			removeInventoryEvent();
		}
	}
}
