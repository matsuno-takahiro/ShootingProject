/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace OneManEscapePlan.Scripts.Events {

	/// <summary>
	/// The EventTimer class fires an event on a timer. This can be used in the inspector to
	/// trigger delayed or repeating actions. For example, we could connect the EventTimer to
	/// a spawner to cause a GameObject to be spawned at regular intervals.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Coroutines,Unity Events
	public class EventTimer : MonoBehaviour {

		[DrawConnections(ColorName.Blue)]
		[SerializeField] private UnityEvent timerTickEvent = new UnityEvent();
		public UnityEvent TimerTickEvent { get { return timerTickEvent; } }

		[Tooltip("Extra delay before the timer first starts, in seconds")]
		[SerializeField] private float predelay = 0f;

		[Tooltip("Duration between ticks, in seconds")]
		[FormerlySerializedAs("delay")]
		[SerializeField] private float duration = 1f;

		[Tooltip("Set to a negative value for infinite repetitions")]
		[SerializeField] private int repetitions = -1;

		[SerializeField] protected bool startOnEnable = false;

		protected bool isActive = false;
		protected int repetitionsRemaining;

		protected Coroutine timerRoutine;

		virtual protected void OnEnable() {
			if (startOnEnable) {
				StartTimer();
			}
		}

		/// <summary>
		/// Start the timer. If it is already running, we stop and restart the timer.
		/// </summary>
		public void StartTimer() {
			if (repetitions == 0) return;
			StopTimer();
			timerRoutine = StartCoroutine(timer());
		}

		public void StopTimer() {
			if (timerRoutine != null) StopCoroutine(timerRoutine);
			isActive = false;
		}

		virtual protected IEnumerator timer() {
			isActive = true;
			repetitionsRemaining = repetitions;
			if (predelay > 0) yield return new WaitForSeconds(predelay);

			while (true) {
				yield return new WaitForSeconds(duration);
				timerTickEvent.Invoke();

				if (repetitions > 0) {
					repetitionsRemaining--;
					if (repetitionsRemaining <= 0) break;
				}
			}
			isActive = false;
		}

		/// <summary>
		/// The number of times that the timer will repeat. Set to a negative value for infinite repetitions.
		/// </summary>
		public int Repetitions {
			get {
				return repetitions;
			}

			set {
				repetitions = value;
			}
		}

		public bool IsActive {
			get {
				return isActive;
			}
		}

		public int RepetitionsRemaining {
			get {
				return repetitionsRemaining;
			}
		}

		public float Duration {
			get {
				return duration;
			}

			set {
				duration = value;
			}
		}

		public float Predelay {
			get {
				return predelay;
			}

			set {
				predelay = value;
			}
		}
	}
}
