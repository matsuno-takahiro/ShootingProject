using OneManEscapePlan.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Invokes a TransformEvent when we collide with a trigger
/// </summary>
/// COMPLEXITY: Beginner
/// CONCEPTS: Colliders, Unity Events
public class ShieldImpactTrigger : MonoBehaviour {

	[SerializeField] protected TransformEvent collisionEnterEvent = new TransformEvent();
	public TransformEvent TriggerEnterEvent { get { return collisionEnterEvent; } }

	protected void OnTriggerEnter(Collider other) {
		collisionEnterEvent.Invoke(other.transform);
	}
}
