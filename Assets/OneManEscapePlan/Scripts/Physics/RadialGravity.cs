using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Physics {

	/// <summary>
	/// Pulls affected RigidBodies towards this object's center. Use a trigger SphereCollider to
	/// define the boundaries in which objects will be affected.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Physics, Colliders
	public class RadialGravity : MonoBehaviour {

		[SerializeField] protected float mass = 1000;
		[SerializeField] protected float gravity = .6674f;

		protected HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();

		virtual protected void FixedUpdate() {
			foreach (Rigidbody body in affectedBodies) {
				if (body != null) {
					float distance = Vector3.Distance(transform.position, body.transform.position);
					Vector3 direction = (transform.position - body.transform.position).normalized;
					Vector3 force = (direction * gravity * mass) / (distance * distance);
					//Debug.Log("Adding force " + force + " to " + body.gameObject.name);
					body.AddForce(force);
				}
			}
		}

		virtual protected void OnTriggerEnter(Collider other) {
			affectedBodies.Add(other.attachedRigidbody);
		}

		virtual protected void OnTriggerExit(Collider other) {
			affectedBodies.Remove(other.attachedRigidbody);
		}
	}
}
