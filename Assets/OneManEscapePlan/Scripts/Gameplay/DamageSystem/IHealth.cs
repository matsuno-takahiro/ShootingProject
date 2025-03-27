using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Gameplay.DamageSystem {

	[System.Serializable]
	public class HealthEvent : UnityEvent<IHealth> { }
	[System.Serializable]
	public class HealthDamageEvent : UnityEvent<IHealth, IDamage> { }

	/// <summary>
	/// Interface for objects wtih hit points
	/// </summary>
	public interface IHealth {
		HealthEvent HealthChangeEvent { get; }
		HealthDamageEvent DamageEvent { get; }
		HealthEvent DeathEvent { get; }

		int HP { get; set; }
		int MaxHP { get; set; }
		bool IsInvulnerable { get; set; }

		int ApplyDamage(IDamage damage);

		T GetComponent<T>();
	}
}