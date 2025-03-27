using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadHandler : MonoBehaviour
{
    // �C���X�y�N�^�[����ݒ�\��UnityEvent
    [SerializeField]
    private UnityEvent onSceneLoaded;

    void OnEnable()
    {
        // �V�[�����[�h�C�x���g�̃��X�i�[��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // �V�[�����[�h�C�x���g�̃��X�i�[������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �V�[�������[�h���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �����ɃV�[�����[�h���̏������L�q
        Debug.Log("�V�[�������[�h����܂���: " + scene.name);

        // UnityEvent���Ăяo��
        if (onSceneLoaded != null)
        {
            onSceneLoaded.Invoke();
        }
    }
}
