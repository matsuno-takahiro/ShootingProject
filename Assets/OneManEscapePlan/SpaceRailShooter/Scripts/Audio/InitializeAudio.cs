/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Audio {

	/// <summary>
	/// Initialize the audio system by reading the volume settings from Preferences
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Audio mixers, decibels, normalization
	public class InitializeAudio : MonoBehaviour {

		[SerializeField] protected AudioMixer masterMixer;

		virtual protected void Start() {
			Assert.IsNotNull<AudioMixer>(masterMixer, "You forgot to select the master audio mixer");

			//note that we have to expose parameters on our master audio mixer in order to adjust the volume.
			//See https://unity3d.com/learn/tutorials/topics/audio/exposed-audiomixer-parameters
			//Decibels are the scientific unit for measuring sound intensity (volume)
			//"Normalized" means "converted to a value between 0 and 1"
			masterMixer.SetFloat("MusicVolume", AudioUtils.NormalizedToDecibel(Preferences.MusicVolume.Value));
			masterMixer.SetFloat("SFXVolume", AudioUtils.NormalizedToDecibel(Preferences.SoundEffectsVolume.Value));

			Destroy(this); //Once the mixer has been initialized, this behaviour is no longer needed
		}
	}
}
