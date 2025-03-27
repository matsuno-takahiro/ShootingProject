/// © 2018-2021 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// The transform parent for the player spacecraft. Moves forward each physics step
	/// as long as the player is alive.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Translation, interpolation
	public class PlayerContainer : MonoBehaviour {

		[SerializeField] private PlayerSpacecraft playerSpacecraft;
		[SerializeField] protected Vector3 speed = new Vector3(0, 0, 6);
		[Tooltip("Controls how quickly the spacecraft returns to it's starting local z-coordinate after being pushed by physics collisions.")]
		[Range(0f, 1f)]
		[SerializeField] protected float zRecenterRate = .5f;

		virtual protected void FixedUpdate() {
			if (playerSpacecraft != null) {
				//move the container (and thus the player) forward each physics step
				transform.Translate(speed * Time.deltaTime, Space.Self);

				//if player spacecraft has been moved forward or backward within the container
				//(e.g. due to physics), lerp the spacecraft back towards the center
				Vector3 localPosition = playerSpacecraft.transform.localPosition;
				if (localPosition.z != 0) {
					localPosition.z = Mathf.Lerp(localPosition.z, 0, zRecenterRate * Time.deltaTime);
					playerSpacecraft.transform.localPosition = localPosition;
				}
			}
		}

		public PlayerSpacecraft PlayerSpacecraft {
			get {
				return playerSpacecraft;
			}

			set {
				playerSpacecraft = value;
			}
		}
	}

}
