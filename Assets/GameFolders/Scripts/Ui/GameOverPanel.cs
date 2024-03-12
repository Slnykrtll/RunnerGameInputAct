using RunnerGameInputAct.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerGameInputAct.Ui
{
    public class GameOverPanel : MonoBehaviour
    {
        public void YesButtonClick()
        {
            GameManager.Instance.LoadScene();
            GameManager.Instance.ResetScore();
            this.gameObject.SetActive(false);
        }
        public void NoButtonClick()
        {
            GameManager.Instance.LoadMenuAndUi(1f);
            //GameManager.Instance.ResetScore();

        }
    }
}

