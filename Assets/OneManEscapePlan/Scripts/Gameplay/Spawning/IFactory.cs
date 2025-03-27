using UnityEngine;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	/// <summary>
	/// IFactory defines an interface for getting instances of a GameObject. These could be newly created instances,
	/// instances managed by a pool, etc. We connect Spawners to Factories to provide the instances that are spawned
	/// by the Spawner.
	/// </summary>
	public interface IFactory {

		/// <summary>
		/// Spawn an instance from the prefab at its default position
		/// </summary>
		/// <returns>The newly created instance</returns>
		GameObject SpawnObject();

		/// <summary>
		/// Spawn an instance from the prefab at its default position, with the given parent
		/// </summary>
		/// <param name="parent">The Transform that will be the new object's parent</param>
		/// <returns>The newly created instance</returns>
		GameObject SpawnObject(Transform parent);

		/// <summary>
		/// Spawn an instance from the prefab at the position of the given Transform
		/// </summary>
		/// <param name="location">A Transform</param>
		void SpawnObjectAt(Transform location);

		/// <summary>
		/// Spawn an instance from the prefab at the given position and with the given rotation
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <returns>The newly created instance</returns>
		GameObject SpawnObject(Vector3 position, Quaternion rotation);

		/// <summary>
		/// Spawn an instance from the prefab at the given position and with the given rotation, optionally with the given parent
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <param name="parent">The Transform that will be the new object's parent</param>
		/// <returns>The newly created instance</returns>
		GameObject SpawnObject(Vector3 position, Quaternion rotation, Transform parent = null);
	}
}