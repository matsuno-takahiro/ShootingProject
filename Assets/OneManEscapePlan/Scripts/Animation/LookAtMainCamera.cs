/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Animation {
	public class LookAtMainCamera : LookAtTarget {
		protected void Awake() {
			Camera camera = Camera.main;
			if (camera != null) target = camera.transform;
		}
	}
}
