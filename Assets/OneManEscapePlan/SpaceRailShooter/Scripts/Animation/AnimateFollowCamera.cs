/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Animation {

	/// <summary>
	/// Animate the attached FollowCamera from the start position and follow rate to the
	/// end position and follow rate, over a specified duration. 
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Coroutines, Interpolation
	[RequireComponent(typeof(FollowCamera))]
	public class AnimateFollowCamera : MonoBehaviour {
		[SerializeField] protected float startingX;
		[SerializeField] protected float startingY;
		[SerializeField] protected float startingFollowRate;

		[SerializeField] protected float finalX = 0;
		[SerializeField] protected float finalY = .75f;
		[SerializeField] protected float finalFollowRate;

		[SerializeField] protected float duration = 2;

		protected FollowCamera followCamera;

		public void Activate() {
			followCamera = GetComponent<FollowCamera>();
			StartCoroutine(animate());
		}

		virtual protected IEnumerator animate() {
			float elapsed = 0;
			while (elapsed < duration) {
				elapsed += Time.unscaledDeltaTime;
				float t = elapsed / duration;
				Vector3 position = transform.position;
				position.x = Mathf.Lerp(startingX, finalX, t);
				position.y = Mathf.Lerp(startingY, finalY, t);
				transform.position = position;
				followCamera.FollowRate = Mathf.Lerp(startingFollowRate, finalFollowRate, t);
				yield return new WaitForEndOfFrame();
			}
			Destroy(this);
		}
	}
}
