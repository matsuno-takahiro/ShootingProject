/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// Causes the attached GameObject to drop an object (spawned by a factory) when it dies
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interfaces, UnityEvent listeners
	[RequireComponent(typeof(IHealth))]
	public class DropOnDeath : MonoBehaviour {

		[Range(0, 1)]
		[SerializeField] protected float dropChance = .5f;
		[SerializeField] protected int quantity = 1;
		[SerializeField] protected FactoryBase factory;

		// Use this for initialization
		virtual protected void Start() {
			Assert.IsNotNull<FactoryBase>(factory, "You forgot to select a factory");
			IHealth health = GetComponent<IHealth>();
			health.DeathEvent.AddListener(onDeath);
		}

		private void onDeath(IHealth health) {
			if (Random.value <= dropChance) factory.SpawnObject(transform.position, transform.rotation);
		}
	}
}
