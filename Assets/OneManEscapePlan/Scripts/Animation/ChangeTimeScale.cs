/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Animation {

	/// <summary>
	/// Interpolate the time scale between two values
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interpolation, Time scale
	public class ChangeTimeScale : MonoBehaviour {

		[SerializeField] private float startingTimeScale = 0;
		[SerializeField] private float finalTimeScale = 1;
		[SerializeField] private float duration = 1f;

		public void Activate() {
			Assert.IsFalse(startingTimeScale < 0, "Time scale cannot be negative");
			Assert.IsFalse(finalTimeScale < 0, "Time scale cannot be negative");
			Assert.IsFalse(duration < 0, "Duration cannot be negative");

			StartCoroutine(animateTimeScale());
		}

		virtual protected IEnumerator animateTimeScale() {
			float t = 0;
			while (t < duration) {
				t += Time.unscaledDeltaTime;
				Time.timeScale = Mathf.Lerp(startingTimeScale, finalTimeScale, t / duration);
				yield return new WaitForEndOfFrame();
			}
		}

		#region Properties
		public float StartingTimeScale {
			get {
				return startingTimeScale;
			}
			set {
				startingTimeScale = value;
			}
		}

		public float FinalTimeScale {
			get {
				return finalTimeScale;
			}
			set {
				finalTimeScale = value;
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
