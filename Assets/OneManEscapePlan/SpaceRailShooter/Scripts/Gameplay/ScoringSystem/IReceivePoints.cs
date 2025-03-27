/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem {
	/// <summary>
	/// Interface for something that receives points
	/// </summary>
	public interface IReceivePoints {
		void AddPoints(int points);
	}
}