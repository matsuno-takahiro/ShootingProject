/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.PowerUps {

	[System.Serializable]
	public class PowerUpCollectorEvent : UnityEvent<PowerUpCollector, PowerUpBase> { }

	/// <summary>
	/// Attach this component to GameObjects that can collect PowerUps (such as the player's spacecraft).
	/// Powerups will automatically be collected when this GameObject collides with them. An event is
	/// invoked each time that a powerup is collected.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Collision, UnityEvents
	public class PowerUpCollector : MonoBehaviour {
		[Tooltip("This event is invoked each time that a Powerup is collected")]
		[SerializeField] private PowerUpCollectorEvent collectedPowerUpEvent = new PowerUpCollectorEvent();
		public PowerUpCollectorEvent CollectedPowerUpEvent { get { return collectedPowerUpEvent; } }

		private void OnTriggerEnter(Collider other) {
			OnCollision(other.gameObject);
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			OnCollision(collision.gameObject);
		}

		/// <summary>
		/// When we collide with a GameObject, check if it is a powerup, and collect it if so
		/// </summary>
		/// <param name="other"></param>
		private void OnCollision(GameObject other) {
			PowerUpBase powerUp = other.GetComponent<PowerUpBase>();
			if (powerUp != null) {
				powerUp.Activate(this); //Tell the powerup that this collector has collected it
				collectedPowerUpEvent.Invoke(this, powerUp);
			}
		}
	}
}
