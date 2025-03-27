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
	/// Attach this component to a Panel that appears when the player gets a game over
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Panels, Coroutines
	[DisallowMultipleComponent]
	public class GameOverPanel : MenuPanel {

		[SerializeField] protected Text levelScoreText;
		[SerializeField] protected Text totalScoreText;

		[SerializeField] protected string mainMenuSceneName = "MainMenu";

		private Game game;
		private int score;

		protected virtual void Awake() {
			Assert.IsNotNull<Text>(levelScoreText, "You forgot to select the level score text");
			Assert.IsNotNull<Text>(totalScoreText, "You forgot to select the total score text");
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

		/// <summary>
		/// The score that the player received for this level (not including points from previous levels)
		/// </summary>
		virtual public int LevelScore {
			set {
				score = value;
				levelScoreText.text = score.ToString("n0");
				totalScoreText.text = (Game.TotalScore + score).ToString();
			}
			get {
				return score;
			}
		}

		/// <summary>
		/// Restart the current level (the player will lose their score for the level)
		/// </summary>
		virtual public void Retry() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		/// <summary>
		/// Return to the main menu
		/// </summary>
		virtual public void Exit() {
			SceneManager.LoadSceneAsync(mainMenuSceneName);
		}
		
	}
}
