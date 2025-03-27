using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphabetAnimation : MonoBehaviour
{
    public Text displayText; // UI Text�R���|�[�l���g���A�^�b�`����
    public float displayDuration = 0.5f; // �e�����̕\������

    private void Start()
    {
        StartCoroutine(DisplayAlphabet());
    }

    private IEnumerator DisplayAlphabet()
    {
        while (true)
        {
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                displayText.text = letter.ToString();
                yield return new WaitForSeconds(displayDuration);
            }
        }
    }
}
