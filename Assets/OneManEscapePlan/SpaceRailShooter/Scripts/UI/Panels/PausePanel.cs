/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

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
	/// Attach this component to a Panel that appears when the player pauses the game.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Panels, Coroutines
	[DisallowMultipleComponent]
	public class PausePanel : MenuPanel {

		[SerializeField] protected string mainMenuSceneName = "MainMenu";

		private Game game;

		virtual protected void Awake() {
			game = GameObject.FindObjectOfType<Game>();
			Assert.IsNotNull<Game>(game);
		}

		protected override void OnEnable() {
			base.OnEnable();
			game.IsPaused = true;
		}

		override protected void OnDestroy() {
			game.IsPaused = false;
			base.OnDestroy();
		}

		virtual public void MainMenu() {
			SceneManager.LoadSceneAsync(mainMenuSceneName);
			Game.TotalScore = 0;
		}

		/// <summary>
		/// Show the Settings panel
		/// </summary>
		virtual public void ShowSettings() {
			PanelManager panelManager = GameObject.FindObjectOfType<PanelManager>();
			Assert.IsNotNull<PanelManager>(panelManager, "Couldn't find PanelManager");
			SettingsPanel sp = panelManager.ShowSettingsPanel();
			showChildPanel(sp);
		}

		virtual public void Quit() {
			Application.Quit();
		}
		
	}
}
