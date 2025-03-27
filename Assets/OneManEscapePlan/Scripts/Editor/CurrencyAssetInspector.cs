/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Editor {

	[CustomEditor(typeof(CurrencyAsset))]
	public class CurrencyAssetInspector : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			// Show default inspector property editor
			DrawDefaultInspector();

			CurrencyAsset asset = (CurrencyAsset)target;

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Examples:", EditorStyles.boldLabel);
			EditorGUILayout.LabelField(asset.Format(12.00f));
			EditorGUILayout.LabelField(asset.Format(25.39f));

		}
	}

}
