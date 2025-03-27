using UnityEngine;
using DG.Tweening;
using UnityEngine.Splines; // 追加

public class SplineMover : MonoBehaviour
{
    // ボスユニットが通過するスプライン
    [SerializeField] private SplineContainer splineA;
    [SerializeField] private SplineContainer splineB;
    [SerializeField] private SplineContainer splineC; // 追加

    // 移動時間
    [SerializeField] private float moveTime;

    private Sequence sequence1;
    private Sequence sequence2;
    private Sequence sequence3; // 追加

    void Start()
    {
        sequence1 = DOTween.Sequence()
            // スプラインに沿って移動
            .Append(transform.DOPath(GetSplinePositions(splineA), moveTime, PathType.Linear).SetOptions(true))
            // スプラインを１周したら少し待機
            .AppendInterval(0.5f)
            // 無限ループ
            .SetLoops(-1)
            // 自動で破棄しない
            .SetAutoKill(false)
            // 自動で再生しない
            .Pause()
            // オブジェクトが削除されたタイミングで破棄
            .SetLink(gameObject);

        sequence2 = DOTween.Sequence()
            // スプラインに沿って移動
            .Append(transform.DOPath(GetSplinePositions(splineB), moveTime, PathType.Linear).SetOptions(true))
            // スプラインを１周したら少し待機
            .AppendInterval(0.5f)
            // 無限ループ
            .SetLoops(-1)
            // 自動で破棄しない
            .SetAutoKill(false)
            // 自動で再生しない
            .Pause()
            // オブジェクトが削除されたタイミングで破棄
            .SetLink(gameObject);

        sequence3 = DOTween.Sequence() // 追加
                                       // スプラインに沿って移動
            .Append(transform.DOPath(GetSplinePositions(splineC), moveTime, PathType.Linear).SetOptions(true))
            // スプラインを１周したら少し待機
            .AppendInterval(0.5f)
            // 無限ループ
            .SetLoops(-1)
            // 自動で破棄しない
            .SetAutoKill(false)
            // 自動で再生しない
            .Pause()
            // オブジェクトが削除されたタイミングで破棄
            .SetLink(gameObject);
    }

    private Vector3[] GetSplinePositions(SplineContainer splineContainer)
    {
        int pointCount = splineContainer.Spline.Count;
        Vector3[] positions = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            positions[i] = splineContainer.Spline.EvaluatePosition(i / (float)(pointCount - 1));
        }
        return positions;
    }

    public void MovePath1()
    {
        // 別のスプラインを停止
        sequence2.Pause();
        sequence3.Pause(); // 追加

        var path1 = DOTween.Sequence();

        // スプラインのスタート地点に移動
        path1.Append(transform.DOMove(GetSplinePositions(splineA)[0], 1.0f))
            // アニメーションを再生
            .AppendCallback(() => sequence1.Restart())
            .SetLink(gameObject);
    }

    public void MovePath2()
    {
        // 別のスプラインを停止
        sequence1.Pause();
        sequence3.Pause(); // 追加

        var path2 = DOTween.Sequence();

        // スプラインのスタート地点に移動
        path2.Append(transform.DOMove(GetSplinePositions(splineB)[0], 1.0f))
            // アニメーションを再生
            .AppendCallback(() => sequence2.Restart())
            .SetLink(gameObject);
    }

    public void MovePath3() // 追加
    {
        // 別のスプラインを停止
        sequence1.Pause();
        sequence2.Pause();

        var path3 = DOTween.Sequence();

        // スプラインのスタート地点に移動
        path3.Append(transform.DOMove(GetSplinePositions(splineC)[0], 1.0f))
            // アニメーションを再生
            .AppendCallback(() => sequence3.Restart())
            .SetLink(gameObject);
    }
}
