/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem {

	/// <summary>
	/// A PointCollector collects score points for a point receiver
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Interfaces
	public class PointCollector : MonoBehaviour, ICollectPoints {

		protected IReceivePoints pointsReceiver;

		virtual public void CheckForPoints(GameObject target) {
			if (pointsReceiver == null) return;

			IGivePoints igp = target.GetComponent<IGivePoints>();
			if (igp != null) igp.GivePoints(pointsReceiver);
		}

		virtual public void CheckForPoints(IDamage damage, IHealth health) {
			if (pointsReceiver == null || health == null || health.HP > 0) return;

			IGivePoints igp = health.GetComponent<IGivePoints>();
			if (igp != null) igp.GivePoints(pointsReceiver);
		}

		public void CopyReceiver(GameObject go) {
			PointCollector collector = go.GetComponent<PointCollector>();
			if (collector != null) CopyReceiver(collector);
		}

		public void CopyReceiver(PointCollector collector) {
			Assert.IsNotNull<PointCollector>(collector, "PointCollector can't copy its PointsReceiver to a null instance");
			collector.PointsReceiver = this.pointsReceiver;
		}

		/// <summary>
		/// The object that will receive score points collected by this PointCollector
		/// </summary>
		public IReceivePoints PointsReceiver {
			get {
				return pointsReceiver;
			}

			set {
				pointsReceiver = value;
			}
		}

	}
}
