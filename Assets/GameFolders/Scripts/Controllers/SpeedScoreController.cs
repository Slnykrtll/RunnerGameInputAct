using RunnerGameInputAct.Manager;
using UnityEngine;
namespace RunnerGameInputAct.Controllers
{
    public class SpeedScoreController : MonoBehaviour
    {
        //private Rigidbody rb;
        //[SerializeField] int scoreSpeed;
        //private float score;
        //Vector3 startingPoint = Vector3.zero;
        public Transform player; // Karakterinizin transform bile�eni
        public float distancePerScore = 5f; // Bir puan kazanmak i�in gereken mesafe
        //private int score = 0; // Ba�lang��ta score'u s�f�r yap�n
        private float lastScoredPosition = 0f; // Son puan kazan�lan pozisyon
        private void Awake()
        {
            //rb = GetComponent<Rigidbody>();


        }
        private void Start()
        {
            lastScoredPosition = player.position.z; // Ba�lang��ta karakterin pozisyonunu kaydedin
        }
        private void Update()
        {
            // Karakterinizin mevcut pozisyonunu al�n
            float currentZPosition = player.position.z;

            // Karakterinizin son puan kazan�lan pozisyondan ne kadar ilerledi�ini hesaplay�n
            float distanceTraveled = currentZPosition - lastScoredPosition;

            // E�er karakter, bir sonraki puan� kazanmak i�in gerekli mesafeyi ge�tiyse
            if (distanceTraveled >= distancePerScore)
            {
                // Kazan�lan puanlar� g�ncelleyin
                int scoredPoints = Mathf.FloorToInt(distanceTraveled / distancePerScore);
                //score += scoredPoints;
                lastScoredPosition += scoredPoints * distancePerScore;
                GameManager.Instance.IncreaseScore(scoredPoints);
            }
            //float currentZSpeed = rb.velocity.z;
            ////float ZSpeed = currentZSpeed*scoreIncreaseRate;
            //scoreSpeed = (int)Mathf.Floor(currentZSpeed);
            ////scoreSpeed = int.Parse(currentZSpeed);
            //GameManager.Instance.IncreaseScore(scoreSpeed);

            ////playerDistance = Vector3.Distance(startingPoint, player.transform.position);
            ////float currentZSpeed = playerDistance;
            ////scoreSpeed= Mathf.FloorToInt(currentZSpeed);
            //////scoreSpeed = (int)Mathf.Floor(currentZSpeed);
            ////GameManager.Instance.IncreaseScore(scoreSpeed);






        }

    }
}
