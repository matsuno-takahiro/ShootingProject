using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component display a slider in the Inspector which we can use to adjust the TimeScale on the fly in the Editor
/// </summary>
/// COMPLEXITY: Beginner
/// CONCEPTS: TimeScale
public class DebugTimescale : MonoBehaviour {

	[Range(0, 10f)]
	[SerializeField] protected float timeScale = 1;

	// Start is called before the first frame update
	void Start() {

	}

#if UNITY_EDITOR
	// Update is called once per frame
	void Update() {
		if (Time.timeScale != timeScale) {
			Time.timeScale = timeScale;
		}
	}
#endif
}
