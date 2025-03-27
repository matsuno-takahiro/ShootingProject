/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {

	/// <summary>
	/// A static class with general-purpose helper functions for audio tasks.
	/// </summary>
	public static class AudioUtils {

		/// <summary>
		/// Convert a decibel level to a normalized volume (value in range 0 - 1)
		/// </summary>
		/// <param name="decibel"></param>
		/// <returns></returns>
		public static float DecibelToNormalized(float decibel) {
			return Mathf.Pow(10, decibel / 20f);
		}

		/// <summary>
		/// Convert a normalized volume (value in range 0 - 1) to a decibel level
		/// </summary>
		/// <param name="normalized"></param>
		/// <returns></returns>
		public static float NormalizedToDecibel(float normalized) {
			if (normalized <= 0) return -80;
			return Mathf.Log(normalized, 10) * 20;
		}
	}
}
