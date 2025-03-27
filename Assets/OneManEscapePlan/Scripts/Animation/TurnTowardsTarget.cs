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
	/// Causes the GameObject to rotate towards the selected target at a given
	/// turn rate. 
	/// </summary>
	/// COMPLEXITY: Beginner
	public class TurnTowardsTarget : MonoBehaviour {

		[Tooltip("Speed of rotation in degrees per second")]
		[SerializeField] private float turnSpeed = 360;

		[DrawConnections(ColorName.PulsingYellow)]
		[SerializeField] private Transform target;

		virtual protected void FixedUpdate() {
			if (target != null) transform.RotateTowards(target.position, turnSpeed * Time.fixedDeltaTime);
		}

		public void SetTarget(GameObject target) {
			this.target = target.transform;
		}

		public Transform Target {
			get {
				return target;
			}

			set {
				target = value;
			}
		}

		public float TurnSpeed {
			get {
				return turnSpeed;
			}

			set {
				turnSpeed = value;
			}
		}
	}
}
