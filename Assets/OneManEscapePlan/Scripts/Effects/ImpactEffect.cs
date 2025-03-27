/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Effects {

	/// <summary>
	/// Instantiate an effect prefab at the point of impact when a collision occurs.
	/// For example, say you want a puff of smoke to appear when your cannonball
	/// collides with something. You could use the ImpactEffect component for this
	/// purpose.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Collisions, Prefabs
	public class ImpactEffect : MonoBehaviour {

		[DrawConnections(ColorName.Yellow)]
		[Tooltip("A prefab or scene object that will be used as the effect")]
		[SerializeField] private GameObject effect = null;

		private void Awake() {
			Assert.IsNotNull<GameObject>(effect, "You forgot to select an effect");
		}

		private void OnCollisionEnter(Collision collision) {
			//show effect where the collision occured
			showEffect(collision.contacts[0].point);
		}

		private void OnTriggerEnter(Collider other) {
			//we don't know exactly where the collision occured when using triggers, so
			//pick the point on the hit object's collider that is closest to our center point
			showEffect(other.ClosestPoint(transform.position));
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			//show effect where the collision occured
			showEffect(collision.GetContact(0).point);
		}

		private void OnTriggerEnter2D(Collider2D other) {
			//with 2D triggers, we have no way of knowing anything about where the colliders came
			//into contact. our only option is to pick the point halfway between the centers of
			//the two colliding objects
			showEffect((other.transform.position + this.transform.position) / 2);
		}

		private void showEffect(Vector3 position) {
			Assert.IsNotNull<GameObject>(effect);

			//if the effect is a prefab, instantiate it at the given position. if it is not a prefab,
			//make it visible at the given position
			if (effect.IsPrefab()) {
				Instantiate(effect, position, new Quaternion());
			} else {
				effect.transform.position = position;
				effect.transform.SetParent(null, true);
				effect.SetActive(true);
			}
		}

		private void OnDestroy() {
			if (effect != null && !effect.IsPrefab()) {
				Destroy(effect.gameObject);
			}
		}
	}
}
