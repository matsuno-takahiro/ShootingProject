/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Properties;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts {

    /// <summary>
    /// PlayerPrefsのラッパーとして機能し、設定キーのスペルミスや
    /// ブール値を整数に手動でキャストすることを心配せずに保存された設定にアクセスできます。
    /// 
    /// セーブデータはPlayerPrefsに保存しないでください。セーブデータは
    /// 別のファイルに保存する必要があります。
    /// </summary>
    /// 複雑さ: 初心者
    public static class Preferences
    {

        private static PlayerPrefsBoolProperty invertYAxis = new PlayerPrefsBoolProperty("invert_y_axis", true);
        public static PlayerPrefsBoolProperty InvertYAxis { get { return invertYAxis; } }

        private static PlayerPrefsFloatProperty musicVolume = new PlayerPrefsFloatProperty("music_volume", 1);
        public static PlayerPrefsFloatProperty MusicVolume { get { return musicVolume; } }

        private static PlayerPrefsFloatProperty soundEffectsVolume = new PlayerPrefsFloatProperty("sfx_volume", 1);
        public static PlayerPrefsFloatProperty SoundEffectsVolume { get { return soundEffectsVolume; } }

        /// <summary>
        /// この便利なメソッドはPlayerPrefs.Save()を呼び出すだけです
        /// </summary>
        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
