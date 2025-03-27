/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.Animation.UI {

	/// <summary>
	/// Blend the color of the attached Text through the selected colors
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interpolation
	[RequireComponent(typeof(Text))]
	public class PulseTextColor : MonoBehaviour {

		[SerializeField] protected List<Color> colors = new List<Color>();
		[Tooltip("The length of the transition from one color to the next, in seconds")]
		[Range(0, 10)]
		[SerializeField] protected float blendDuration = 1f;

		protected Color lastColor;
		protected Text text;
		protected int colorIndex = 1;
		float t = 0;

		void Start() {
			Assert.IsTrue(colors.Count >= 2, "You must specify at least two colors");
			text = GetComponent<Text>();
			lastColor = colors[0];
			colorIndex = 1;
		}

		void Update() {
			t += Time.deltaTime;
			//Smoothly blend from the current color to the next color
			text.color = Color.Lerp(lastColor, colors[colorIndex], t / blendDuration);
			//When the blend animation has finished, update the next color
			if (t >= blendDuration) {
				colorIndex++;
				if (colorIndex >= colors.Count) colorIndex = 0;
				t = 0;
				lastColor = text.color;
			}
		}
	}
}
