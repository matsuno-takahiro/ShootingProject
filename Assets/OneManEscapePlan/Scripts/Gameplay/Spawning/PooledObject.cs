using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	[Serializable]
	public class PooledObjectEvent : UnityEvent<PooledObject> { }

	/// <summary>
	/// PooledObject is attached to objects that are managed by a SpawnPool. It includes a method for returning
	/// the object to the pool, and invokes an event when this method is called. The pool will detect when this
	/// event is invoked and carry out the corresponding logic.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: UnityEvents
	public class PooledObject : MonoBehaviour {

		[DrawConnections(ColorName.PulsingBlue)]
		[SerializeField] protected PooledObjectEvent returnToPoolEvent = new PooledObjectEvent();
		public PooledObjectEvent ReturnToPoolEvent { get { return returnToPoolEvent; } }

		virtual public void ReturnToPool() {
			returnToPoolEvent.Invoke(this);

			//reset Rigidbody
			Rigidbody body = GetComponent<Rigidbody>();
			if (body != null) {
				body.velocity = Vector3.zero;
				body.angularVelocity = Vector3.zero;
			}
		}

	}
}
