using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.LevelScripting {

	/// <summary>
	/// When activated, changes the direction that the player's spacecraft is moving. This can be used to
	/// build levels with turns, or (when activated by triggers or other events) to create alternate paths
	/// through levels.
	/// 
	/// To use:
	///		1. Place this script and set up a trigger or event that activates it
	///		2. Add a GameObject representing the position and direction of the new axis that the player will 
	///		   be flying along 
	///		3. Select the GameObject from step 2 in the "newAxis" field
	///		
	/// When ChangePlayerHeading is activated, it will wait out the delay, and then smoothly animate the player 
	/// container from its current position/rotation to the position and rotation of the "newAxis" object.
	/// 
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: 3D transformations, Coroutines, Handles, Gizmos
	public class ChangePlayerHeading : MonoBehaviour {

		[Tooltip("The player container, which will be moved/rotated to change the player's heading")]
		[SerializeField] protected PlayerContainer playerContainer;
		[Tooltip("Delay before the player's ship starts changing heading, in seconds")]
		[SerializeField] protected float delay = .5f;
		[Tooltip("How long the player's ship will take to fly to the new axis, in seconds")]
		[SerializeField] protected float duration = 1f;
		[Tooltip("A GameObject positioned such that its center and forward vector represent the new axis the ship will move on")]
		[SerializeField] protected Transform newAxis;

		virtual protected void Start() {
			Assert.IsNotNull<PlayerContainer>(playerContainer, "You forgot to select the PlayerContainer");
			Assert.IsNotNull<Transform>(newAxis, "You forgot to select an object representing the new axis");
		}

		virtual public void Activate() {
			StartCoroutine(rotatePlayerContainer());
		}

		virtual protected IEnumerator rotatePlayerContainer() {
			if (delay > 0) yield return new WaitForSeconds(delay);

			float t = 0;
			Quaternion startRotation = playerContainer.transform.rotation;
			Vector3 startPosition = playerContainer.transform.position;
			while (t < duration) {
				t += Time.deltaTime;
				playerContainer.transform.rotation = Quaternion.Slerp(startRotation, newAxis.transform.rotation, t / duration);
				playerContainer.transform.position = Vector3.Lerp(startPosition, newAxis.transform.position, t / duration);
				yield return new WaitForEndOfFrame();
			}
			playerContainer.transform.position = newAxis.transform.position;
			playerContainer.transform.rotation = newAxis.transform.rotation;
		}

		virtual protected void OnDrawGizmos() {
			if (newAxis != null) {
				Gizmos.DrawLine(transform.position, newAxis.transform.position);
				Gizmos.color = Color.blue;
				Gizmos.DrawRay(newAxis.transform.position, newAxis.transform.forward * 5);
			}
		}

#if UNITY_EDITOR
		[CustomEditor(typeof(ChangePlayerHeading))]
		public class ChangePlayerHeadingInspector : UnityEditor.Editor {

			private void OnSceneGUI() {
				ChangePlayerHeading cph = (ChangePlayerHeading)target;
				Color previousColor = Handles.color;
				Handles.color = Color.blue;
				if (cph.newAxis != null) Handles.ArrowHandleCap(1, cph.newAxis.transform.position, cph.newAxis.transform.rotation, 5, EventType.Repaint);
				Handles.color = previousColor;
			}
		}
#endif
	}
}
