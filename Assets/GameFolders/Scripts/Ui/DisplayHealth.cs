using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace RunnerGameInputAct.Ui
{
    public class DisplayHealth : MonoBehaviour
    {
        TextMeshProUGUI healthText;
        private void Awake()
        {
            healthText = GetComponent<TextMeshProUGUI>();
        }
        public void WriteHealth(int currentHealth)
        {
            healthText.text ="Health:"+ currentHealth.ToString(); 
        }
    }
}

