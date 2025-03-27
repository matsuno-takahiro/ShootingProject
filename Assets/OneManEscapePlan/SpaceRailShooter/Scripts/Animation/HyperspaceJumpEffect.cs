/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Animation {

	[System.Serializable]
	public enum HyperspaceJumpType {
		JumpIn, JumpOut
	}

	/// <summary>
	/// The HyperspaceJumpEffect is a script-driven animation which makes an object (presumably a spaceship)
	/// appear to make a hyperspace jump into or out of the area.
	/// 
	/// The animation works by stretching the object along its local z (forward) axis and sliding it forward.
	/// This simple trick effectively recreates the look of hyperspace / warp jumps seen in many sci-fi films. 
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Coroutines, Interpolation
	[RequireComponent(typeof(MeshRenderer))]
	public class HyperspaceJumpEffect : MonoBehaviour {

		[SerializeField] protected HyperspaceJumpType type = HyperspaceJumpType.JumpIn;
		[SerializeField] protected float delay = 1f;
		[Tooltip("The maximum amount this object will stretch along the local z-axis during the hyperspace jump animation")]
		[SerializeField] protected float stretchRatio = 10f;
		[Tooltip("Controls how much this object moves during the hyperspace jump. Recommended: 17")]
		[SerializeField] protected float moveRatio = 17;
		[SerializeField] protected float duration = .33f;
		[SerializeField] protected Material jumpMaterial;

		[DrawConnections(ColorName.Blue)]
		[SerializeField] protected UnityEvent jumpCompletedEvent = new UnityEvent();
		public UnityEvent JumpCompletedEvent { get { return jumpCompletedEvent; } }

		public HyperspaceJumpType Type {
			get {
				return type;
			}

			set {
				type = value;
			}
		}

		public void SetType(HyperspaceJumpType type) {
			this.type = type;
		}

		new protected MeshRenderer renderer;

		virtual protected void Awake() {
			renderer = GetComponent<MeshRenderer>();
			//If we are set to jump in, we should hide the GameObject right away, so that it isn't visible
			//until the jump-in animation starts
			if (type == HyperspaceJumpType.JumpIn) {
				gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// Activate the selected hyperspace animation (JumpIn or JumpOut)
		/// </summary>
		virtual public void Activate() {
			gameObject.SetActive(true);
			
			if (type == HyperspaceJumpType.JumpIn) {
				StartCoroutine(jumpIn());
			} else {
				StartCoroutine(jumpOut());
			}
		}

		virtual protected IEnumerator jumpIn() {
			//first we hide the renderer, so our ship isn't visible until the animation starts
			renderer.enabled = false;

			yield return new WaitForSeconds(delay);
			renderer.enabled = true;

			Vector3 startPosition = transform.position + (-transform.forward * stretchRatio * moveRatio);
			Vector3 targetPosition = transform.position;
			float originalZScale = transform.localScale.z;
			float maxZScale = originalZScale * stretchRatio;

			yield return StartCoroutine(performJump(startPosition, targetPosition, maxZScale, originalZScale));
		}

		virtual protected IEnumerator jumpOut() {
			renderer.enabled = true;
			yield return new WaitForSeconds(delay);

			Vector3 startPosition = transform.position;
			Vector3 targetPosition = transform.position + transform.forward * stretchRatio * moveRatio;
			float originalZScale = transform.localScale.z;
			float maxZScale = originalZScale * stretchRatio;

			yield return StartCoroutine(performJump(startPosition, targetPosition, originalZScale, maxZScale));

			renderer.enabled = false;

			//restore original position/scale
			transform.position = startPosition;
			Vector3 scale = transform.localScale;
			scale.z = originalZScale;
			transform.localScale = scale;
			
		}

		virtual protected IEnumerator performJump(Vector3 startPosition, Vector3 targetPosition, float startScale, float endScale) {
			Material originalMaterial = renderer.material;
			if (jumpMaterial != null) renderer.material = jumpMaterial;

			Vector3 localScale = transform.localScale;
			localScale.z = startScale;
			transform.localScale = localScale;
			transform.position = startPosition;

			float t = 0;
			while (t < duration) {
				yield return new WaitForFixedUpdate();
				t += Time.fixedDeltaTime;
				localScale.z = Mathf.Lerp(startScale, endScale, t / duration);
				transform.localScale = localScale;
				transform.position = Vector3.Lerp(startPosition, targetPosition, t / duration);
			}

			renderer.material = originalMaterial;

			jumpCompletedEvent.Invoke();
			//Destroy(this);
		}
	}
}
