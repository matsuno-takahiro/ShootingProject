using UnityEngine;

namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// A float Property that is synchronized with PlayerPrefs
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inheritance, PlayerPrefs
	public class PlayerPrefsFloatProperty : PlayerPrefsProperty<float> {

		public PlayerPrefsFloatProperty(string key, float defaultValue) : base(key, defaultValue) {
		}

		public override float Value {
			get {
				return PlayerPrefs.GetFloat(key, defaultValue);
			}

			set {
				PlayerPrefs.SetFloat(key, value);
				ValueChangedEvent.Invoke(value);
			}
		}
	}
}
