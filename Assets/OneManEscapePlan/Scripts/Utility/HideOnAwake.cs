/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {
	/// <summary>
	/// Hide this GameObject as soon as it awakens
	/// </summary>
	/// COMPLEXITY: Beginner
	public class HideOnAwake : MonoBehaviour {
		private void Awake() {
			gameObject.SetActive(false);
			Destroy(this); //removes this script from the gameobject; does not destroy the gameobject
		}
	}
}