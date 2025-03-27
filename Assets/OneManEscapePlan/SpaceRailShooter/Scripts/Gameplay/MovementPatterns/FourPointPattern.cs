/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.MovementPatterns {

	/// <summary>
	/// This pattern causes the AI to randomly move between 4 points equally spaced around the 
	/// waypoint. This can make the AI very difficult to hit
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Coroutines
	public class FourPointPattern : MovementPattern {

		[SerializeField] private float range = 1f;
		[SerializeField] private float speed = 5f;
		[SerializeField] private float minLingerTime = .5f;
		[SerializeField] private float maxLingerTime = 1f;

		protected Vector3[] corners = new Vector3[4];

		protected Vector3 target;
		protected int lastCorner = -1;
		protected float lastRange;

		override protected void Awake() {
			base.Awake();
			StartCoroutine(nextTarget());

			initCorners();
		}

		protected void initCorners() {
			corners[0] = new Vector3(-range, -range, 0);
			corners[1] = new Vector3(-range, range, 0);
			corners[2] = new Vector3(range, range, 0);
			corners[3] = new Vector3(range, -range, 0);
			lastRange = range;
		}

		/// <summary>
		/// Pick a new corner and fly to it
		/// </summary>
		/// <returns></returns>
		virtual protected IEnumerator nextTarget() {
			if (lastRange != range) initCorners();

			//pick a new corner to fly to, which cannot be the current corner
			int corner = lastCorner;
			while (corner == lastCorner) corner = Random.Range(0, 4);
			lastCorner = corner;

			target = waypointComponent.waypoint + corners[corner];
			while (Vector3.Distance(transform.position, target) > speed / 100f) {
				transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
				yield return new WaitForFixedUpdate();
				target = waypointComponent.waypoint + corners[corner];
			}

			yield return new WaitForSeconds(Random.Range(minLingerTime, maxLingerTime));

			StartCoroutine(nextTarget());
		}

		#region Properties
		public float Range {
			get {
				return range;
			}

			set {
				range = value;
			}
		}

		public float Speed {
			get {
				return speed;
			}

			set {
				speed = value;
			}
		}

		public float MinLingerTime {
			get {
				return minLingerTime;
			}

			set {
				minLingerTime = value;
			}
		}

		public float MaxLingerTime {
			get {
				return maxLingerTime;
			}

			set {
				maxLingerTime = value;
			}
		}
		#endregion

#if UNITY_EDITOR
		private void OnDrawGizmosSelected() {
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(target, .5f);
		}
#endif
	}
}
