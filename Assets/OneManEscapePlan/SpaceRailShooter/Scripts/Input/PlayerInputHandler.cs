/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.HUD;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Input {

	/// <summary>
	/// This behaviour sends input from the Unity Input system to our spacecraft. Reading our input in
	/// a separate class like this may seem like a hassle at first, but it makes our game more
	/// flexible.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Input, 
	[RequireComponent(typeof(IPlayerMovement))]
	public class PlayerInputHandler : MonoBehaviour {

		[SerializeField] private List<WeaponInputMapping> weapons = null;

		private IPlayerMovement playerMovement;
		private Game game;

		private void Awake() {
			game = GameObject.FindObjectOfType<Game>();
			Assert.IsNotNull<Game>(game);

			//validate weapon-input mappings
			foreach (WeaponInputMapping weapon in weapons) {
				Assert.IsNotNull<ProjectileLauncher>(weapon.launcher, "You forgot to select a ProjectileLauncher instance");
				Assert.IsFalse(string.IsNullOrEmpty(weapon.buttonName), "You forgot to enter the button name that will fire the " + weapon.launcher.gameObject.name);
			}

			playerMovement = GetComponent<IPlayerMovement>();
		}

		// Update is called once per frame
		void Update() {
			if (game.IsPaused) return;

			//if user is pressing the menu button, pause and show the menu
			if (UnityEngine.Input.GetButtonDown("Menu")) {
				PanelManager pm = PanelManager.Current;
				Assert.IsNotNull<PanelManager>(pm);
				pm.ShowPausePanel();
			}

			//read and apply horizontal and vertical axis input
			playerMovement.HorizontalInput = UnityEngine.Input.GetAxis("Horizontal");
			playerMovement.VerticalInput = UnityEngine.Input.GetAxis("Vertical");

			//Fire any weapons for which we are pressing the correct button
			foreach (WeaponInputMapping weapon in weapons) {
				if (UnityEngine.Input.GetButton(weapon.buttonName))	weapon.launcher.Fire();
			}
		}
	}

	/// <summary>
	/// Used to map a virtual button name to a weapon that we want to be fired by that button
	/// </summary>
	[System.Serializable]
	public class WeaponInputMapping {
		[Tooltip("A ProjectileLauncher")]
		public ProjectileLauncher launcher;
		[Tooltip("The name of a button defined in the Project Input settings that will fire this weapon")]
		public string buttonName;
	}
}
