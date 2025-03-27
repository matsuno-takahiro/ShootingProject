using OneManEscapePlan.Scripts.Gameplay.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Gameplay.DamageSystem {

	/// <summary>
	/// The DamageTrigger applies damage to any object with an IHealth that collides with the trigger.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Triggers, Interfaces
	public class DamageTrigger : Trigger, IDamage {

		[Tooltip("The amount of damage that the trigger does to objects that collide with it")]
		[SerializeField] private int damage;
		[Tooltip("The type of damage applied by the trigger")]
		[SerializeField] private DamageType damageType;
		[Tooltip("If true, this trigger is destroyed or returned to its spawnpool immediately upon contact with a valid target")]
		[SerializeField] private bool destroyOnContact = true;

		/// <summary>
		/// This event is invoked when the trigger does damage
		/// </summary>
		[DrawConnections(ColorName.Red)]
		[SerializeField] protected DamageEvent damageEvent = new DamageEvent();
		public DamageEvent DamageEvent { get { return damageEvent; } }

		override protected void Awake() {
			base.Awake();
			enterEvent.AddListener(onTrigger);
			stayEvent.AddListener(onTrigger);
			exitEvent.AddListener(onTrigger);
		}

		virtual protected void onTrigger(GameObject go) {
			IHealth health = go.GetComponent<IHealth>();
			if (health != null && health.HP > 0) {
				health.ApplyDamage(this);
				damageEvent.Invoke(this, health);
			}
			if (destroyOnContact) {
				gameObject.ReturnToPoolOrDestroy();
			}
		}

		public int Damage {
			get {
				return damage;
			}
			set {
				damage = value;
			}
		}

		public DamageType Type {
			get {
				return damageType;
			}
			set {
				damageType = value;
			}
		}

		public bool DestroyOnContact {
			get {
				return destroyOnContact;
			}

			set {
				destroyOnContact = value;
			}
		}
	}
}
