/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using UnityEngine;

namespace OneManEscapePlan.Scripts.Animation.Curves {
	public interface I3DCurveFunction {
		Vector3 Evaluate(float t);
	}
}