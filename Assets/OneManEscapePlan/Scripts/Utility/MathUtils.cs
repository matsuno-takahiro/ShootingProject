/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneManEscapePlan.Scripts.Utility {

	/// <summary>
	/// Helper functions for mathmatical operations
	/// </summary>
	public static class MathUtils {

		/// <summary>
		/// Convert the given angle to an equivalent angle between -180 and 180 degrees. For example, 200
		/// would be converted to -160.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static float constrainAngle(float input) {
			while (input > 180) {
				input -= 360;
			}
			input = input % 180;
			return input;
		}
	}
}
