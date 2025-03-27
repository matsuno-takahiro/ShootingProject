using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	/// <summary>
	/// SceneFactoryLink looks for a factory with a specific name and tries to use that factory
	/// to spawn objects. When you want multiple prefabs to share a single spawn pool, you can
	/// place the spawn pool in a scene and then attach the SceneFactoryLink to the prefabs.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Factories
	public class SceneFactoryLink : FactoryBase, IFactory {

		[SerializeField] protected string factoryName;

		protected FactoryBase sceneFactory;

		virtual protected void Awake() {
			Assert.IsFalse(string.IsNullOrEmpty(factoryName), "You forgot to enter a factory name");
			GameObject go = GameObject.Find(factoryName);
			Assert.IsNotNull<GameObject>(go, "Unable to find a GameObject named " + factoryName);
			sceneFactory = go.GetComponent<FactoryBase>();
			Assert.IsNotNull<FactoryBase>(sceneFactory, "Unable to find a FactoryBase on " + go.name);
		}

		override public GameObject SpawnObject() {
			return sceneFactory.SpawnObject();
		}

		override public GameObject SpawnObject(Transform parent) {
			return sceneFactory.SpawnObject(parent);
		}

		override public GameObject SpawnObject(Vector3 position, Quaternion rotation) {
			return sceneFactory.SpawnObject(position, rotation);
		}

		override public GameObject SpawnObject(Vector3 position, Quaternion rotation, Transform parent = null) {
			return sceneFactory.SpawnObject(position, rotation, parent);
		}

		override public void SpawnObjectAt(Transform location) {
			sceneFactory.SpawnObjectAt(location);
		}

#if UNITY_EDITOR
		//Draw a line from this object to the factory it links to
		protected void OnDrawGizmosSelected() {
			if (sceneFactory != null) {
				Gizmos.color = Color.magenta;
				Gizmos.DrawLine(transform.position, sceneFactory.transform.position);
			}
		}
#endif
	}
}
