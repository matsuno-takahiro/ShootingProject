/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.UI.Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

	/// <summary>
	/// Attach this component to a Panel where the user can adjust the controls
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Panels
	[DisallowMultipleComponent]
	public class ControlsPanel : MenuPanel {

		[SerializeField] private Toggle invertYAxisToggle = null;

		override protected void OnEnable() {
			base.OnEnable();

			Assert.IsNotNull<Toggle>(invertYAxisToggle, "You forgot to select the 'Invert Y-axis' toggle");
			
			invertYAxisToggle.isOn = Preferences.InvertYAxis.Value;
		}

		/// <summary>
		/// When this panel is hidden/destroyed, save the selected settings
		/// </summary>
		virtual protected void OnDisable() {
			Preferences.InvertYAxis.Value = invertYAxisToggle.isOn;
			Preferences.Save();
		}
	}
}
