/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Animation;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Animation {

	/// <summary>
	/// Causes the GameObject to turn towards the player each frame
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Inheritance
	public class LookAtPlayer : TurnTowardsTarget {

		[DrawConnections(ColorName.PulsingYellow)]
		[SerializeField] protected PlayerSpacecraft player;

		protected override void FixedUpdate() {
			if (player == null) {
				player = GameObject.FindObjectOfType<PlayerSpacecraft>();
				if (player != null) Target = player.transform;
			}

			//stop following player when we are about to fall behind them
			if (player != null && transform.position.z <= player.transform.position.z + 1) return;

			base.FixedUpdate();
		}
	}
}
