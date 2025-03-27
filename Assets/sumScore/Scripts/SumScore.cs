using UnityEngine;

/// <summary>
/// シンプルなスコアマネージャー。シーン内のゲームオブジェクトにSumScoreManagerがアタッチされている必要があります。
/// </summary>
public class SumScore
{

    public static int Score { get; protected set; }
    public static int HighScore { get; set; }

    private static SumScoreManager mgr; // マネージャーインスタンスへの簡単な参照

    // プライベートコンストラクタで唯一のコピーが存在することを保証
    private SumScore() { }

    /// <summary>合計スコアにポイントを追加します</summary>
    /// <remarks>
    /// Subtractメソッドのショートカットとして負の数を使用することもできます
    /// </remarks>
    /// <param name="pointsToAdd">追加するポイント数</param>
    public static void Add(int pointsToAdd)
    {
        Debug.Log(pointsToAdd + " ポイント " + ((pointsToAdd > 0) ? "追加" : "削除"));
        Score += pointsToAdd; // 現在のスコアにポイントを追加
        if (MgrSet())
        {
            // 負の値にならないように確認
            if (Score < 0 && !mgr.allowNegative)
                Score = 0; // スコアを0にリセット
            mgr.Updated(); // スコアが変更されたことをマネージャーに通知
        }
    }

    /// <summary>合計スコアからポイントを削除します</summary>
    /// <param name="pointsToSubtract">削除するポイント数</param>
    public static void Subtract(int pointsToSubtract)
    {
        Add(-pointsToSubtract);
    }

    /// <summary>スコアを0に設定し、マネージャーを更新します</summary>
    public static void Reset()
    {
        Debug.Log("スコアをリセット");
        Score = 0;
        if (MgrSet())
        {
            mgr.Updated();
        }
    }

    /// <summary>スクリプトに必要な参照を確認および設定します</summary>
    /// <returns>成功した場合はtrue、失敗した場合はfalse</returns>
    static bool MgrSet()
    {
        if (mgr == null)
        {
            mgr = SumScoreManager.instance; // インスタンス参照を設定
            if (mgr == null)
            {
                // リンクできない場合はエラーメッセージを表示
                Debug.LogError("<b>SumScoreManager.instance</b>が見つかりません。オブジェクトがインスペクターでアクティブになっていることを確認してください。");
                return false;
            }
        }
        return true;
    }

    /// <summary>スコアをハイスコアと比較し、より高い場合は保存します</summary>
    public static void SaveHighScore()
    {
        if (Score > HighScore)
        {
            Debug.Log("新しいハイスコア " + Score);
            HighScore = Score;
            PlayerPrefs.SetInt("sumHS", Score); // ハイスコアをプレイヤープレフに保存
            if (MgrSet())
                mgr.UpdatedHS(); // 変更をマネージャーに通知
        }
    }

    /// <summary>ハイスコアをリセットし、プレイヤープレフから削除します</summary>
    public static void ClearHighScore()
    {
        Debug.Log("ハイスコアを削除");
        PlayerPrefs.DeleteKey("sumHS");
        HighScore = 0;
        if (MgrSet())
            mgr.UpdatedHS(); // 変更をマネージャーに通知
    }

}
