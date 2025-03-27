using UnityEngine;
using TMPro;
using DG.Tweening;

public class AlphabetDisplay : MonoBehaviour
{
    public TextMeshProUGUI alphabetText;
    private int currentIndex = 0;
    private Tween tweener;
    public float animationDuration = 0.5f; // アニメーションの遅延時間を変更可能にする

    void Start()
    {
        DisplayCurrentLetter();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        if (input != 0)
        {
            if (tweener != null && tweener.IsActive())
            {
                tweener.Kill();
            }

            int targetIndex = currentIndex + (int)Mathf.Sign(input);
            if (targetIndex < 0)
            {
                targetIndex = 25;
            }
            else if (targetIndex >= 26)
            {
                targetIndex = 0;
            }

            tweener = DOTween.To(() => currentIndex, x => currentIndex = x, targetIndex, animationDuration)
                .OnUpdate(() => DisplayCurrentLetter())
                .OnComplete(() => currentIndex = targetIndex);
        }
    }

    void DisplayCurrentLetter()
    {
        char letter = (char)('A' + currentIndex);
        alphabetText.text = letter.ToString();
    }
}
