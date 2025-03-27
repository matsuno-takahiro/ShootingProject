using UnityEngine;
using UnityEngine.UI;

/// <summary>インスペクターからアクセス可能なSumScoreのマネージャー</summary>
/// <remarks>
/// シーン内のゲームオブジェクトにアタッチしてください。
/// これはシングルトンなので、アクティブなインスタンスは1つだけです。
/// </remarks>
public class SumScoreManager : MonoBehaviour
{

    public static SumScoreManager instance = null;  // シングルトンのための静的インスタンス

    public int initialScore = 0;
    public bool storeHighScore = true, allowNegative = true;
    public Text field; // 現在のスコアを表示するテキストフィールド
    public Text highScoreField; // ハイスコアを表示するテキストフィールド

    void Awake()
    {
        // 実行中のインスタンスが1つだけであることを確認
        if (instance == null)
            instance = this; // インスタンスをこのオブジェクトに設定
        else
            Destroy(gameObject); // 自分自身を破壊
        // リンクされた参照が失われていないことを確認
        if (field == null)
            Debug.LogError("<b>SumScoreManager</b>コンポーネントの'field'への参照がありません");
        if (storeHighScore && highScoreField == null)
            Debug.LogError("<b>SumScoreManager</b>コンポーネントの'highScoreField'への参照がありません");
    }

    void Start()
    {
        SumScore.Reset(); // オブジェクトがロードされたときにスコアが0であることを確認
        if (initialScore != 0)
            SumScore.Add(initialScore);  // 初期スコアを設定
        if (storeHighScore)
        {
            if (PlayerPrefs.HasKey("sumHS"))
            {
                // ハイスコアの値を設定し、マネージャーに通知
                SumScore.HighScore = PlayerPrefs.GetInt("sumHS");
                UpdatedHS();
            }
            else
                SumScore.HighScore = 0;
        }

        Updated(); // UIに初期スコアを設定
    }

    /// <summary>スコアの変更をこのマネージャーに通知します</summary>
    public void Updated()
    {
        field.text = SumScore.Score.ToString("0"); // 新しいスコアをテキストフィールドに表示
    }

    /// <summary>ハイスコアの変更をこのマネージャーに通知します</summary>
    public void UpdatedHS()
    {
        if (storeHighScore)
            highScoreField.text = SumScore.HighScore.ToString("0"); // 新しいハイスコアをテキストフィールドに表示
    }


}
