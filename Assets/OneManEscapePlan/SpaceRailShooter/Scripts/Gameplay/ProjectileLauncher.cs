/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.InventorySystem;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// The ProjectileLauncher fires projectiles created by a Factory. The launcher fires
	/// "bursts" of one or more shots. We can configure the delay between shots in a burst,
	/// as well as the cooldown between bursts.
	/// </summary>
	/// COMPLEXITY: Intermediate
	/// CONCEPTS: Inheritance, Coroutines, Factories, Interfaces
	public class ProjectileLauncher : HasCooldown {
		[Tooltip("The factory that will create the projectile instances. If this is not set, the projectile launcher will not be able to fire!")]
		[SerializeField] private FactoryBase projectileFactory = null;

		[Tooltip("If selected, the launcher will consume this item type as ammo (optional)")]
		[SerializeField] private InventoryItemAsset ammoType = null;
		[Tooltip("The inventory we will consume ammo from, if we have selected an ammo type (optional)")]
		[SerializeField] private Inventory ammoSource = null;
		[Tooltip("How much ammo is consumed per shot, if we have selected an ammo type")]
		[Range(1, 100)]
		[SerializeField] private int ammoUsePerShot = 1;

		[Range(1, 5)]
		[SerializeField] protected int shotsPerBurst = 1;
		[SerializeField] protected float delayBetweenShotsInBurst = .08f;

		[SerializeField] protected AudioClip[] audioClips;
		[SerializeField] protected AudioSource audioSource;

		/// <summary>
		/// An object (such as the Player) that will receive points when this launcher's projectiles destroy targets that have a score value.
		/// The projectile must implement the IPointCollector interface in order for this to function.
		/// </summary>
		protected IReceivePoints pointsReceiver;

		virtual protected void Awake() {
			Assert.IsFalse(ammoType != null && ammoSource == null, "You selected an ammo type, but not an ammo source");
			Assert.IsFalse(ammoUsePerShot < 0, "ProjectileLaunchers cannot consume negative ammo");

			pointsReceiver = GetComponentInParent<IReceivePoints>();
		}

		virtual public void Fire() {
			if (ammoType != null) {
				if (ammoSource == null || ammoSource.GetQuantity(ammoType) < ammoUsePerShot) return;
			}
			tryToActivate();
		}

		protected override void activate() {
			base.activate();

			StartCoroutine(fireBurst());
		}

		protected IEnumerator fireBurst() {
			//if we haven't selected a factory, abort the attempt to fire
			if (projectileFactory == null) {
				Debug.LogWarning("[" + GetType().Name + "] Unable to fire; projectileFactory is null", gameObject);
				yield break;
			}

			for (int i = 0; i < shotsPerBurst; i++) {
				//if we use ammo and don't have enough ammo to fire another shot, stop firing
				if (ammoType != null) {
					Assert.IsNotNull<Inventory>(ammoSource, gameObject.name + " lost reference to ammo inventory; was it destroyed?");
					if (ammoSource.GetQuantity(ammoType) < ammoUsePerShot) yield break;
				}

				GameObject projectile = projectileFactory.SpawnObject(transform.position, transform.rotation);

				//if projectile is not null, perform next steps. otherwise, our factory is probably a spawnpool that is currently full
				if (projectile != null) {
					//play a random sound effect from our audio clip list, if possible
					if (audioClips != null && audioClips.Length > 0 && audioSource != null) {
						audioSource.PlayOneShot(audioClips.GetRandom());
					}
					//configure the projectile to collect points for this launcher's parent, if applicable
					if (pointsReceiver != null) {
						ICollectPoints pointCollector = projectile.GetComponent<ICollectPoints>();
						if (pointCollector != null) pointCollector.PointsReceiver = this.pointsReceiver;
					}

					//consume ammo if applicable
					if (ammoType != null) ammoSource.Remove(ammoType, ammoUsePerShot);
				}

				yield return new WaitForSeconds(delayBetweenShotsInBurst);
			}
		}

		protected FactoryBase ProjectileFactory {
			get {
				return projectileFactory;
			}

			set {
				projectileFactory = value;
			}
		}
	}

}
