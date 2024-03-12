using RunnerGameInputAct.Manager;
using RunnerGameInputAct.Ui;
using System.Collections;
using UnityEngine;
namespace RunnerGameInputAct.Controllers
{
    public class SpeedScoreController : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] int scoreSpeed;
        private float score;
        Vector3 startingPoint = Vector3.zero;
        //[SerializeField] float scoreIncreaseRate = 0.1f;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();


        }
        private void Update()
        {

            float currentZSpeed = rb.velocity.z;
            //float ZSpeed = currentZSpeed*scoreIncreaseRate;
            scoreSpeed = (int)Mathf.Floor(currentZSpeed);
            GameManager.Instance.IncreaseScore(scoreSpeed);

            //playerDistance = Vector3.Distance(startingPoint, player.transform.position);
            //float currentZSpeed = playerDistance;
            //scoreSpeed= Mathf.FloorToInt(currentZSpeed);
            ////scoreSpeed = (int)Mathf.Floor(currentZSpeed);
            //GameManager.Instance.IncreaseScore(scoreSpeed);

        }




    }

}
