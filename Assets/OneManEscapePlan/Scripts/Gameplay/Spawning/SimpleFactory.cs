using OneManEscapePlan.Scripts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	/// <summary>
	/// The SimpleFactory creates new instances from a prefab
	/// </summary>
	/// COMPLEXITY: Beginner
	/// Concepts: Factories, Prefabs
	public class SimpleFactory : FactoryBase, IFactory {

		[SerializeField] protected GameObject prefab;

		[SerializeField] protected GameObjectEvent spawnEvent = new GameObjectEvent();
		public GameObjectEvent SpawnEvent { get { return spawnEvent; } }

		virtual protected void Awake() {
			Assert.IsNotNull<GameObject>(prefab, "You forgot to select the prefab");
		}

		/// <summary>
		/// Spawn the prefab at this factory's location
		/// </summary>
		virtual public void Spawn() {
			SpawnObject(transform.position, transform.rotation);
		}

		/// <summary>
		/// Spawn an instance from the prefab at its default position
		/// </summary>
		/// <returns>The newly created instance</returns>
		override public GameObject SpawnObject() {
			GameObject go = Instantiate(prefab);
			spawnEvent.Invoke(go);
			return go;
		}

		/// <summary>
		/// Spawn an instance from the prefab at its default position, with the given parent
		/// </summary>
		/// <param name="parent">The Transform that will be the new object's parent</param>
		/// <returns>The newly created instance</returns>
		override public GameObject SpawnObject(Transform parent) {
			GameObject go = Instantiate(prefab, parent);
			spawnEvent.Invoke(go);
			return go;
		}

		/// <summary>
		/// Spawn an instance from the prefab at the given position and with the given rotation
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <returns>The newly created instance</returns>
		override public GameObject SpawnObject(Vector3 position, Quaternion rotation) {
			GameObject go = Instantiate(prefab, position, rotation);
			spawnEvent.Invoke(go);
			return go;
		}

		/// <summary>
		/// Spawn an instance from the prefab at the given position and with the given rotation, optionally with the given parent
		/// </summary>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <param name="parent">The Transform that will be the new object's parent</param>
		/// <returns>The newly created instance</returns>
		override public GameObject SpawnObject(Vector3 position, Quaternion rotation, Transform parent = null) {
			GameObject go = Instantiate(prefab, position, rotation, parent);
			spawnEvent.Invoke(go);
			return go;
		}

		public override void SpawnObjectAt(Transform location) {
			SpawnObject(location.position, Quaternion.identity);
		}

		public GameObject Prefab {
			get {
				return prefab;
			}
		}

	}
}
