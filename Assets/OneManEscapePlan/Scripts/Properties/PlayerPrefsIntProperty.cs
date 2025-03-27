using UnityEngine;

namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// An int Property that is synchronized with PlayerPrefs
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inheritance, PlayerPrefs
	public class PlayerPrefsIntProperty : PlayerPrefsProperty<int> {

		public PlayerPrefsIntProperty(string key, int defaultValue) : base(key, defaultValue) {
		}

		public override int Value {
			get {
				return PlayerPrefs.GetInt(key, defaultValue);
			}

			set {
				PlayerPrefs.SetInt(key, value);
				ValueChangedEvent.Invoke(value);
			}
		}
	}
}
