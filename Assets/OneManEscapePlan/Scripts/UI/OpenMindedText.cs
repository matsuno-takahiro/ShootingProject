/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component can be used to receive values of various types by targeting its methods in the inspector.
/// It's "open-minded" because it takes ints, floats, objects, etc, and I couldn't think of a better name.
/// </summary>
/// COMPLEXITY: Beginner
/// CONCEPTS: Function overloading, Numeric formats for strings 
[RequireComponent(typeof(Text))]
[DisallowMultipleComponent]
public class OpenMindedText : MonoBehaviour {
	[Tooltip("Used by numeric type ToString() methods to format the numerical value. For example, 'n0', 'C2', 'F3'")]
	[SerializeField] protected string numberFormat = "n0";

	private Text text;

	private bool isInitialized = false;

	// Use this for initialization
	void Awake () {
		if (!isInitialized) init();
	}

	private void init() {
		text = GetComponent<Text>();
	}

	public void SetText(int value) {
		if (!isInitialized) init();
		text.text = value.ToString(numberFormat);
	}

	public void SetText(float value) {
		if (!isInitialized) init();
		text.text = value.ToString(numberFormat);
	}

	public void SetText(GameObject value) {
		if (!isInitialized) init();
		text.text = value.ToString();
	}

	public void SetText(Object value) {
		if (!isInitialized) init();
		text.text = value.ToString();
	}

	public void SetText(object value) {
		if (!isInitialized) init();
		text.text = value.ToString();
	}
}
