/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	[System.Serializable]
	public class Movement {
		public Vector3 direction;
		public float speed;
		public float duration;
	}

	[System.Serializable]
	public class ScriptedPatternEvent : UnityEvent<ScriptedPattern> { }

	/// <summary>
	/// This movement pattern moves the AI according to a sequence of scripted movements.
	/// Each step in the sequence has a direction, speed, and rotation. So for example, we
	/// could tell the AI to move forward at 5 units/s for 3 seconds, then upward at 3 u/s
	/// for 8 seconds.
	/// 
	/// This is much harder to work with than a follow-curve pattern, but is more flexible.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: 3D movement, Quaternions, World vs Local space, Interpolation
	public class ScriptedPattern : MovementPattern {

		[Tooltip("Whether the directions are interpreted as local ('self') space or world space")]
		[SerializeField] protected Space space = Space.Self;
		[Tooltip("Sequence of movements")]
		[SerializeField] protected List<Movement> script = new List<Movement>();
		[Range(0.01f, 1)]
		[Tooltip("Controls how fast we turn towards the next direction when we reach the next step in the sequence")]
		[SerializeField] protected float turnInterpolationRate = .15f;

		[SerializeField] protected bool loop = true;

		[Tooltip("This event is invoked when the last step in the sequence is completed")]
		[SerializeField] protected ScriptedPatternEvent sequenceCompleteEvent = new ScriptedPatternEvent();
		public ScriptedPatternEvent SequenceCompleteEvent { get { return sequenceCompleteEvent; } }

		protected int scriptIndex = 0;
		protected float t = 0;

		protected override void Awake() {
			base.Awake();

			if (space == Space.Self) {
				foreach (Movement movement in script) {
					movement.direction = transform.TransformDirection(movement.direction.normalized);
				}
			}
		}

		protected void FixedUpdate() {
			//if we've completed the script, invoke the event, and start over if we are set to loop
			if (scriptIndex >= script.Count) {
				sequenceCompleteEvent.Invoke(this);
				if (loop) scriptIndex = 0;
				else return;
			}

			Movement movement = script[scriptIndex];
			Vector3 desiredHeading = movement.direction;

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredHeading, transform.up), turnInterpolationRate);
			//move forward
			Vector3 distance = (transform.forward * movement.speed * Time.fixedDeltaTime);
			waypointComponent.waypoint += distance;
			transform.position += distance;

			t += Time.fixedDeltaTime;
			if (t >= movement.duration) {
				t = 0;
				scriptIndex++;
			}
		}

	}
}
