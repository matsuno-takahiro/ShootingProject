
namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// Base class for Properties that are synchronized with PlayerPrefs
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Inheritance, Generics
	public class PlayerPrefsProperty<T> : Property<T> {

		protected string key;
		protected T defaultValue;

		public PlayerPrefsProperty(string key, T defaultValue) : base() {
			this.key = key;
			this.defaultValue = defaultValue;
		}

		public T DefaultValue {
			get {
				return defaultValue;
			}
			set {
				defaultValue = value;
			}
		}

		virtual public string Key {
			get {
				return key;
			}
		}
	}
}
