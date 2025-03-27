/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Animation {

	/// <summary>
	/// Forces a GameObject to always look directly at the selected target
	/// </summary>
	/// COMPLEXITY: Beginner
	public class LookAtTarget : MonoBehaviour {
		[DrawConnections(ColorName.PulsingYellow)]
		public Transform target;

		private void Update() {
			if (target != null) {
				transform.LookAt(target);
			}
		}
	}
}
