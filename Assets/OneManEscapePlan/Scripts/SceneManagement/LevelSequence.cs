/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.SceneManagement {

	/// <summary>
	/// Defines an entry in a level sequence
	/// </summary>
	[System.Serializable]
	public struct LevelSequenceEntry {
		[SerializeField] private string fileName;
		[SerializeField] private string displayName;

		public LevelSequenceEntry(string fileName, string displayName) {
			this.fileName = fileName;
			this.displayName = displayName;
		}

		public string FileName {
			get {
				return fileName;
			}
		}

		public string DisplayName {
			get {
				return displayName;
			}
		}
	}

	/// <summary>
	/// This ScriptableObject defines an asset consistent of a sequence of levels, each with a display name. This can
	/// be used to manage levels in a game with linear progression.
	/// </summary>
	[CreateAssetMenu(fileName = "LevelSequence", menuName = "Level Sequence", order = 0)]
	public class LevelSequence : ScriptableObject, IEnumerable, IEnumerable<LevelSequenceEntry> {
		[SerializeField] private List<LevelSequenceEntry> sequence = new List<LevelSequenceEntry>();

		virtual public LevelSequenceEntry GetEntryAt(int index) {
			if (index < 0 || index > sequence.Count) throw new ArgumentOutOfRangeException("value", "Invalid level index");
			return sequence[index];
		}

		public IEnumerator GetEnumerator() {
			return sequence.GetEnumerator();
		}

		IEnumerator<LevelSequenceEntry> IEnumerable<LevelSequenceEntry>.GetEnumerator() {
			return sequence.GetEnumerator();
		}

		virtual public int IndexOf(string sceneName) {
			for (int i = 0; i < sequence.Count; i++) {
				if (sequence[i].FileName == sceneName) return i;
			}
			return -1;
		}

		public int Count {
			get {
				return sequence.Count;
			}
		}
	}
}
