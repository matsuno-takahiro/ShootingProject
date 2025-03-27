/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.UI {

	/// <summary>
	/// Assign the attached canvas to the main camera
	/// </summary>
	/// COMPLEXITY: Beginner
	[RequireComponent(typeof(Canvas))]
	public class AssignCanvasToMainCamera : MonoBehaviour {

		[SerializeField] protected float planeDistance = 100f;

		// Use this for initialization
		virtual protected void Awake() {
			Canvas canvas = GetComponent<Canvas>();
			canvas.worldCamera = Camera.main;
			canvas.planeDistance = planeDistance;
		}
	}
}
