/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

	/// <summary>
	/// The MainMenu is a screen where the player can start the game, or access the Settings or Credits.
	/// It is a panel, but is not spawned by the PanelManager, since it's always located at the root of
	/// the MainMenu scene.
	/// </summary>
	public class MainMenu : MenuPanel {

		[SerializeField] protected PanelManager panelManager;

		// Use this for initialization
		void Start() {
			Assert.IsNotNull<PanelManager>(panelManager, "You forgot to select the PanelManager instance");
			Application.targetFrameRate = 60;
		}

		/// <summary>
		/// Show the Settings panel
		/// </summary>
		virtual public void ShowSettingsPanel() {
			SettingsPanel settingsPanel = panelManager.ShowSettingsPanel();
			showChildPanel(settingsPanel);
		}

		/// <summary>
		/// Show the Credits panel
		/// </summary>
		virtual public void ShowCreditsPanel() {
			CreditsPanel creditsPanel = panelManager.ShowCreditsPanel();
			showChildPanel(creditsPanel);
		}

		virtual public void Quit() {
			Application.Quit();
		}
	}
}
