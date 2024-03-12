using RunnerGameInputAct.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerGameInputAct.Ui
{
    public class LoadingCanvas : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.LoadMenuAndUi(2f);
        }
    }
}

