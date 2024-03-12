using RunnerGameInputAct.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunnerGameInputAct.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int score;
        [SerializeField] int speed;
        [SerializeField] int coin;
        [SerializeField] int highScore;
        [SerializeField] float delayLevelTime = 1f;

        public static GameManager Instance { get; private set; }
        public event System.Action<bool> OnSceneChanged;
        public event System.Action<int> OnScoreChanged;
        public event System.Action<int> OnCoinChanged;
        public event System.Action<int> OnHighScoreChanged;
        private void Awake()
        {
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void LoadScene(int playScene = 0)
        {
            StartCoroutine(LoadSceneAsync(playScene));
        }


        private IEnumerator LoadSceneAsync(int playScene)
        {
            yield return new WaitForSeconds(delayLevelTime);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            yield return SceneManager.UnloadSceneAsync(buildIndex);//Þuan sadece MenuScene unload yapmak için gerekli
            SceneManager.LoadSceneAsync(buildIndex + playScene, LoadSceneMode.Additive).completed += (AsyncOperation obj) =>
            {                                          //mevcut sahneyi yok etmeden diðer sahneyi yükler      
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex + playScene));
                //Activescenemizi belirliyoruz
            };
            OnSceneChanged?.Invoke(false);
        }
        public void ExitGame()
        {
            Debug.Log("ExitGame");
            Application.Quit();
        }
        public void LoadMenuAndUi(float delayLoadingTime)
        {
            StartCoroutine(LoadMenuAndUiAsync(delayLoadingTime));
        }
        IEnumerator LoadMenuAndUiAsync(float delayLoadingTime)
        {
            yield return new WaitForSeconds(delayLoadingTime);
            yield return SceneManager.LoadSceneAsync("Menu");
            yield return SceneManager.LoadSceneAsync("Ui", LoadSceneMode.Additive);

            OnSceneChanged?.Invoke(true);
           

        }
        public void IncreaseScore(int score = 0)
        {
            this.score += score;
            if (this.score > highScore)
            {
                highScore = this.score;
                OnHighScoreChanged?.Invoke(highScore);
                PlayerPrefs.SetInt("HighScore", this.score);

            }

            OnScoreChanged?.Invoke(this.score);

        }
        public void ResetScore() 
        { 
            score= 0;
            //OnScoreChanged?.Invoke(score);
        }
        public void IncreaseCoin(int coin=0)
        {
           

            int totalCoin = PlayerPrefs.GetInt("Coin", 0); 
            totalCoin += coin;
            PlayerPrefs.SetInt("Coin", totalCoin); 
            OnCoinChanged?.Invoke(totalCoin); 

        }
        public void CheckHighScore()
        {
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            OnHighScoreChanged?.Invoke(PlayerPrefs.GetInt("HighScore", 0));

        }

    }
}

