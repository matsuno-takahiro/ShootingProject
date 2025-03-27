using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetController : MonoBehaviour
{
    public Text displayText; // UI Text�R���|�[�l���g���A�^�b�`����
    public Text alphabetText; // alphabet��\�����邽�߂�UI Text�R���|�[�l���g
    public float animationSpeed = 0.15f; // �A�j���[�V�������x�i�b�j
    public float moveAmountMultiplier = 1.0f; // �ړ��ʂ̔{��
    public float blinkInterval = 0.5f; // �_�ŊԊu�i�b�j
    private char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '_' };
    private int currentIndex = 0;
    private float timer = 0f;
    private string alphabet = ""; // ��������i�[����ϐ�

    private void Start()
    {
        alphabet = ""; // �Q�[���J�n����alphabet����ɂ���
        displayText.text = ""; // displayText����ɂ���
        StartCoroutine(BlinkText());

        // alphabetText���\���ɂ���
        if (alphabetText != null)
        {
            alphabetText.enabled = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput > 0)
            {
                IncrementLetter();
                timer = 0f;
            }
            else if (horizontalInput < 0)
            {
                DecrementLetter();
                timer = 0f;
            }

            displayText.text = letters[currentIndex].ToString();
        }

        // Fire1���͂����o���ĕ������i�[���AdisplayText���E�Ɉړ�
        if (Input.GetButtonDown("Fire1"))
        {
            alphabet += letters[currentIndex];
            Debug.Log("Current Alphabet: " + alphabet);

            // displayText���E�Ɉړ�
            RectTransform rectTransform = displayText.GetComponent<RectTransform>();
            float moveAmount = displayText.fontSize * moveAmountMultiplier;
            rectTransform.anchoredPosition += new Vector2(moveAmount, 0);

            // alphabetText��alphabet�̓��e��\�����A�\����Ԃɂ���
            if (alphabetText != null)
            {
                alphabetText.text = alphabet;
                alphabetText.enabled = true;
            }
        }

        // Fire2���͂����o����alphabet�̍Ō�̕������폜���AdisplayText�����Ɉړ�
        if (Input.GetButtonDown("Fire2"))
        {
            if (alphabet.Length > 0)
            {
                alphabet = alphabet.Substring(0, alphabet.Length - 1);
                Debug.Log("Current Alphabet after deletion: " + alphabet);

                // alphabetText��alphabet�̓��e��\��
                if (alphabetText != null)
                {
                    alphabetText.text = alphabet;
                }

                // displayText�����Ɉړ�
                RectTransform rectTransform = displayText.GetComponent<RectTransform>();
                float moveAmount = displayText.fontSize * moveAmountMultiplier;
                rectTransform.anchoredPosition += new Vector2(-moveAmount, 0);
            }
        }
    }

    private void IncrementLetter()
    {
        currentIndex = (currentIndex + 1) % letters.Length;
    }

    private void DecrementLetter()
    {
        currentIndex = (currentIndex - 1 + letters.Length) % letters.Length;
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            displayText.enabled = !displayText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
