using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Properties {

	/// <summary>
	/// Property<> wraps a type (presumably a value type, such as a primitive or struct), and
	/// provides a UnityEvent for tracking when changes are made to the value. This can save
	/// us a lot of boiler-plate in classes which have value fields we want to expose events for 
	/// in the inspector.
	/// 
	/// For example, instead of doing all of this:
	/// 
	///		[SerializeField] protected int score;
	///		[SerializeField] protected IntEvent scoreChangedEvent = new IntEvent();
	///		public IntEvent ScoreChangedEvent { get { return scoreChangedEvent; } }
	/// 
	///		public int Score {
	///			get { return score; }
	///			set {
	///				score = value;
	///				scoreChangedEvent.Invoke(score);
	///			}
	///		}
	///		
	/// we can just do this:
	/// 
	///		[SerializeField] protected IntProperty score = new IntProperty();
	///		public IntProperty Score { get { return score; } }
	/// 
	/// </summary>
	/// COMPLEXITY: Advanced
	/// CONCEPTS: Generics, Operator overloading
	[Serializable]
	public class Property<T> {

		[Serializable]
		public class ValueEvent : UnityEvent<T> { }

		[SerializeField] protected T value;
		[SerializeField] private ValueEvent valueChangedEvent = new ValueEvent();
		/// <summary>
		/// Invoked when this property's Value changes.
		/// NOTE: ValueEvent won't appear in the inspector, so we may wish to override this
		/// with a concrete type (e.g. IntEvent)
		/// </summary>
		virtual public UnityEvent<T> ValueChangedEvent { get { return valueChangedEvent; } }

		public Property() {
			this.value = default(T);
			ValueChangedEvent.Invoke(value);
		}

		public Property(T value) {
			this.value = value;
			ValueChangedEvent.Invoke(value);
		}

		/// <summary>
		/// The value of this Property. When setting the value, the ValueChangedEvent will be invoked.
		/// </summary>
		virtual public T Value {
			get {
				return value;
			}
			set {
				this.value = value;
				ValueChangedEvent.Invoke(value);
			}
		}

		/// <summary>
		/// We consider a Property<T> equal to a T if the Property's Value is equal to the T.
		/// For example, <c>new Property<int>(5) == 5</c> is <c>true</c>
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			if (obj is T) return ((T)obj).Equals(value);
			if (obj is Property<T>) return ((Property<T>)obj).value.Equals(value);
			throw new ArgumentException("Property<" + typeof(T) + "> cannot be compared with " + obj.GetType().Name);
		}

		/// <summary>
		/// I _think_ this solution is fine?
		/// </summary>
		public override int GetHashCode() {
			return value.GetHashCode();
		}

		public override string ToString() {
			return "{Property:" + value.ToString() + "}";
		}
	}
}
