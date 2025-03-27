using UnityEngine;

using TMPro; // TextMeshPro�̖��O��Ԃ��C���|�[�g
using DG.Tweening;

public class NumberDisplay : MonoBehaviour
{
    public TextMeshProUGUI numberText; // TextMeshProUGUI�ɕύX
    private float currentValue = 0f;
    private Tweener tweener;

    void Start()
    {
        // �����l��ݒ�
        numberText.text = ConvertToAlphabet(currentValue);
    }

    void Update()
    {
        // Horizontal���͂��擾
        float input = Input.GetAxis("Horizontal");

        // ���͂�����ꍇ�ɃA�j���[�V�������X�V
        if (input != 0)
        {
            float targetValue = currentValue + input;

            // targetValue��25�𒴂����ꍇ�A5�Ƀ��Z�b�g
            if (targetValue >= 26)
            {
                targetValue = 0;
            }

            // �����̃A�j���[�V�������~
            if (tweener != null && tweener.IsActive())
            {
                tweener.Kill();
            }

            // �V�����A�j���[�V�������J�n
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
