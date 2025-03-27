/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.Scripts.Gameplay.Spawning;
using OneManEscapePlan.Scripts.UI.HUD;
using OneManEscapePlan.SpaceRailShooter.Scripts.Animation;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// PlayerSpawner is used to respawn the player after the player has died.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Coroutines, Factories
	[DisallowMultipleComponent]
	public class PlayerSpawner : MonoBehaviour {

		[DrawConnections(ColorName.Magenta)]
		[SerializeField] protected FactoryBase playerFactory;
		[DrawConnections(ColorName.Cyan)]
		[SerializeField] new protected FollowCamera camera;
		[Tooltip("Delay before the player respawns, in seconds")]
		[SerializeField] protected float respawnDelay = 3.0f;
		[SerializeField] protected HealthBar healthBar;
		[SerializeField] protected PlayerContainer playerContainer;

		[DrawConnections(ColorName.Orange)]
		[SerializeField] protected PlayerSpacecraftEvent playerSpawnedEvent = new PlayerSpacecraftEvent();
		public PlayerSpacecraftEvent PlayerSpawnedEvent { get { return playerSpawnedEvent; } }

		virtual protected void Awake() {
			Assert.IsNotNull<FactoryBase>(playerFactory, "You forgot to select a Player factory");
			Assert.IsNotNull<FollowCamera>(camera, "You forgot to select the camera");
			Assert.IsNotNull<PlayerContainer>(playerContainer, "You forgot to select the PlayerContainer");
		}

		/// <summary>
		/// Respawn the Player at the given position, after a short delay
		/// </summary>
		/// <param name="position">The position to spawn the player at</param>
		public void SpawnPlayer(Vector3 position) {
			StartCoroutine(spawnPlayer(respawnDelay, position));
		}

		virtual protected IEnumerator spawnPlayer(float delay, Vector3 position) {
			if (delay > 0) yield return new WaitForSeconds(delay);

			//Spawn the new spacecraft
			GameObject go = playerFactory.SpawnObject(position, new Quaternion(), playerContainer.transform);
			PlayerSpacecraft spacecraft = go.GetComponent<PlayerSpacecraft>();
			Assert.IsNotNull<PlayerSpacecraft>(spacecraft, "The prefab you assigned to the player factory does not contain the PlayerSpacecraft script");

			//Make the camera follow the new spacecraft
			camera.target = spacecraft.transform;
			//Update the HUD health-bar to point at the new spacecraft's health
			healthBar.HealthComponent = spacecraft.GetComponent<HealthComponent>();

			playerContainer.PlayerSpacecraft = spacecraft;
			playerSpawnedEvent.Invoke(spacecraft);
		}
	}
}
