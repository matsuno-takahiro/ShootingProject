/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem {

	/// <summary>
	/// Attach this component to GameObjects that will give points to the player.
	/// PointCollectors will automatically detect these and give points to the
	/// player.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interfaces
	public class ScoreValueComponent : MonoBehaviour, IGivePoints {

		[Tooltip("The amount of points that will be given (can be negative)")]
		[SerializeField] protected int points;

		/// <summary>
		/// Look for a Player and give points to the first one we find. This can be
		/// triggered by events in the Inspector
		/// </summary>
		virtual protected void Activate() {
			Player player = GameObject.FindObjectOfType<Player>();
			GivePoints(player);
		}

		/// <summary>
		/// Add the score value to the given point receiver. This is automatically
		/// called by PointCollectors.
		/// </summary>
		/// <param name="receiver"></param>
		public void GivePoints(IReceivePoints receiver) {
			receiver.AddPoints(points);
		}

		public int Points {
			get {
				return points;
			}

			set {
				points = value;
			}
		}
	}
}
