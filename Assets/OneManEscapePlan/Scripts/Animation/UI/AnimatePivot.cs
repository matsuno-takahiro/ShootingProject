/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Animation.UI {

	/// <summary>
	/// Animate the Pivot of the attached RectTransform between the given starting and ending positions
	/// over a given period of time. This is useful if you want a simple panning animation and using
	/// Unity's animation system would be overkill.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: UI, Interpolation
	[RequireComponent(typeof(RectTransform))]
	public class AnimatePivot : MonoBehaviour {

		[SerializeField] protected Vector2 startPivot;
		[SerializeField] protected Vector2 endPivot;
		[SerializeField] protected float duration = 1;

		protected RectTransform rt;

		virtual public void Activate() {
			rt = GetComponent<RectTransform>();
			StartCoroutine(animate());
		}

		virtual protected IEnumerator animate() {
			float t = 0;
			while (t < duration) {
				t += Time.deltaTime;
				//interpolate between starting and ending positions
				rt.pivot = Vector2.Lerp(startPivot, endPivot, t / duration);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
