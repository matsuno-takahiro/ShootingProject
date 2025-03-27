/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Effects {

	/// <summary>
	/// Changes the first material of the attached GameObject when it takes damage. If we supply
	/// a flashing animated material, this will cause the GameObject to flash when it takes
	/// damage.
	/// </summary>
	/// COMPLEXITY: Intermediate
	/// CONCEPTS: Mesh renderers, Materials, Coroutines
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(IHealth))]
	public class FlashWhenDamaged : MonoBehaviour {

		[Tooltip("This material is applied for the selected duration after each time this object takes damage")]
		[SerializeField] protected Material damageMaterial;
		[Tooltip("How long the damage material is applied")]
		[SerializeField] protected float flashDuration = .32f;

		new protected MeshRenderer renderer;
		protected IHealth health;
		protected bool flashing = false;

		Material previousMaterial = null;

		protected void Start() {
			Assert.IsNotNull<Material>(damageMaterial);

			renderer = GetComponent<MeshRenderer>();
			health = GetComponent<IHealth>();
			health.DamageEvent.AddListener(onTakeDamage);

			PooledObject po = GetComponent<PooledObject>();
			if (po != null) po.ReturnToPoolEvent.AddListener(onReturnToPool);
		}

		/// <summary>
		/// When this GameObject is returned to a SpawnPool, if it is in the middle of
		/// the flashing animation, restore the original material and end the animation
		/// </summary>
		protected void onReturnToPool(PooledObject arg0) {
			if (previousMaterial != null && flashing) {
				var sharedMaterials = renderer.sharedMaterials;
				sharedMaterials[0] = previousMaterial;
				renderer.sharedMaterials = sharedMaterials;
				StopAllCoroutines();
			}
		}

		protected void onTakeDamage(IHealth arg0, IDamage arg1) {
			if (flashing) return;
			StartCoroutine(flash());
		}

		virtual protected IEnumerator flash() {
			flashing = true;

			var sharedMaterials = renderer.sharedMaterials;
			//save original material
			previousMaterial = sharedMaterials[0];
			
			//apply damage material
			sharedMaterials[0] = damageMaterial;
			renderer.sharedMaterials = sharedMaterials;

			yield return new WaitForSeconds(flashDuration);

			//restore original material
			sharedMaterials[0] = previousMaterial;
			renderer.sharedMaterials = sharedMaterials;

			flashing = false;
		}


	}
}
