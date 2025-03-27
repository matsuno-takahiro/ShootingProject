using UnityEngine;
using DG.Tweening;
using UnityEngine.Splines; // �ǉ�

public class SplineMover : MonoBehaviour
{
    // �{�X���j�b�g���ʉ߂���X�v���C��
    [SerializeField] private SplineContainer splineA;
    [SerializeField] private SplineContainer splineB;
    [SerializeField] private SplineContainer splineC; // �ǉ�

    // �ړ�����
    [SerializeField] private float moveTime;

    private Sequence sequence1;
    private Sequence sequence2;
    private Sequence sequence3; // �ǉ�

    void Start()
    {
        sequence1 = DOTween.Sequence()
            // �X�v���C���ɉ����Ĉړ�
            .Append(transform.DOPath(GetSplinePositions(splineA), moveTime, PathType.Linear).SetOptions(true))
            // �X�v���C�����P�������班���ҋ@
            .AppendInterval(0.5f)
            // �������[�v
            .SetLoops(-1)
            // �����Ŕj�����Ȃ�
            .SetAutoKill(false)
            // �����ōĐ����Ȃ�
            .Pause()
            // �I�u�W�F�N�g���폜���ꂽ�^�C�~���O�Ŕj��
            .SetLink(gameObject);

        sequence2 = DOTween.Sequence()
            // �X�v���C���ɉ����Ĉړ�
            .Append(transform.DOPath(GetSplinePositions(splineB), moveTime, PathType.Linear).SetOptions(true))
            // �X�v���C�����P�������班���ҋ@
            .AppendInterval(0.5f)
            // �������[�v
            .SetLoops(-1)
            // �����Ŕj�����Ȃ�
            .SetAutoKill(false)
            // �����ōĐ����Ȃ�
            .Pause()
            // �I�u�W�F�N�g���폜���ꂽ�^�C�~���O�Ŕj��
            .SetLink(gameObject);

        sequence3 = DOTween.Sequence() // �ǉ�
                                       // �X�v���C���ɉ����Ĉړ�
            .Append(transform.DOPath(GetSplinePositions(splineC), moveTime, PathType.Linear).SetOptions(true))
            // �X�v���C�����P�������班���ҋ@
            .AppendInterval(0.5f)
            // �������[�v
            .SetLoops(-1)
            // �����Ŕj�����Ȃ�
            .SetAutoKill(false)
            // �����ōĐ����Ȃ�
            .Pause()
            // �I�u�W�F�N�g���폜���ꂽ�^�C�~���O�Ŕj��
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
        // �ʂ̃X�v���C�����~
        sequence2.Pause();
        sequence3.Pause(); // �ǉ�

        var path1 = DOTween.Sequence();

        // �X�v���C���̃X�^�[�g�n�_�Ɉړ�
        path1.Append(transform.DOMove(GetSplinePositions(splineA)[0], 1.0f))
            // �A�j���[�V�������Đ�
            .AppendCallback(() => sequence1.Restart())
            .SetLink(gameObject);
    }

    public void MovePath2()
    {
        // �ʂ̃X�v���C�����~
        sequence1.Pause();
        sequence3.Pause(); // �ǉ�

        var path2 = DOTween.Sequence();

        // �X�v���C���̃X�^�[�g�n�_�Ɉړ�
        path2.Append(transform.DOMove(GetSplinePositions(splineB)[0], 1.0f))
            // �A�j���[�V�������Đ�
            .AppendCallback(() => sequence2.Restart())
            .SetLink(gameObject);
    }

    public void MovePath3() // �ǉ�
    {
        // �ʂ̃X�v���C�����~
        sequence1.Pause();
        sequence2.Pause();

        var path3 = DOTween.Sequence();

        // �X�v���C���̃X�^�[�g�n�_�Ɉړ�
        path3.Append(transform.DOMove(GetSplinePositions(splineC)[0], 1.0f))
            // �A�j���[�V�������Đ�
            .AppendCallback(() => sequence3.Restart())
            .SetLink(gameObject);
    }
}
