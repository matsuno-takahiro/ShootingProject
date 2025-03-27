using OneManEscapePlan.SpaceRailShooter.Scripts;
using UnityEngine;

/// <summary>
/// SumScoreとSumScoreManagerの使用例
/// </summary>
public class SumScoreExample : MonoBehaviour
{

    bool timed = false;

    /// <summary>
    /// ゲームオブジェクトからポイントを追加する例。
    /// </summary>
    /// <remarks>インスペクターのボタンから呼び出すことができます</remarks>
    /// <param name="points">追加するポイントの数（負の値で減算）</param>
	public void AddPoints(int points)
    {
        SumScore.Add(points);
    }

    /// <summary>
    /// ゲームオブジェクトからポイントを減算する例。
    /// </summary>
    /// <remarks>インスペクターのボタンから呼び出すことができます</remarks>
    /// <param name="points">スコアから減算するポイントの数</param>
    public void SubtractPoints(int points)
    {
        SumScore.Add(-points);
        // 注 - タイピングが好きならSumScore.Subtract(points)も使用できます
    }

    /// <summary>
    /// 時間に基づいてスコアを追加するかどうかを切り替えます
    /// </summary>
    /// <remarks>インスペクターのボタンから呼び出すことができます。</remarks>
    public void ToggleTimed()
    {
        timed = !timed;
    }

    /// <summary>スコアをゼロにリセットします</summary>
    /// <remarks>インスペクターのボタンから呼び出すことができます</remarks>
    public void ResetPoints()
    {
        SumScore.Reset();
    }

    /// <summary>現在のスコアがハイスコアより高い場合に保存します</summary>
    public void CheckHighScore()
    {
        if (SumScore.Score > SumScore.HighScore)
            SumScore.SaveHighScore();
    }

    /// <summary>ハイスコアをゼロにリセットします</summary>
    /// <remarks>インスペクターのボタンから呼び出すことができます</remarks>
    public void ClearHighScore()
    {
        SumScore.ClearHighScore();
    }

    public void AddGameTotalScore()
    {
        AddPoints(Game.TotalScore);

        // ハイスコアをチェックし、超過した場合に保存
        if (SumScore.Score > SumScore.HighScore)
        {
            SumScore.SaveHighScore();
        }

        // ハイスコアが超過した場合にPlayerPrefsに保存
        if (SumScore.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", SumScore.Score);
            PlayerPrefs.Save();
        }
    }


    void Update()
    {
        if (timed)
            // Time.deltaTimeを使用してポイントを安定して追加します。
            // この例では1秒あたり100ポイントを追加します
            SumScore.Add(Mathf.RoundToInt(Time.deltaTime * 100));
    }
}
