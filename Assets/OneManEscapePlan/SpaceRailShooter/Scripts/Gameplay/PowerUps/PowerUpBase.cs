/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.PowerUps {

	[System.Serializable]
	public class PowerUpEvent : UnityEvent<PowerUpBase> { }

	/// <summary>
	/// Abstract base class for powerups
	/// </summary>
	public abstract class PowerUpBase : MonoBehaviour {

		[SerializeField] protected PowerUpEvent collectedEvent = new PowerUpEvent();
		public PowerUpEvent CollectedEvent { get { return collectedEvent; } }

		/// <summary>
		/// Indicate this PowerUp was collected by the given collector and should activate
		/// </summary>
		/// <param name="collector">The object that collected this PowerUp</param>
		virtual public void Activate(PowerUpCollector collector) {
			collectedEvent.Invoke(this);
			gameObject.ReturnToPoolOrDestroy();
		}

	}
}
