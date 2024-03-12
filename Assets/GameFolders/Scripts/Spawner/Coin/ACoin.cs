using RunnerGameInputAct.Obstacle.Spawner;
//using RunnerGameInputAct.Tile;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ACoin : MonoBehaviour
{
    //public float turnSpeed = 90f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ObstacleSpawner>() != null) {
            Destroy(gameObject);
            return;
        }
            
        if(other.gameObject.name != "Player")
        {
            return;
        }

        Destroy(gameObject);
    }
    private void Update()
    {
        //transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
