using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	/// <summary>
	/// FactoryBase is an abstract base class for Factories - classes which create instances of
	/// GameObjects
	/// </summary>
	[Serializable]
	public abstract class FactoryBase : MonoBehaviour, IFactory {

		/// <summary>
		/// Spawn an instance from the prefab at its default position
		/// </summary>
		/// <returns>The newly created instance</returns>
		abstract public GameObject SpawnObject();

		/// <summary>
		/// Spawn an instance from the prefab at its default position, with the given parent
		/// </summary>
		/// <param name="parent">The Transform that will be the new object's parent</param>
		/// <returns>The newly created instance</returns>
		abstract public GameObject SpawnObject(Transform parent);

		/// <summary>
		/// Spawn an instance from the prefab at the given position and with the given rotation
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <returns>The newly created instance</returns>
		abstract public GameObject SpawnObject(Vector3 position, Quaternion rotation);

		/// <summary>
		/// Spawn an instance from the prefab at the given position and with the given rotation, optionally with the given parent
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <param name="parent">The Transform that will be the new object's parent</param>
		/// <returns>The newly created instance</returns>
		abstract public GameObject SpawnObject(Vector3 position, Quaternion rotation, Transform parent = null);

		abstract public void SpawnObjectAt(Transform location);
	}
}
