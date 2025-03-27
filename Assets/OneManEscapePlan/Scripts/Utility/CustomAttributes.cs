/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ColorName {
	Red, Green, Blue, Violet, Cyan, Magenta, Yellow, Orange, White, Gray, Black, PulsingRed, PulsingGreen, PulsingBlue, PulsingYellow
}

/// <summary>
/// The DrawConnectionsAttribute lets the GlobalGizmos class know that we want to draw connections
/// for the indicated field. Note that because Attributes cannot accept structs such as Color as
/// constructor arguments, we have to store colors as separate r, g, and b values.
/// </summary>
/// COMPLEXITY: Moderate
/// CONCEPTS: Attributes https://docs.unity3d.com/Manual/Attributes.html
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class DrawConnectionsAttribute : PropertyAttribute {
	private float r, g, b;

	public DrawConnectionsAttribute() {
		this.r = .5f;
		this.g = .5f;
		this.b = .5f;
	}

	public DrawConnectionsAttribute(float r, float g, float b) {
		this.r = r;
		this.g = g;
		this.b = b;
	}

	/// <summary>
	/// Attributes cannot accept structs (such as Unity's Color class) as constructor arguments, 
	/// so we have to use this hacky workaround to declare colors by name
	/// </summary>
	/// <param name="color"></param>
	public DrawConnectionsAttribute(ColorName color) {
		ApplyColor(color);
	}

	public void ApplyColor(ColorName name) {
		switch (name) {
			case ColorName.Red:
				b = g = 0f;
				r = 1f;
				break;
			case ColorName.Green:
				r = b = 0f;
				g = 1f;
				break;
			case ColorName.Blue:
				r = g = 0f;
				b = 1f;
				break;
			case ColorName.Violet:
				r = .5f;
				g = 0f;
				b = 1f;
				break;
			case ColorName.Cyan:
				r = Color.cyan.r;
				g = Color.cyan.g;
				b = Color.cyan.b;
				break;
			case ColorName.Magenta:
				r = Color.magenta.r;
				g = Color.magenta.g;
				b = Color.magenta.b;
				break;
			case ColorName.Yellow:
				r = 1;
				g = 1f;
				b = 0f;
				break;
			case ColorName.Orange:
				r = 1;
				g = .6f;
				b = 0f;
				break;
			case ColorName.White:
				r = g = b = 1;
				break;
			case ColorName.Gray:
				r = g = b = .5f;
				break;
			case ColorName.Black:
				r = g = b = 0;
				break;
			case ColorName.PulsingRed:
				r = -1;
				g = b = 0;
				break;
			case ColorName.PulsingGreen:
				g = -1;
				r = b = 0;
				break;
			case ColorName.PulsingBlue:
				b = -1;
				r = g = 0;
				break;
			case ColorName.PulsingYellow:
				b = 0;
				r = g = -1;
				break;
		}
	}

	public Color GetColor() {
		return new Color(R, G, B);
	}

	#region Properties
	public float R {
		get {
			if (r >= 0) return r;
			return Mathf.Abs(Mathf.Sin(DateTime.Now.Millisecond / 400f));
		}

		set {
			r = value;
		}
	}

	public float G {
		get {
			if (g >= 0) return g;
			return Mathf.Abs(Mathf.Sin(DateTime.Now.Millisecond / 400f));
		}

		set {
			g = value;
		}
	}

	public float B {
		get {
			if (b >= 0) return b;
			return Mathf.Abs(Mathf.Sin(DateTime.Now.Millisecond / 400f));
		}

		set {
			b = value;
		}
	}
	#endregion

}