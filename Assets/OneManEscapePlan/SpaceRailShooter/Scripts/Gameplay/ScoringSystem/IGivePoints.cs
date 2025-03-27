/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem {
	/// <summary>
	/// Interface for something that gives points
	/// </summary>
	public interface IGivePoints {
		void GivePoints(IReceivePoints receiver);
		int Points { get; set; }
	}
}