/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A bunch of different UnityEvent classes for commonly used data types and structures
/// </summary>
/// COMPLEXITY: Moderate
/// CONCEPTS: Inheritance, Generics
namespace OneManEscapePlan.Scripts.Events {
	[Serializable]
	public class IntEvent : UnityEvent<int> { }
	[Serializable]
	public class UintEvent : UnityEvent<uint> { }
	[Serializable]
	public class ShortEvent : UnityEvent<short> { }
	[Serializable]
	public class LongEvent : UnityEvent<long> { }
	[Serializable]
	public class FloatEvent : UnityEvent<float> { }
	[Serializable]
	public class DoubleEvent : UnityEvent<double> { }
	[Serializable]
	public class ByteEvent : UnityEvent<byte> { }
	[Serializable]
	public class StringEvent : UnityEvent<string> { }

	[Serializable]
	public class GameObjectEvent : UnityEvent<GameObject> { }
	[Serializable]
	public class TransformEvent : UnityEvent<Transform> { }
	[Serializable]
	public class RigidbodyEvent : UnityEvent<Transform> { }
	[Serializable]
	public class ColliderEvent : UnityEvent<Collider> { }
	[Serializable]
	public class Collider2DEvent : UnityEvent<Collider2D> { }
	[Serializable]
	public class Vector2Event : UnityEvent<Vector2> { }
	[Serializable]
	public class Vector3Event : UnityEvent<Vector3> { }
	[Serializable]
	public class QuaternionEvent : UnityEvent<Quaternion> { }
}
