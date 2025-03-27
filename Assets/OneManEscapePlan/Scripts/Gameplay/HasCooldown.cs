using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay {

	/// <summary>
	/// Base class for components that have a cooldown period after they are activated before 
	/// they can be activated again.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Time, Coroutines, Inheritance
	public class HasCooldown : MonoBehaviour {

		[Range(.02f, 10f)]
		[SerializeField] protected float cooldown = .2f;

		[Tooltip("If true, and we try to activate this action/ability/power while it is on cooldown, it will activate as soon as the cooldown is over")]
		[SerializeField] protected bool queueIfOnCooldown = true;

		protected float timeLastFired;

		private bool busy = false;

		/// <summary>
		/// Try to activate the ability
		/// </summary>
		/// <returns><c>True</c> if the ability activated/will activate, <c>false</c> if it is on cooldown and currently can't be activated</returns>
		protected bool tryToActivate() {
			if (busy) return false;

			float timeRemaining = TimeRemaining;
			if (timeRemaining <= 0) {
				activate();
			} else {
				if (queueIfOnCooldown)	activateWithDelay(timeRemaining);
				else return false;
			}
			return true;
		}

		/// <summary>
		/// Override this function in your child class
		/// </summary>
		virtual protected void activate() {
			if (Time.inFixedTimeStep) timeLastFired = Time.fixedTime;
			else timeLastFired = Time.time;
		}

		/// <summary>
		/// Activate this action/power/ability after the given delay
		/// </summary>
		/// <param name="delay"></param>
		/// <returns></returns>
		protected IEnumerator activateWithDelay(float delay) {
			busy = true;
			yield return new WaitForSeconds(delay);
			activate();
			busy = false;
		}

		/// <summary>
		/// The number of seconds after activating this component before it becomes activatable again
		/// </summary>
		public float Cooldown {
			get {
				return cooldown;
			}

			set {
				Assert.IsTrue(value >= 0, "Cooldown cannot be negative");
				cooldown = value;
			}
		}

		public float TimeRemaining {
			get {
				float time;
				if (Time.inFixedTimeStep) time = Time.fixedTime;
				else time = Time.time;

				float elapsed = time - timeLastFired;
				float remaining = Math.Max(0, cooldown - elapsed);
				return remaining;
			}
		}
	}
}
