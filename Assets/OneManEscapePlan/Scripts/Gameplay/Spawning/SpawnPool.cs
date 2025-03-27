using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	public enum PoolFullBehavior {
		ReturnNull, ThrowError
	}

	/// <summary>
	/// A SpawnPool manages a pool of recycleable objects. When we request an object from the pool,
	/// it returns an inactive object, or creates and returns a new object if there is still room
	/// in the pool. When the object would otherwise be destroyed, we will return it to the pool
	/// instead.
	/// 
	/// Pooling is better for performance for objects that would otherwise be created and destroyed
	/// frequently.
	/// </summary>
	/// COMPLEXITY: Advanced
	/// CONCEPTS: Factories, HashSets, Queues, 
	/// <seealso cref="OneManEscapePlan.Scripts.Gameplay.Spawning.PooledObject"/>
	public class SpawnPool : FactoryBase, IFactory {

		[SerializeField] protected GameObject prefab;

		[Tooltip("What will happen if the pool is asked to return an object but is already at maximum capacity")]
		[SerializeField] protected PoolFullBehavior poolFullBehavior;
		[Tooltip("How many instances will be created when the pool first initializes")]
		[SerializeField] protected int startingSize;
		[Tooltip("The maximum number of objects that the pool can manage")]
		[SerializeField] protected int maxCapacity;

		[DrawConnections(ColorName.Blue)]
		[SerializeField] protected PooledObjectEvent spawnEvent = new PooledObjectEvent();

		protected HashSet<PooledObject> active;
		protected Queue<PooledObject> inactive;

		virtual protected void Awake() {
			initialize();
		}

		virtual protected void initialize() {
			if (isInitialized) return;

			Assert.IsNotNull<GameObject>(prefab, "You forgot to select the prefab");

			active = new HashSet<PooledObject>();
			inactive = new Queue<PooledObject>();

			if (startingSize > 0) {
				while (Size < startingSize) {
					PooledObject po = createInstance();
					inactive.Enqueue(po);
					po.gameObject.SetActive(false);
				}
			}
		}

		/// <summary>
		/// Destroy all objects that have been spawned by this pool, whether they are active or inactive
		/// </summary>
		public void Clear() {
			if (!isInitialized) initialize();

			foreach (PooledObject po in active) {
				if (po != null) {
					Destroy(po.gameObject);
				}
			}
			foreach (PooledObject po in inactive) {
				if (po != null) {
					Destroy(po.gameObject);
				}
			}

			active.Clear();
			inactive.Clear();
		}

		override public GameObject SpawnObject() {
			PooledObject po = spawnObject();
			if (po == null) return null;

			spawnEvent.Invoke(po);
			return po.gameObject;
		}

		virtual protected PooledObject spawnObject() {
			if (!isInitialized) initialize();

			PooledObject po = null;
			if (inactive.Count > 0) {
				po = inactive.Dequeue();
			} else {
				if (Size < maxCapacity) {
					po = createInstance();
				} else {
					if (poolFullBehavior == PoolFullBehavior.ReturnNull) {
						return null;
					} else if (poolFullBehavior == PoolFullBehavior.ThrowError) {
						throw new System.InvalidOperationException("SpawnPool is full; cannot spawn a new instance");
					}
				}
			}

			if (po == null) return null;

			active.Add(po);
			po.ReturnToPoolEvent.AddListener(onReturnObjectToPool);
			po.gameObject.SetActive(true);
			return po;
		}

		override public GameObject SpawnObject(Transform parent) {
			PooledObject po = spawnObject();
			if (po == null) return null;

			po.transform.SetParent(parent, false);
			spawnEvent.Invoke(po);
			return po.gameObject;
		}

		override public GameObject SpawnObject(Vector3 position, Quaternion rotation) {
			PooledObject po = spawnObject();
			if (po == null) return null;

			po.transform.position = position;
			po.transform.rotation = rotation;
			spawnEvent.Invoke(po);
			return po.gameObject;
		}

		override public GameObject SpawnObject(Vector3 position, Quaternion rotation, Transform parent = null) {
			PooledObject po = spawnObject();
			if (po == null) return null;

			po.transform.SetParent(parent, false);
			po.transform.position = position;
			po.transform.rotation = rotation;
			spawnEvent.Invoke(po);
			return po.gameObject;
		}

		override public void SpawnObjectAt(Transform location) {
			SpawnObject(location.position, Quaternion.identity);
		}	
		
		public void SpawnObjectAt(Vector3 position) {
			SpawnObject(position, Quaternion.identity);
		}

		virtual protected PooledObject createInstance() {
			GameObject go = Instantiate(prefab);
			PooledObject po = go.GetComponent<PooledObject>();
			if (po == null) {
				po = go.AddComponent<PooledObject>();
			}
			return po;
		}

		virtual protected void onReturnObjectToPool(PooledObject po) {
			if (active.Contains(po)) {
				active.Remove(po);
				inactive.Enqueue(po);
				po.ReturnToPoolEvent.RemoveListener(onReturnObjectToPool);
				po.gameObject.SetActive(false);
			} else {
#if UNITY_EDITOR
				Debug.LogWarning("Tried to return a non-active PooledObject to the pool. This should never happen.");
#endif
			}
		}

		protected bool isInitialized {
			get {
				//if we've created the `inactive` queue, we must be initialized
				return inactive != null;
			}
		}

		public int Size {
			get {
				if (!isInitialized) initialize();
				if (active == null) return 0;
				return active.Count + inactive.Count;
			}
		}

		public int NumActive {
			get {
				if (!isInitialized) initialize();
				if (active == null) return 0;
				return active.Count;
			}
		}

		public int NumInactive {
			get {
				if (!isInitialized) initialize();
				if (inactive == null) return 0;
				return inactive.Count;
			}
		}

		/// <summary>
		/// When this SpawnPool is destroyed, we want to clean up any objects that were spawned by it
		/// </summary>
		virtual protected void OnDestroy() {
			Clear();
		}

	}
}
