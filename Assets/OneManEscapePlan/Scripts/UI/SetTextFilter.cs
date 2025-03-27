/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.UI {

	/// <summary>
	/// Unity does not let us set the FilterMode of a font in the Inspector, so this
	/// workaround can be used to set the filter mode for the current session. The
	/// general use case for this will be if you have a pixel-style font and don't
	/// want Unity to apply smoothing; setting the filter mode to Point disables
	/// smoothing.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Text FilterModes
	[ExecuteInEditMode]
	[RequireComponent(typeof(Text))]
	public class SetTextFilter : MonoBehaviour {

		[SerializeField] private FilterMode filterMode = FilterMode.Point;

		// Use this for initialization
		void Awake() {
			Text text = GetComponent<Text>();
			text.mainTexture.filterMode = filterMode;
		}
	}
}
