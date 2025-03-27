using UnityEngine;
using DG.Tweening;

public class MoveAlongPath : MonoBehaviour
{
    // ユニットが通過する座標
    [SerializeField] private Vector3[] wayPoints;
    // 移動時間
    [SerializeField] private float moveTime;

    void Start()
    {
        // 生成したパスに沿って移動させる
        transform.DOPath(wayPoints, moveTime, PathType.Linear)
            // 等速移動
            .SetEase(Ease.Linear)
            // 撃破されたらアニメーションを停止
            .SetLink(gameObject);
    }
}
