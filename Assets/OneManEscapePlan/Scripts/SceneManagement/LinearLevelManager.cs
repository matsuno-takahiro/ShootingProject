/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace OneManEscapePlan.Scripts.SceneManagement {

	/// <summary>
	/// Use this script for navigating between levels in a linear sequence (where one level follows the next, with no branching). 
	/// We are defining "level" as a gameplay scene; all levels are scenes, but not all scenes are levels (for example, the main 
	/// menu scene is not a level).
	/// </summary>
	public class LinearLevelManager : MonoBehaviour, ILevelManager {

		[SerializeField] private LevelSequence levelSequence;

		virtual protected void Awake() {
			Assert.IsNotNull<LevelSequence>(levelSequence, "You forgot to select a LevelSequence");
		}

		virtual public void StartFirstLevel(bool async = true) {
			GoToLevel(0, async);
		}

		/// <summary>
		/// Go to the level with the given index
		/// </summary>
		/// <param name="index">The index of the level to load (this is not necessarily the same as the scene index</param>
		/// <param name="async">Whether to use asynchronous loading</param>
		virtual public void GoToLevel(int index, bool async = true) {
			if (index < 0 || index > levelSequence.Count) throw new ArgumentOutOfRangeException("value", "Invalid level index");
			var entry = levelSequence.GetEntryAt(index);

			if (SceneManager.GetActiveScene().name != entry.FileName) {
				if (async) SceneManager.LoadSceneAsync(entry.FileName);
				else SceneManager.LoadScene(entry.FileName);
			}
		}

		/// <summary>
		/// Go to the next level, if one exists
		/// </summary>
		/// <param name="async">Whether to use asynchronous loading</param>
		/// <returns><c>True</c> if we are proceeding to the next level, <c>false</c> if we are on the last level</returns>
		virtual public bool NextLevel(bool async = true) {
			int currentIndex = CurrentLevelIndex;
			if (currentIndex < levelSequence.Count - 1) {
				GoToLevel(currentIndex + 1, async);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Go to the previous level, if one exists
		/// </summary>
		/// <param name="async">Whether to use asynchronous loading</param>
		/// <returns><c>True</c> if we are returning to the previous level, <c>false</c> if we are on the first level or some non-level scene</returns>
		virtual public bool PreviousLevel(bool async = true) {
			int currentIndex = CurrentLevelIndex;
			if (currentIndex > 0) {
				GoToLevel(currentIndex - 1, async);
				return true;
			}
			return false;
		}

		public LevelSequence LevelSequence {
			get {
				return levelSequence;
			}

			set {
				if (value == null) throw new ArgumentNullException("value");
				levelSequence = value;
			}
		}

		virtual public int CurrentLevelIndex {
			get {
				return levelSequence.IndexOf(SceneManager.GetActiveScene().name);
			}
		}
	}
}
