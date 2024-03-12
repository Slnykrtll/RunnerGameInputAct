using RunnerGameInputAct.Manager;
using UnityEngine;
namespace RunnerGameInputAct.Ui
{
    public class MenuPanel : MonoBehaviour
    {
        public void StartButtonClick()
        {
            GameManager.Instance.LoadScene(1);
        }
        public void ExitButtonClick()
        {
            GameManager.Instance.ExitGame();
        }
    }
}

