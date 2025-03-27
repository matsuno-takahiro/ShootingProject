using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay.DamageSystem {

	/// <summary>
	/// HealthComponent adds hit points to an object. Events let us know when the
	/// object takes damage or is killed.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Interfaces, UnityEvents, 
	[DisallowMultipleComponent]
	public class HealthComponent : MonoBehaviour, IHealth {

		[Tooltip("Hit points")]
		[SerializeField] protected int hp = 100;
		[Tooltip("Maximum hit points")]
		[SerializeField] protected int maxHP = 100;
		[Tooltip("If true, HP can fall below zero")]
		[SerializeField] protected bool allowNegativeHealth = false;
		[Tooltip("Whether this object is currently invulnerable to damage")]
		[SerializeField] protected bool invulnerable = false;

		[Tooltip("Resistances to various types of damage")]
		[SerializeField] protected List<DamageResistance> resistances = new List<DamageResistance>();

		/// <summary>
		/// Invoked whenever the current hit points changes (either due to gaining or losing health)
		/// </summary>
		[DrawConnections(ColorName.Blue)]
		[SerializeField] protected HealthEvent healthChangedEvent = new HealthEvent();
		public HealthEvent HealthChangeEvent { get { return healthChangedEvent; } }

		/// <summary>
		/// Invoked whenever the current hit points decreases due to damage
		/// </summary>
		[DrawConnections(ColorName.Orange)]
		[SerializeField] protected HealthDamageEvent damageEvent = new HealthDamageEvent();
		public HealthDamageEvent DamageEvent { get { return damageEvent; } }

		/// <summary>
		/// Invoked when hit points first falls to or below 0
		/// </summary>
		[DrawConnections(ColorName.Red)]
		[SerializeField] protected HealthEvent deathEvent = new HealthEvent();
		public HealthEvent DeathEvent { get { return deathEvent; } }

		// Use this for initialization
		virtual public void Start() {
			clampHP();
		}

		/// <summary>
		/// Restrict the current HP value within the acceptable range
		/// </summary>
		virtual protected void clampHP() {
			if (hp > maxHP) hp = maxHP;
			if (!allowNegativeHealth && hp < 0) hp = 0;
		}

		/// <summary>
		/// Apply the given IDamage to this HealthComponent
		/// </summary>
		/// <param name="damage">An IDamage instance</param>
		/// <returns>New HP after the damage is applied</returns>
		virtual public int ApplyDamage(IDamage damage) {
			if (invulnerable || hp <= 0) return hp;

			float value = damage.Damage;
			foreach (DamageResistance resistance in resistances) {
				if (resistance.type == damage.Type) {
					value *= (1 - resistance.resistance);
				}
			}

			int previousHP = hp;

			//apply damage. Make sure we invoke death event last because that might cause this GameObject to be
			//immediately destroyed by another script
			setHP(hp - Mathf.FloorToInt(value)); //round down
			DamageEvent.Invoke(this, damage);
			//if we just died, invoke death event
			if (hp <= 0 && previousHP > 0) deathEvent.Invoke(this);

			return hp;
		}

		/// <summary>
		/// Current hit points
		/// </summary>
		virtual public int HP {
			get {
				return hp;
			}

			set {
				if (hp != value) {
					int previousHP = hp;
					setHP(value);
					//if we just died, invoke death event
					if (hp <= 0 && previousHP > 0) deathEvent.Invoke(this);
				}
			}
		}

		virtual protected void setHP(int value) {
			if (hp != value) {
				hp = value;
				clampHP();
				healthChangedEvent.Invoke(this);
			}
		}

		/// <summary>
		/// Maximum hit points
		/// </summary>
		virtual public int MaxHP {
			get {
				return maxHP;
			}

			set {
				Assert.IsTrue(value > 0, "MaxHP cannot be less than 1");

				if (maxHP != value) {
					maxHP = value;
					clampHP();
					healthChangedEvent.Invoke(this);
				}
			}
		}

		public bool IsInvulnerable {
			get {
				return invulnerable;
			}

			set {
				invulnerable = value;
			}
		}
	}

	[System.Serializable]
	public struct DamageResistance {
		public DamageType type;
		[Tooltip("Percentage of damage reduction, where 0 is no reduction and 1 is 100% reduction")]
		[Range(0, 1)]
		public float resistance;
	}
}