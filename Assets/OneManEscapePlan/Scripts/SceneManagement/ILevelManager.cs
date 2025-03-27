/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

namespace OneManEscapePlan.Scripts.SceneManagement {
	public interface ILevelManager {
		int CurrentLevelIndex { get; }
		LevelSequence LevelSequence { get; set; }

		void GoToLevel(int index, bool async = true);
		bool NextLevel(bool async = true);
		bool PreviousLevel(bool async = true);
		void StartFirstLevel(bool async = true);
	}
}