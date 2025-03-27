/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Load a scene by name when the attached button is pressed
/// </summary>
/// COMPLEXITY: Beginner
/// CONCEPTS: Scene management, Buttons
[RequireComponent(typeof(Button))]
public class LoadSceneButton : MonoBehaviour {

	[Tooltip("Name of the scene to load")]
	[SerializeField] protected string sceneName;
	public bool loadAsync = true;

	protected Button button;

	void Start () {
		this.SceneName = sceneName;
		button.onClick.AddListener(StartScene);
	}

	/// <summary>
	/// Start the scene with the chosen SceneName
	/// </summary>
	public void StartScene() {
		if (loadAsync) SceneManager.LoadSceneAsync(sceneName);
		else SceneManager.LoadScene(sceneName);
	}

	public string SceneName {
		get {
			return sceneName;
		}

		set {
			Assert.IsFalse(string.IsNullOrEmpty(value), "SceneName cannot be null or empty");
			sceneName = value;
		}
	}
}
