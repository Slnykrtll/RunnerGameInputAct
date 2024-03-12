using RunnerGameInputAct.Manager;
using RunnerGameInputAct.Player;
//using RunnerGameInputAct.Tile;
//using RunnerGameInputAct.Ui;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using Unity.VisualScripting;
using UnityEngine;
namespace RunnerGameInputAct.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        public static ScoreController Instance;
     
        [SerializeField] int score = 10;
        [SerializeField] int coin = 10;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
        }
        

        public void ActiveCoin()
        {
            if (!gameObject.activeSelf)
            {
                Invoke("ActivateAfterDelay", 3f);

            }

        }

        private void ActivateAfterDelay()
        {
     
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                GameManager.Instance.IncreaseScore(score);
                GameManager.Instance.IncreaseCoin(coin);
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
                ActiveCoin();

            }

           


        }

    }
}

