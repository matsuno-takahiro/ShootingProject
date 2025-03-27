/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {
	/// <summary>
	/// Attach this script to a GameObject if you want to be able to have it be destroyed by
	/// an event. Then you can point the event at the "Destroy" function of this script.
	/// </summary>
	/// COMPLEXITY: Beginner
	public class Destroyer : MonoBehaviour {

		[SerializeField] protected bool debugLogging = false;

		/// <summary>
		/// Return the given object to its spawn pool, or destroy it if it is not pooled
		/// </summary>
		/// <param name="gameObject"></param>
		public void Destroy(GameObject gameObject) {
#if UNITY_EDITOR
			if (debugLogging) Debug.Log("[Destroyer] Removing " + gameObject.name);
#endif
			gameObject.ReturnToPoolOrDestroy();
		}

		/// <summary>
		/// Return this to its spawn pool, or destroy it if it is not pooled
		/// </summary>
		public void Destroy() {
			this.Destroy(gameObject);
		}
	}
}