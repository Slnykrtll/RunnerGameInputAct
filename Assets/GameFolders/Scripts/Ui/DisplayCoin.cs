using RunnerGameInputAct.Manager;
using RunnerGameInputAct.Ui;
using TMPro;
using UnityEngine;
namespace RunnerGameInputAct.Ui
{
    public class DisplayCoin : MonoBehaviour
    {
        TextMeshProUGUI coinText;

        private void Awake()
        {
            coinText = GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {

        }
        private void OnEnable()
        {
            GameManager.Instance.OnCoinChanged += HandleCoinChanged;
            GameManager.Instance.IncreaseCoin();
        }

        private void HandleCoinChanged(int coin)
        {
            coinText.text = $"Coin: {PlayerPrefs.GetInt("Coin", 0)}";
        }
        private void OnDisable()
        {
            GameManager.Instance.OnCoinChanged -= HandleCoinChanged;

        }

    }

}
