/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Shop {

	/// <summary>
	/// Defines an asset file representing a currency (such as dollars, euros, coins)
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: String formatting (https://docs.microsoft.com/en-us/dotnet/api/system.single.tostring?view=netframework-4.7.2#System_Single_ToString_System_String)
	[CreateAssetMenu(fileName = "Currency", menuName = "Shop/Currency", order = 0)]
	public class CurrencyAsset : ScriptableObject {

		[SerializeField] new private string name = "";
		[SerializeField] private string prefix = "";
		[SerializeField] private string suffix = "";
		[Tooltip("Used by float.ToString() method to format the numerical value. For example, 'C', 'C2', 'F3'")]
		[SerializeField] private string formatString = "";

		public string Name {
			get {
				return name;
			}
		}

		public string Prefix {
			get {
				return prefix;
			}
		}

		public string Suffix {
			get {
				return suffix;
			}
		}

		/// <summary>
		/// Format the given value using the default formatting settings
		/// </summary>
		/// <see cref="formatString"/>
		/// <param name="value"></param>
		/// <returns></returns>
		public string Format(float value) {
			return Format(value, this.formatString);
		}

		/// <summary>
		/// Format the given value using the given format string on the numeric part of the value, in addition to
		/// our defined prefix and suffix (if applicable). If the format string is invalid, it will be ignored.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="formatString">A format string accepted by C#, such as "C", "C2", "F3"</param>
		/// <returns></returns>
		public string Format(float value, string formatString) {
			if (prefix == null) prefix = "";
			if (suffix == null) suffix = "";
			if (!string.IsNullOrEmpty(formatString)) {
				try {
					return prefix + value.ToString(formatString) + suffix;
				}
				catch /*(System.Exception ex)*/ {
					//Debug.LogWarning(ex.Message); //outputs way too many warnings
				}
			}
			//default case when formatString is empty or invalid
			return prefix + value + suffix;
		}
	}
}