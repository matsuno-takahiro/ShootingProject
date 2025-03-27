using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // �uComplete!!�v�e�L�X�g��GameObject
    [SerializeField] private GameObject _completeObject;

    // Animator�R���|�[�l���g�ւ̎Q��
    private Animator _animator;

    // �A�j���[�V�����p�����[�^�̖��O���C���X�y�N�^�[����ݒ�ł���悤�ɂ���
    [SerializeField] private string _animationParameterName;

    void Start()
    {
        // Animator�R���|�[�l���g���擾
        _animator = GetComponent<Animator>();

        // �����������Ȃǂ�����΂����ɋL�q
    }

    // z������3�ړ����郁�\�b�h
    public void MoveInZDirection()
    {
        transform.DOLocalMoveZ(3f, 1f)
            .OnComplete(MyCompleteFunction);

        // �A�j���[�V�����p�����[�^��ݒ肵�ăA�j���[�V�������Đ�
        if (_animator != null && !string.IsNullOrEmpty(_animationParameterName))
        {
            _animator.SetBool(_animationParameterName, true);
        }
    }

    // �uComplete!!�v�e�L�X�g��\�����郁�\�b�h
    private void MyCompleteFunction()
    {
        _completeObject.SetActive(true);

        // �A�j���[�V�����p�����[�^�����Z�b�g
        if (_animator != null && !string.IsNullOrEmpty(_animationParameterName))
        {
            _animator.SetBool(_animationParameterName, false);
        }
    }
}
