using OneManEscapePlan.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// An integer property that behaves somewhat like an <c>int</c> (by overloading many operators).
	/// 
	/// We don't overload all operators because this is a reference type and we don't want to be
	/// assigning values to it in expressions as if it was a value type. For example, say we
	/// overloaded * like this: 
	/// 
	///		public static IntProperty operator *(IntProperty a, int b) {
	///			return new IntProperty(a.value* b);
	///		 }
	///		 
	/// Then we did this in our code:
	/// 
	///		IntProperty ip = new IntProperty(5);
	///		ip.ValueChangedEvent.AddListener(onValueChanged);
	///		ip *= 3;
	/// 
	/// In this scenario we would have replaced `ip` with a _new instance_ of IntProperty. The value 
	/// of the old instance would not change, so the event would not be invoked, and future changes to
	/// `ip` would not invoke the event (since the new instance does not have a listener)
	/// 
	/// See <seealso cref="Property{T}"/>
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Operator overloading, Generics
	[System.Serializable]
	public class IntProperty : Property<int> {

#if !UNITY_2020_1_OR_NEWER
		//we replace the generic ValueEvent with an IntEvent because ValueEvent doesn't show in the Inspector in Unity versions before 2020
		[SerializeField] protected IntEvent valueChangedEvent = new IntEvent();
		override public UnityEvent<int> ValueChangedEvent {
			get {
				return valueChangedEvent;
			}
		}
#endif

		public IntProperty(int defaultValue) : base(defaultValue) {

		}

		public static int operator +(int a, IntProperty b) {
			return a + b.value;
		}

		public static int operator -(int a, IntProperty b) {
			return a - b.value;
		}

		public static int operator *(int a, IntProperty b) {
			return a * b.value;
		}

		public static int operator /(int a, IntProperty b) {
			return a / b.value;
		}

		public static bool operator >(IntProperty a, int b) {
			return a.value > b;
		}

		public static bool operator <(IntProperty a, int b) {
			return a.value < b;
		}

		public static bool operator >(int a, IntProperty b) {
			return a > b.value;
		}

		public static bool operator <(int a, IntProperty b) {
			return a < b.value;
		}

		public static bool operator >=(IntProperty a, int b) {
			return a.value >= b;
		}

		public static bool operator <=(IntProperty a, int b) {
			return a.value <= b;
		}

		public static bool operator >=(int a, IntProperty b) {
			return a >= b.value;
		}

		public static bool operator <=(int a, IntProperty b) {
			return a <= b.value;
		}

		public static implicit operator int(IntProperty a) {
			return a.Value;
		}

		/* THESE SHOULD NOT BE USED
		public static IntProperty operator +(IntProperty a, int b) {
			return new IntProperty(a.value + b);
		}
		public static IntProperty operator -(IntProperty a, int b) {
			return new IntProperty(a.value - b);
		}
		public static IntProperty operator *(IntProperty a, int b) {
			return new IntProperty(a.value * b);
		}
		public static IntProperty operator /(IntProperty a, int b) {
			return new IntProperty(a.value / b);
		}
		*/
	}
}
