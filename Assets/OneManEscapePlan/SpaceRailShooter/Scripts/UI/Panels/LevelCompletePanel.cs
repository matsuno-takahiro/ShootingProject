/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.SceneManagement;
using OneManEscapePlan.Scripts.UI.Panels;
using OneManEscapePlan.Scripts.Utility;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.HUD;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

	/// <summary>
	/// Attach this component to a Panel that appears when the player completes a level. This panel
	/// extends GameOverPanel with additional functionality for displaying the "level complete"
	/// message and proceeding to the next level.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Panels, Coroutines
	[DisallowMultipleComponent]
	public class LevelCompletePanel : GameOverPanel {

		[SerializeField] protected Text titleText;

		protected LinearLevelManager levelManager;

		override protected void Awake() {
			base.Awake();
			Assert.IsNotNull<Text>(titleText, "You forgot to select the title text");

			levelManager = GameObject.FindObjectOfType<LinearLevelManager>();
			Assert.IsNotNull<LinearLevelManager>(levelManager);
			int index = levelManager.CurrentLevelIndex;
			titleText.text = levelManager.LevelSequence.GetEntryAt(index).DisplayName + " Complete";
		}

		virtual public void Continue() {
			Game.TotalScore += LevelScore;
			levelManager.NextLevel();
		}
	}
}
