/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	/// <summary>
	/// An interface for objects that accept horizontal and vertical input
	/// </summary>
	public interface IPlayerMovement {
		float HorizontalInput { get; set; }
		float VerticalInput { get; set; }
	}
}