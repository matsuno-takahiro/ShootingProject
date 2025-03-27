using UnityEngine;

namespace OneManEscapePlan.Scripts.Properties {
	/// <summary>
	/// A string Property that is synchronized with PlayerPrefs
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inheritance, PlayerPrefs
	public class PlayerPrefsStringProperty : PlayerPrefsProperty<string> {

		public PlayerPrefsStringProperty(string key, string defaultValue) : base(key, defaultValue) {
		}

		public override string Value {
			get {
				return PlayerPrefs.GetString(key, defaultValue);
			}

			set {
				PlayerPrefs.SetString(key, value);
				ValueChangedEvent.Invoke(value);
			}
		}
	}
}
