/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.UI {

	/// <summary>
	/// Copies text from a target Text into the attached Text. Useful for font effects
	/// that require two layers of overlapping Text, so that we don't have to manually
	/// apply the same text to both layers.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: UI
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	public class CopyText : MonoBehaviour {

		[DrawConnections(ColorName.PulsingYellow)]
		[SerializeField] private Text textToCopy = null;

		private Text text;

		// Use this for initialization
		void Awake() {
			if (Application.isPlaying) {
				Assert.IsNotNull<Text>(textToCopy, "You forgot to select the text that will be copied");
			}
			text = GetComponent<Text>();
			Assert.IsFalse(textToCopy == text, "CopyText cannot copy itself");
			updateText();
		}

		private void updateText() {
			if (textToCopy != null) text.text = textToCopy.text;
		}

#if UNITY_EDITOR
		private void OnGUI() {
			updateText();
		}
#endif
	}
}
