using UnityEngine;

namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// A bool Property that is synchronized with PlayerPrefs
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inheritance, PlayerPrefs
	public class PlayerPrefsBoolProperty : PlayerPrefsProperty<bool> {

		protected const int TRUE = 1;
		protected const int FALSE = 0;

		public PlayerPrefsBoolProperty(string key, bool defaultValue) : base(key, defaultValue) {
		}

		private int boolToInt(bool value) {
			if (value == true) return TRUE;
			else return FALSE;
		}

		public override bool Value {
			get {
				return PlayerPrefs.GetInt(key, boolToInt(defaultValue)) == TRUE;
			}

			set {
				PlayerPrefs.SetInt(key, boolToInt(value));
				ValueChangedEvent.Invoke(value);
			}
		}
	}
}
