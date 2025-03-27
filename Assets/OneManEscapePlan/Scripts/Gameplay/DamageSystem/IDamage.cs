using System;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Gameplay.DamageSystem {

	[Serializable]
	public class DamageEvent : UnityEvent<IDamage, IHealth> { }

	/// <summary>
	/// Interface for objects that can deal damage
	/// </summary>
	public interface IDamage {
		int Damage { get; }
		DamageType Type { get; }

		T GetComponent<T>();
	}
}
