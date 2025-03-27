using UnityEngine;
using DG.Tweening;

public class MoveAlongPath : MonoBehaviour
{
    // ���j�b�g���ʉ߂�����W
    [SerializeField] private Vector3[] wayPoints;
    // �ړ�����
    [SerializeField] private float moveTime;

    void Start()
    {
        // ���������p�X�ɉ����Ĉړ�������
        transform.DOPath(wayPoints, moveTime, PathType.Linear)
            // �����ړ�
            .SetEase(Ease.Linear)
            // ���j���ꂽ��A�j���[�V�������~
            .SetLink(gameObject);
    }
}
