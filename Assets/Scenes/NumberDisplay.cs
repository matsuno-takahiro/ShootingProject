using UnityEngine;

using TMPro; // TextMeshProの名前空間をインポート
using DG.Tweening;

public class NumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI numberText; // TextMeshProUGUIに変更
    private float currentValue = 0f;
    private Tweener tweener;

    void Start()
    {
        // 初期値を設定
        numberText.text = ConvertToAlphabet(currentValue);
    }

    void Update()
    {
        // Horizontal入力を取得
        float input = Input.GetAxis("Horizontal");

        // 入力がある場合にアニメーションを更新
        if (input != 0)
        {
            float targetValue = currentValue + input;

            // targetValueが25を超えた場合、5にリセット
            if (targetValue >= 26)
            {
                targetValue = 0;
            }

            // 既存のアニメーションを停止
            if (tweener != null && tweener.IsActive())
            {
                tweener.Kill();
            }

            // 新しいアニメーションを開始
            tweener = DOTween.To(() => currentValue, x =>
            {
                currentValue = x;
                numberText.text = ConvertToAlphabet(currentValue);
            }, targetValue, 0.15f).SetEase(Ease.Linear);
        }
    }

    private string ConvertToAlphabet(float value)
    {
        int intValue = Mathf.RoundToInt(value);
        return ((char)('A' + intValue)).ToString();
    }
}
