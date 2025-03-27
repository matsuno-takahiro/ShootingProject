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
	/// Rotate the GameObject at a constant speed
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: 3D rotation
	public class SimpleRotator : MonoBehaviour {

		public Vector3 speed = new Vector3(0, 180, 0);
		public Space movementSpace = Space.Self;

		/// <summary>
		/// Rotate the GameObject each physics tick
		/// </summary>
		protected void FixedUpdate() {
			transform.Rotate(speed * Time.fixedDeltaTime, movementSpace);
		}

	}
}
