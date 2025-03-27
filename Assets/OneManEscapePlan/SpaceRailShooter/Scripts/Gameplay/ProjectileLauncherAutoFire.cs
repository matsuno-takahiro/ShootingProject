/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// Automatically fires the attached ProjectileLauncher
	/// </summary>
	//TODO: add options for randomness, bursts, etc
	[RequireComponent(typeof(ProjectileLauncher))]
	public class ProjectileLauncherAutoFire : MonoBehaviour {

		protected float t;
		protected ProjectileLauncher launcher;

		virtual protected void Awake() {
			launcher = GetComponent<ProjectileLauncher>();
			t += Random.Range(-.15f, .25f);
		}

		virtual protected void Update() {
			t += Time.deltaTime;
			if (t >= launcher.Cooldown) {
				launcher.Fire();
				t -= launcher.Cooldown;
			}
		}
	}
}
