/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.UI.Panels;
using OneManEscapePlan.Scripts.Utility;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.HUD;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

	/// <summary>
	/// Attach this component to a Panel where the player can adjust the game settings.
	/// </summary>
	/// COMPLEXITY: Intermediate
	/// CONCEPTS: Panels, Coroutines, AudioMixers
	[DisallowMultipleComponent]
	public class SettingsPanel : MenuPanel {

		[Tooltip("The master audio mixer for this game")]
		[SerializeField] protected AudioMixer masterMixer;
		[Tooltip("A UI slider used to adjust the music volume")]
		[SerializeField] protected Slider musicSlider;
		[Tooltip("A UI slider used to adjust the sound effects volume")]
		[SerializeField] protected Slider soundEffectsSlider;

		virtual protected void Awake() {
			Assert.IsNotNull<AudioMixer>(masterMixer, "You forgot to select the master audio mixer");
			Assert.IsNotNull<Slider>(musicSlider, "You forgot to select the music slider");
			Assert.IsNotNull<Slider>(soundEffectsSlider, "You forgot to select the sound effects slider");

			float musicDecibelLevel;
			float sfxDecibelLevel;
			masterMixer.GetFloat("MusicVolume", out musicDecibelLevel);
			masterMixer.GetFloat("SFXVolume", out sfxDecibelLevel);

			musicSlider.value = AudioUtils.DecibelToNormalized(musicDecibelLevel);
			soundEffectsSlider.value = AudioUtils.DecibelToNormalized(sfxDecibelLevel);
		}

		virtual public void SetMusicVolume(float normalizedVolume) {
			masterMixer.SetFloat("MusicVolume", AudioUtils.NormalizedToDecibel(normalizedVolume));
		}

		virtual public void SetSoundEffectsVolume(float normalizedVolume) {
			masterMixer.SetFloat("SFXVolume", AudioUtils.NormalizedToDecibel(normalizedVolume));
		}

		/// <summary>
		/// Show the Controls panel
		/// </summary>
		virtual public void ShowControls() {
			PanelManager panelManager = GameObject.FindObjectOfType<PanelManager>();
			Assert.IsNotNull<PanelManager>(panelManager, "Couldn't find PanelManager");
			ControlsPanel cp = panelManager.ShowControlsPanel();
			showChildPanel(cp);
		}

		virtual protected void OnDisable() {
			Preferences.MusicVolume.Value = musicSlider.value;
			Preferences.SoundEffectsVolume.Value = soundEffectsSlider.value;
			Preferences.Save();
		}
	}
}
