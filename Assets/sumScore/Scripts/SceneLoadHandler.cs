using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using OneManEscapePlan.SpaceRailShooter.Scripts; // Gameクラスの名前空間をインポート

public class SceneLoadHandler : MonoBehaviour
{
    // インスペクターから設定可能なUnityEvent
    [SerializeField]
    private UnityEvent onSceneLoaded;

    void OnEnable()
    {
        // シーンロードイベントのリスナーを登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // シーンロードイベントのリスナーを解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーンがロードされたときに呼び出されるメソッド
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ここにシーンロード時の処理を記述
        Debug.Log("シーンがロードされました: " + scene.name);

        // TotalScoreを初期化
        Game.ResetTotalScore();

        // UnityEventを呼び出す
        if (onSceneLoaded != null)
        {
            onSceneLoaded.Invoke();
        }
    }
}
