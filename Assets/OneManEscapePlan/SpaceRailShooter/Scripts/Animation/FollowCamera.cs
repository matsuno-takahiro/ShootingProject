/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Animation {

	/// <summary>
	/// Attach this component to a camera that will follow behind the player
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Interpolation, 3D transformations
	[DisallowMultipleComponent]
	public class FollowCamera : MonoBehaviour {

		[Tooltip("How strongly to follow the player's movement")]
		[Range(0, 1)]
		[SerializeField] protected float ratio = .25f;

		[Tooltip("How quickly to tilt towards the player's movement")]
		[Range(0, .25f)]
		[SerializeField] protected float tiltRate = .15f;

		[Tooltip("Try to stay this distance behind the player")]
		[Range(0f, 10)]
		[SerializeField] protected float distance = 3.5f;

		[Tooltip("How quickly to move to the desired distance behind the player")]
		[Range(0, 1)]
		[SerializeField] protected float followRate = .19f;

		/// <summary>
		/// The target we are following
		/// </summary>
		public Transform target;

		/// <summary>
		/// Update the position and rotation of the camera each physics tick
		/// </summary>
		virtual protected void FixedUpdate() {
			if (target != null) {
				//Look towards our target
				Vector3 lookPosition;
				if (target.transform.parent == null) {
					lookPosition = target.position * ratio;
					lookPosition.z = target.position.z;
				} else {
					Vector3 temp = target.localPosition * ratio;
					temp.z = target.localPosition.z;
					lookPosition = target.parent.TransformPoint(temp);
				}

				transform.LookAt(lookPosition, transform.up);

				Vector3 rotation = transform.localRotation.eulerAngles;
				//Tilt along the z-axis towards the target's tilt value
				if (tiltRate > 0) {
					float targetAngle = MathUtils.constrainAngle(target.transform.localRotation.eulerAngles.z) * ratio;
					rotation.z = Mathf.LerpAngle(rotation.z, targetAngle, tiltRate);
				} else {
					rotation.z = 0;
				}
				transform.localRotation = Quaternion.Euler(rotation);

				//Move towards the target
				if (followRate > 0) {
					Vector3 position = transform.localPosition;
					position.z = Mathf.Lerp(position.z, transform.InverseTransformPoint(target.position).z - distance, followRate);
					transform.localPosition = position;
				}
			}
		}

		public float FollowRate {
			get {
				return followRate;
			}

			set {
				followRate = value;
			}
		}

		public float Distance {
			get {
				return distance;
			}

			set {
				distance = value;
			}
		}

		public float TiltRate {
			get {
				return tiltRate;
			}

			set {
				tiltRate = value;
			}
		}

		public float Ratio {
			get {
				return ratio;
			}

			set {
				ratio = value;
			}
		}
	}
}
