/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// Defines movement behavior for a player spaceship in an "on-rails" environment
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: 3D transformations, Interpolation
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerMovementOnRails : MonoBehaviour, IPlayerMovement {

		[SerializeField] //The SerializeField tag allows our private variable to be edited in the Unity Inspector
		[Tooltip("The speed that our ship moves, in units per second")]
		private float speed = 4f;

		[Tooltip("The maximum amount our ship can rotate on any axis, in degrees")]
		[SerializeField]
		[Range(0, 90)]
		private float maxTurnAngle = 25;

		[Tooltip("Affects the rate at which our ship turns (higher values means faster turning). Setting this value too high may make the game more difficult.")]
		[SerializeField]
		[Range(0, 1)]
		private float turnInterpolationRate = .1f;

		[Tooltip("The rate at which our ship returns to facing straight forward when no input is supplied.")]
		[SerializeField]
		[Range(0, 1)]
		private float recenterInterpolationRate = .1f;

		[Tooltip("Controls how far our ship can move away from the center of the screen")]
		[SerializeField]
		private Boundaries2D boundaries = new Boundaries2D(-5, 5, -3f, 3f);

		[SerializeField]
		private bool invertVerticalInput = true;

		protected float horizontalInput, verticalInput;

		protected Rigidbody body;

		/// <summary>
		/// Awake is called when the GameObject is first added to the scene (or when the scene loads)
		/// </summary>
		virtual protected void Awake() {
			body = GetComponent<Rigidbody>(); //Find the Rigidboy attached to this behaviour's GameObject
		}

		virtual protected void OnEnable() {
			//Rather than read the setting from the PlayerPrefs each frame (which is computationally expensive), we
			//store it to a local variable and use an event listener to detect changes to the setting
			invertVerticalInput = Preferences.InvertYAxis.Value;
			Preferences.InvertYAxis.ValueChangedEvent.AddListener(SetInvertVerticalInput);
		}

		virtual protected void OnDisable() {
			Preferences.InvertYAxis.ValueChangedEvent.RemoveListener(SetInvertVerticalInput);
		}

		/// <summary>
		/// Update is called once per frame. We update the velocity of our ship based on the current inputs.
		/// </summary>
		virtual protected void Update() {

			float verticalInput = this.verticalInput;
			if (invertVerticalInput) {
				verticalInput = -verticalInput;
			}

			if (transform.parent == null) body.velocity = new Vector3(horizontalInput, verticalInput, 0) * speed;
			else body.velocity = transform.parent.TransformVector(new Vector3(horizontalInput, verticalInput, 0) * speed);

			float rate = turnInterpolationRate;
			if (horizontalInput == 0 && verticalInput == 0) rate = recenterInterpolationRate;
			rate = rate * (Time.deltaTime / .05f);

			//rotate our ship based on the current inputs
			Vector3 rotation = transform.localRotation.eulerAngles;
			//the targetRotation is the desired orientation we want to rotate towards
			Vector3 targetRotation = new Vector3();
			targetRotation.x = -verticalInput * maxTurnAngle; //roll (counter-clockwise/clockwise)
			targetRotation.y = horizontalInput * maxTurnAngle; //yaw (left/right)
			targetRotation.z = -horizontalInput * maxTurnAngle; //pitch (up/down)

			//Interpolation smoothly transitions from one value to another value each frame.
			//The interpolation rate lets us adjust how quickly the ship turns. Keep in mind that this affects
			//gameplay - the faster the ship turns, the harder it is to aim at small targets!
			rotation.x = Mathf.Lerp(MathUtils.constrainAngle(rotation.x), targetRotation.x, rate);
			rotation.y = Mathf.Lerp(MathUtils.constrainAngle(rotation.y), targetRotation.y, rate);
			rotation.z = Mathf.Lerp(MathUtils.constrainAngle(rotation.z), targetRotation.z, rate);

			transform.localRotation = Quaternion.Euler(rotation);
		}

		/// <summary>
		/// FixedUpdate is called every time the physics engine updates
		/// </summary>
		virtual protected void FixedUpdate() {
			//Restrict the movement range of our player so they can't move too far from the center of the screen
			Vector3 position = boundaries.Clamp(transform.localPosition);
			transform.localPosition = position;		
		}

		public void SetInvertVerticalInput(bool value) {
			invertVerticalInput = value;
		}

		#region Properties
		virtual public float HorizontalInput {
			get {
				return horizontalInput;
			}

			set {
				horizontalInput = value;
			}
		}

		virtual public float VerticalInput {
			get {
				return verticalInput;
			}

			set {
				verticalInput = value;
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

		public float MaxTurnAngle {
			get {
				return maxTurnAngle;
			}

			set {
				if (value < 0) throw new System.ArgumentOutOfRangeException("value", value, "MaxTurnAngle cannot be negative");
				maxTurnAngle = value;
			}
		}

		/// <summary>
		/// [Tooltip("Affects the rate at which our ship turns (higher values means faster turning). Setting this value too high may make the game more difficult.")]
		/// </summary>
		public float TurnInterpolationRate {
			get {
				return turnInterpolationRate;
			}

			set {
				if (value < 0 || value > 1) throw new System.ArgumentOutOfRangeException("value", value, "TurnInterpolationRate must be in range 0 - 1");
				turnInterpolationRate = value;
			}
		}

		/// <summary>
		/// Controls how far our ship can move away from the center of the screen
		/// </summary>
		public Boundaries2D Boundaries {
			get {
				return boundaries;
			}

			set {
				boundaries = value;
			}
		}

		public bool InvertVerticalInput {
			get {
				return invertVerticalInput;
			}

			set {
				invertVerticalInput = value;
			}
		}
		#endregion
	}


	#region Boundaries2D
	/// <summary>
	/// Defines a 2D box with given edges. We can clamp Vectors within these bounds.
	/// </summary>
	[Serializable]
	public struct Boundaries2D {
		public float minimumX, maximumX, minimumY, maximumY;

		public Boundaries2D(float minimumX, float maximumX, float minimumY, float maximumY) {
			this.minimumX = minimumX;
			this.maximumX = maximumX;
			this.minimumY = minimumY;
			this.maximumY = maximumY;
		}

		/// <summary>
		/// Clamp the given Vector within the defined boundaries (prevent it from going
		/// outside of the boundaries) and return it.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public Vector3 Clamp(Vector3 value) {
			value.x = Mathf.Clamp(value.x, minimumX, maximumX);
			value.y = Mathf.Clamp(value.y, minimumY, maximumY);
			return value;
		}
	}
	#endregion

}
