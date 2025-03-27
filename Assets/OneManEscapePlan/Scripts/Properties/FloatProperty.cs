using OneManEscapePlan.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// A float property that behaves somewhat like an <c>float</c> (by overloading many operators).
	/// 
	/// We don't overload all operators because this is a reference type and we don't want to be
	/// assigning values to it in expressions as if it was a value type. For example, say we
	/// overloaded * like this: 
	/// 
	///		public static FloatProperty operator *(FloatProperty a, float b) {
	///			return new FloatProperty(a.value* b);
	///		 }
	///		 
	/// Then we did this in our code:
	/// 
	///		FloatProperty ip = new FloatProperty(5f);
	///		ip.ValueChangedEvent.AddListener(onValueChanged);
	///		ip *= 3f;
	/// 
	/// In this scenario we would have replaced `ip` with a _new instance_ of FloatProperty. The value 
	/// of the old instance would not change, so the event would not be invoked, and future changes to
	/// `ip` would not invoke the event (since the new instance does not have a listener)
	/// 
	/// See <seealso cref="Property{T}"/>
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Operator overloading, Generics
	[System.Serializable]
	public class FloatProperty : Property<float> {

#if !UNITY_2020_1_OR_NEWER
		//we replace the generic ValueEvent with a FloatEvent because ValueEvent doesn't show in the Inspector in Unity versions before 2020
		[SerializeField] protected FloatEvent valueChangedEvent = new FloatEvent();
		override public UnityEvent<float> ValueChangedEvent {
			get {
				return valueChangedEvent;
			}
		}
#endif

		public FloatProperty(float defaultValue) : base(defaultValue) {

		}

		public static float operator +(float a, FloatProperty b) {
			return a + b.value;
		}

		public static float operator -(float a, FloatProperty b) {
			return a - b.value;
		}

		public static float operator *(float a, FloatProperty b) {
			return a * b.value;
		}

		public static float operator /(float a, FloatProperty b) {
			return a / b.value;
		}

		public static bool operator >(FloatProperty a, float b) {
			return a.value > b;
		}

		public static bool operator <(FloatProperty a, float b) {
			return a.value < b;
		}

		public static bool operator >(float a, FloatProperty b) {
			return a > b.value;
		}

		public static bool operator <(float a, FloatProperty b) {
			return a < b.value;
		}

		public static bool operator >=(FloatProperty a, float b) {
			return a.value >= b;
		}

		public static bool operator <=(FloatProperty a, float b) {
			return a.value <= b;
		}

		public static bool operator >=(float a, FloatProperty b) {
			return a >= b.value;
		}

		public static bool operator <=(float a, FloatProperty b) {
			return a <= b.value;
		}

		public static implicit operator float(FloatProperty a) {
			return a.Value;
		}

		/* THESE SHOULD NOT BE USED
		public static floatProperty operator +(floatProperty a, float b) {
			return new floatProperty(a.value + b);
		}
		public static floatProperty operator -(floatProperty a, float b) {
			return new floatProperty(a.value - b);
		}
		public static floatProperty operator *(floatProperty a, float b) {
			return new floatProperty(a.value * b);
		}
		public static floatProperty operator /(floatProperty a, float b) {
			return new floatProperty(a.value / b);
		}
		*/
	}
}
