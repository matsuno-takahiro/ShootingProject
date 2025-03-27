/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using UnityEngine;
using UnityEngine.Animations;

namespace OneManEscapePlan.Scripts.Animation {

	/// <summary>
	/// Move the GameObject along the given axis using a sine wave as input.
	/// Causes the object to bob back and forth.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Sine waves
	public class SineWaveMover : MonoBehaviour {

		[SerializeField] private Axis axis = Axis.Y;
		[SerializeField] private float speed = .25f;
		[SerializeField] private float frequency = 1f;

		/// <summary>
		/// Add a bit of randomness so multiple objects with the same settings don't move in tandem
		/// </summary>
		protected float offset;

		virtual protected void Awake() {
			offset = Random.value;
		}

		virtual protected void FixedUpdate() {
			Vector3 position = transform.position;
			float change = Mathf.Sin(Time.fixedTime * frequency + offset) * speed * Time.fixedDeltaTime;
			if (axis == Axis.X) {
				position.x += change;
			} else if (axis == Axis.Y) {
				position.y += change;
			} else if (axis == Axis.Z) {
				position.z += change;
			}
			transform.position = position;
		}

		#region Properties
		public Axis Axis {
			get {
				return axis;
			}

			set {
				axis = value;
			}
		}

		public float Speed {
			get {
				return speed;
			}

			set {
				speed = value;
			}
		}

		public float Frequency {
			get {
				return frequency;
			}

			set {
				frequency = value;
			}
		}
		#endregion
	}
}
