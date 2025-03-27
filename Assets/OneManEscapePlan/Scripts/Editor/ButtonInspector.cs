using OneManEscapePlan.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.Editor {

	/// <summary>
	/// THIS CLASS CURRENTLY DOES NOT WORK. Although the Unity documentation claims that you can
	/// extend ButtonEditor ( https://docs.unity3d.com/ScriptReference/UI.ButtonEditor.html ),
	/// this is not currently possible (as of Unity 2018.2.11f1) because ButtonEditor is hidden in 
	/// a DLL. Uncommenting this class will produce a compiler warning: 
	/// "Unable to open Unity/2018.2.11f1/Editor/Data/UnityExtensions/Unity/GUISystem/Standalone/UnityEngine.UI.dll: Check external application preferences."
	/// </summary>

	/*
	[CustomEditor(typeof(Button))]
	public class ButtonInspector : UnityEditor.UI.ButtonEditor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			Button button = target as Button;

			Debug.Log("Test");
			button.DrawLinesToListeners(button.onClick, Color.blue);
		}

		public void OnSceneGUI() {
			Button button = target as Button;
			button.DrawLinesToListeners(button.onClick, Color.blue);
		}
	}
	*/

}
