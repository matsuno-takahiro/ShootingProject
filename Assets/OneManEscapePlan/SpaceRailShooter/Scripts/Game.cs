/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts {

	/// <summary>
	/// Configures game settings such as framerate.
	/// </summary>
	/// COMPLEXITY: Beginner
	public class Game : MonoBehaviour {

		#region Static
		private static int totalScore = 0;
		/// <summary>
		/// The player's current total score across all levels they have completed
		/// </summary>
		public static int TotalScore {
			get {
				return totalScore;
			}
			set {
				if (value < 0) throw new ArgumentOutOfRangeException("value", "TotalScore cannot be negative");
				totalScore = value;
			}
		}
		#endregion

		[Tooltip("Application target framerate (frames per second)")]
		[Range(10, 120)]
		[SerializeField] protected int frameRate = 20;

		private bool isPaused = false;

		virtual protected void Awake() {
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = frameRate;
			Camera.main.allowDynamicResolution = false;
			//Note that this does not work in the Editor
			//Screen.SetResolution(screenResolution.x, screenResolution.y, true);
		}

		/// <summary>
		/// The target framerate
		/// </summary>
		public int FrameRate {
			get {
				return frameRate;
			}

			set {
				frameRate = value;
			}
		}


		public bool IsPaused {
			get {
				return isPaused;
			}

			set {
				isPaused = value;
				if (value) Time.timeScale = 0;
				else Time.timeScale = 1;
			}
		}

	}
}
