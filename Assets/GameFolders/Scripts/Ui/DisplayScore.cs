using RunnerGameInputAct.Manager;
using System.Collections;
using TMPro;
using UnityEngine;

namespace RunnerGameInputAct.Ui
{
    public class DisplayScore : MonoBehaviour
    {
        public static DisplayScore Instance { get; private set; }

        TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI highScoreText;
        float flashSpeed = 0.15f;
        public static IEnumerator blinkScoreCoroutine;

        public Color origColor;
        public Color Color2;
        private void Awake()
        {
            SingletonThisGameObject();
            scoreText = GetComponent<TextMeshProUGUI>();
            origColor = scoreText.color;

        }
        private void SingletonThisGameObject()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            GameManager.Instance.OnScoreChanged += HandleScoreChanged;
            //GameManager.Instance.OnHighScoreChanged += ScoreFlash; 
            GameManager.Instance.OnHighScoreChanged += HandleHighScoreChanged;
            GameManager.Instance.CheckHighScore();
        }

        private void HandleScoreChanged(int score = 0)
        {
            scoreText.text = "Score:" + score.ToString();

        }
        private void HandleHighScoreChanged(int highScore)
        {
            highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
            

        }

        private void OnDisable()
        {
            GameManager.Instance.OnScoreChanged -= HandleScoreChanged;
            GameManager.Instance.OnHighScoreChanged -= HandleHighScoreChanged;

        }
        public void ScoreFlash()
        {
            blinkScoreCoroutine = FlashingText();
            StartCoroutine(blinkScoreCoroutine);
           

        }
        public IEnumerator FlashingText()
        {
            
            for (int i = 0; i < 6; i++)
            {
                highScoreText.color = Color.yellow;
                yield return new WaitForSeconds(flashSpeed);
                highScoreText.color = origColor;
                yield return new WaitForSeconds(flashSpeed);
            }
        }


    }
}

