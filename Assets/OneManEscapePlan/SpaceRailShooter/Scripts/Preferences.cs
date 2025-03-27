/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Properties;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts {

	/// <summary>
	/// Serves as a wrapper for PlayerPrefs, allowing us to access saved preferences
	/// without having to worry about misspelling preference keys or manually casting
	/// booleans to/from integers.
	/// 
	/// PLEASE REMEMBER THAT SAVE GAME DATA SHOULD NOT BE SAVED IN PLAYER PREFS. Save
	/// game data should be saved in its own separate files.
	/// </summary>
	/// COMPLEXITY: Beginner
	public static class Preferences {

		private static PlayerPrefsBoolProperty invertYAxis = new PlayerPrefsBoolProperty("invert_y_axis", true);
		public static PlayerPrefsBoolProperty InvertYAxis { get { return invertYAxis; } }

		private static PlayerPrefsFloatProperty musicVolume = new PlayerPrefsFloatProperty("music_volume", 1);
		public static PlayerPrefsFloatProperty MusicVolume { get { return musicVolume; } }

		private static PlayerPrefsFloatProperty soundEffectsVolume = new PlayerPrefsFloatProperty("sfx_volume", 1);
		public static PlayerPrefsFloatProperty SoundEffectsVolume { get { return soundEffectsVolume; } }

		/// <summary>
		/// This convenience method just calls PlayerPrefs.Save()
		/// </summary>
		public static void Save() {
			PlayerPrefs.Save();
		}
	}
}
