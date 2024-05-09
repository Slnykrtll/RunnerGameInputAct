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
        public Transform player; // Karakterinizin transform bileþeni
        public float distancePerScore = 5f; // Bir puan kazanmak için gereken mesafe
        //private int score = 0; // Baþlangýçta score'u sýfýr yapýn
        private float lastScoredPosition = 0f; // Son puan kazanýlan pozisyon
        private void Awake()
        {
            //rb = GetComponent<Rigidbody>();


        }
        private void Start()
        {
            lastScoredPosition = player.position.z; // Baþlangýçta karakterin pozisyonunu kaydedin
        }
        private void Update()
        {
            // Karakterinizin mevcut pozisyonunu alýn
            float currentZPosition = player.position.z;

            // Karakterinizin son puan kazanýlan pozisyondan ne kadar ilerlediðini hesaplayýn
            float distanceTraveled = currentZPosition - lastScoredPosition;

            // Eðer karakter, bir sonraki puaný kazanmak için gerekli mesafeyi geçtiyse
            if (distanceTraveled >= distancePerScore)
            {
                // Kazanýlan puanlarý güncelleyin
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
