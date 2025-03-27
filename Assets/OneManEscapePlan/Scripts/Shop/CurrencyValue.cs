/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Shop {

	[Serializable]
	public class CurrencyValueEvent : UnityEvent<CurrencyValue> { }

	/// <summary>
	/// A CurrencyValue represents an amount of a particular currency. For example, 25 Gold.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Operator overloading (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/operator)
	[System.Serializable]
	public struct CurrencyValue {

		[SerializeField] private CurrencyAsset currency;
		[SerializeField] private float value;

		/// <summary>
		/// Create a new CurrencyValue
		/// </summary>
		/// <param name="currency">The currency</param>
		/// <param name="value">The amount of the currency</param>
		public CurrencyValue(CurrencyAsset currency, float value) {
			if (currency == null) throw new System.ArgumentNullException("currency", "Currency cannot be null");
			
			this.currency = currency;
			this.value = value;
		}

		public CurrencyAsset Currency {
			get {
				return currency;
			}
		}

		public float Value {
			get {
				return value;
			}

			set {
				this.value = value;
			}
		}

		override public string ToString() {
			return currency.Format(value);
		}

		#region Operators

		public static CurrencyValue operator +(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot add currencies of different types");
			return new CurrencyValue(a.currency, a.value + b.value);
		}

		public static CurrencyValue operator -(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot subtract currencies of different types");
			return new CurrencyValue(a.currency, a.value - b.value);
		}

		public static CurrencyValue operator *(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot multiply currencies of different types");
			return new CurrencyValue(a.currency, a.value * b.value);
		}

		public static CurrencyValue operator /(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot divide currencies of different types");
			return new CurrencyValue(a.currency, a.value / b.value);
		}

		public static bool operator >(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot compare currencies of different types");
			return a.value > b.value;
		}
		public static bool operator >=(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot compare currencies of different types");
			return a.value >= b.value;
		}
		public static bool operator <(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot compare currencies of different types");
			return a.value < b.value;
		}
		public static bool operator <=(CurrencyValue a, CurrencyValue b) {
			if (a.currency != b.currency) throw new Exception("Cannot compare currencies of different types");
			return a.value < b.value;
		}

		public static bool operator ==(CurrencyValue a, CurrencyValue b) {
			return a.currency == b.currency && a.value == b.value;
		}
		public static bool operator !=(CurrencyValue a, CurrencyValue b) {
			return a.currency != b.currency || a.value != b.value;
		}
		public override bool Equals(object obj) {
			if (obj is CurrencyValue) {
				CurrencyValue cv = (CurrencyValue)obj;
				return (cv.currency == this.currency && cv.value == this.value);
			}
			return false;
		}
		public override int GetHashCode() {
			return currency.GetHashCode() ^ value.GetHashCode();
		}

		public static CurrencyValue operator +(CurrencyValue a, float b) {
			return new CurrencyValue(a.currency, a.value + b);
		}

		public static CurrencyValue operator -(CurrencyValue a, float b) {
			return new CurrencyValue(a.currency, a.value - b);
		}

		public static CurrencyValue operator *(CurrencyValue a, float b) {
			return new CurrencyValue(a.currency, a.value * b);
		}

		public static CurrencyValue operator /(CurrencyValue a, float b) {
			return new CurrencyValue(a.currency, a.value / b);
		}

		public static bool operator >(CurrencyValue a, float b) {
			return a.value > b;
		}
		public static bool operator >=(CurrencyValue a, float b) {
			return a.value >= b;
		}
		public static bool operator <(CurrencyValue a, float b) {
			return a.value < b;
		}
		public static bool operator <=(CurrencyValue a, float b) {
			return a.value <= b;
		}

		#endregion
	}
}
