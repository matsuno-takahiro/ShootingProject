/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {
	/// <summary>
	/// Disable this GameObject on the selected platforms
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Preprocessor directives (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-if) 
	/// https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
	public class HideOnPlatform : MonoBehaviour {

		[SerializeField] private bool hideOnIOS = false;
		[SerializeField] private bool hideOnAndroid = false;
		[SerializeField] private bool hideOnWindows = false;
		[SerializeField] private bool hideOnOSX = false;
		[SerializeField] private bool hideOnLinux = false;
		[SerializeField] private bool hideOnWebGL = false;

		// Use this for initialization
		virtual protected void Awake() {
#if UNITY_ANDROID
			if (hideOnAndroid) hide();
#elif UNITY_IOS
			if (hideOnIOS) hide();
#elif UNITY_STANDALONE_WIN
			if (hideOnWindows) hide();
#elif UNITY_STANDALONE_LINUX
			if (hideOnLinux) hide();
#elif UNITY_STANDALONE_OSX
			if (hideOnOSX) hide();
#elif UNITY_WEBGL
			if (hideOnWebGL) hide();
#endif
		}

		virtual protected void hide() {
			gameObject.SetActive(false);
		}

		override public string ToString() {
			return gameObject.name + ": hideOnIOS = " + hideOnIOS + ", hideOnAndroid = " + hideOnAndroid + ", hideOnWindows = " + hideOnWindows
				+ ", hideOnOSX = " + hideOnOSX + ", hideOnLinux = " + hideOnLinux + ", hideOnWebGL = " + hideOnWebGL;
		}
	}
}
