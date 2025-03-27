/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {
	/// <summary>
	/// Displays a field for you to take notes. This has no effect on gameplay
	/// </summary>
	public class Notes : MonoBehaviour {
		//TODO: wrapping the notes field in this compiler directive prevents
		//the field from getting compiled into builds. Does that mean that the
		//actual note strings are stripped from our scene/prefab data?
#if UNITY_EDITOR
		[TextArea(1, 4)]
		public string notes;
#endif
	}
}
