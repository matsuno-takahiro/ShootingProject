using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetController : MonoBehaviour
{
    public Text displayText; // UI Textコンポーネントをアタッチする
    public Text alphabetText; // alphabetを表示するためのUI Textコンポーネント
    public float animationSpeed = 0.15f; // アニメーション速度（秒）
    public float moveAmountMultiplier = 1.0f; // 移動量の倍率
    public float blinkInterval = 0.5f; // 点滅間隔（秒）
    private char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '_' };
    private int currentIndex = 0;
    private float timer = 0f;
    private string alphabet = ""; // 文字列を格納する変数

    private void Start()
    {
        alphabet = ""; // ゲーム開始時にalphabetを空にする
        displayText.text = ""; // displayTextを空にする
        StartCoroutine(BlinkText());

        // alphabetTextを非表示にする
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

        // Fire1入力を検出して文字を格納し、displayTextを右に移動
        if (Input.GetButtonDown("Fire1"))
        {
            alphabet += letters[currentIndex];
            Debug.Log("Current Alphabet: " + alphabet);

            // displayTextを右に移動
            RectTransform rectTransform = displayText.GetComponent<RectTransform>();
            float moveAmount = displayText.fontSize * moveAmountMultiplier;
            rectTransform.anchoredPosition += new Vector2(moveAmount, 0);

            // alphabetTextにalphabetの内容を表示し、表示状態にする
            if (alphabetText != null)
            {
                alphabetText.text = alphabet;
                alphabetText.enabled = true;
            }
        }

        // Fire2入力を検出してalphabetの最後の文字を削除し、displayTextを左に移動
        if (Input.GetButtonDown("Fire2"))
        {
            if (alphabet.Length > 0)
            {
                alphabet = alphabet.Substring(0, alphabet.Length - 1);
                Debug.Log("Current Alphabet after deletion: " + alphabet);

                // alphabetTextにalphabetの内容を表示
                if (alphabetText != null)
                {
                    alphabetText.text = alphabet;
                }

                // displayTextを左に移動
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
