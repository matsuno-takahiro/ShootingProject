/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Animation {

	/// <summary>
	/// Smoothly transition the GameObject from startScale to endScale
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interpolation
	public class SimpleScaler : MonoBehaviour {

		[SerializeField] private Vector3 startScale = new Vector3(1, 1, 1);
		[SerializeField] private Vector3 endScale = new Vector3(1, 1, 1);
		[SerializeField] private float duration = 1f;

		[DrawConnections(ColorName.Blue)]
		[SerializeField] protected UnityEvent finishedScalingEvent = new UnityEvent();
		public UnityEvent FinishedScalingEvent { get { return finishedScalingEvent; } }

		public void Activate() {
			StartCoroutine(scale());
		}

		virtual protected IEnumerator scale() {
			float t = 0;
			while (t < duration) {
				yield return new WaitForFixedUpdate();
				t += Time.fixedDeltaTime;
				transform.localScale = Vector3.Lerp(startScale, endScale, t / duration);
			}
			finishedScalingEvent.Invoke();
		}

		#region Properties
		public Vector3 StartScale {
			get {
				return startScale;
			}

			set {
				startScale = value;
			}
		}

		public Vector3 EndScale {
			get {
				return endScale;
			}

			set {
				endScale = value;
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
		#endregion
	}
}
