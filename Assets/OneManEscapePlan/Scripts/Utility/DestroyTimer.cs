/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {

	/// <summary>
	/// Attach this script to a GameObject that you want to automatically be
	/// returned to its pool/destroyed a few seconds after it is enabled
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Coroutines
	public class DestroyTimer : MonoBehaviour {

		[Tooltip("The delay between when the GameObject is enabled and when it is destroyed, in seconds")]
		[SerializeField] protected float time = 2f;

		/// <summary>
		/// The delay between when the GameObject is enabled and when it is destroyed, in seconds
		/// </summary>
		public float Time { get { return time; } }

		virtual protected void OnEnable() {
			StartCoroutine(destroy());
		}

		virtual protected IEnumerator destroy() {
			yield return new WaitForSeconds(time);
			gameObject.ReturnToPoolOrDestroy();
		}
	}
}
