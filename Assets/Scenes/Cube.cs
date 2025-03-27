using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // 「Complete!!」テキストのGameObject
    [SerializeField] private GameObject _completeObject;

    // Animatorコンポーネントへの参照
    private Animator _animator;

    // アニメーションパラメータの名前をインスペクターから設定できるようにする
    [SerializeField] private string _animationParameterName;

    void Start()
    {
        // Animatorコンポーネントを取得
        _animator = GetComponent<Animator>();

        // 初期化処理などがあればここに記述
    }

    // z方向に3移動するメソッド
    public void MoveInZDirection()
    {
        transform.DOLocalMoveZ(3f, 1f)
            .OnComplete(MyCompleteFunction);

        // アニメーションパラメータを設定してアニメーションを再生
        if (_animator != null && !string.IsNullOrEmpty(_animationParameterName))
        {
            _animator.SetBool(_animationParameterName, true);
        }
    }

    // 「Complete!!」テキストを表示するメソッド
    private void MyCompleteFunction()
    {
        _completeObject.SetActive(true);

        // アニメーションパラメータをリセット
        if (_animator != null && !string.IsNullOrEmpty(_animationParameterName))
        {
            _animator.SetBool(_animationParameterName, false);
        }
    }
}
