/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// Spacecrafts have a name and must have an attached health. Event listeners are used
	/// to return the spacecraft to its spawn pool when it is destroyed (if it was spawned
	/// by a pool), and to reset its health when it is returned to the spawn pool.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interfaces, UnityEvent listeners
	[DisallowMultipleComponent]
	[RequireComponent(typeof(HealthComponent))]
	public class Spacecraft : MonoBehaviour {

		[SerializeField] new protected string name;
		public string Name { get { return name; } }

		protected IHealth health;

		virtual protected void Awake() {
			health = GetComponent<IHealth>();
			health.DeathEvent.AddListener(onDeath);

			PooledObject po = GetComponent<PooledObject>();
			if (po != null) {
				po.ReturnToPoolEvent.AddListener(onReturnToPool);
			}
		}

		virtual protected void onReturnToPool(PooledObject po) {
			ResetSpacecraft();
		}

		virtual public void ResetSpacecraft() {
			health.HP = health.MaxHP;
		}

		virtual protected void onDeath(IHealth health) {
			gameObject.ReturnToPoolOrDestroy();
		}

		public override string ToString() {
			return name;
		}
	}
}