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
	/// Moves the GameObject along a fixed vector at a constant speed
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: 3D movement, World vs Local space
	public class SimpleMover : MonoBehaviour {

		[Tooltip("Movement speed/direction, in units per second")]
		public Vector3 speed = new Vector3(0, 0, 1);
		[Tooltip("Controls whether the movement is relative to the world or relative to this object's local coordinate system")]
		public Space movementSpace = Space.Self;

		/// <summary>
		/// Move the GameObject each physics tick
		/// </summary>
		protected void FixedUpdate() {
			transform.Translate(speed * Time.fixedDeltaTime, movementSpace);
		}

	}
}
