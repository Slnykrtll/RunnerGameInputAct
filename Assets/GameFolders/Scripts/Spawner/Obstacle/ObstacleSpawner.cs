using RunnerGameInputAct.Controllers;
using RunnerGameInputAct.Player;
using RunnerGameInputAct.Tile.Spawner;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGameInputAct.Obstacle.Spawner
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public static ObstacleSpawner instance;

        private TileSpawner tileSpawner;

        [SerializeField]
        private List<GameObject> obstacleList = new List<GameObject>();
        Queue<GameObject> obstaclePool = new Queue<GameObject>();
        public GameObject[] obstaclePrefabs;
        public GameObject obstacle;
        public Transform tileTransformDeneme1234;
        private Vector3 currentObstacleLocation = Vector3.zero;
        private void Start()
        {
           
        }
       
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            for (int i = 0; i < 5; i++)
            {
                obstacleList.Add(Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], currentObstacleLocation, Quaternion.identity));

                obstacleList[i].SetActive(false);
            }
            tileSpawner = GetComponent<TileSpawner>();
        }
        private void Update()
        {
            AccessChildTransform();
        }
        public void AccessChildTransform()
        {
            if (tileSpawner.currentTiles.Count >= 4)
            {
                GameObject tile = tileSpawner.currentTiles[0];
                if (tile.transform.childCount > 0)
                {
                    Transform childTransform = tile.transform.GetChild(0);
                    tileTransformDeneme1234 = childTransform;

                }
            }
        }
        public void InstantiateRandomObject()
        {
            obstacle = obstacleList[Random.Range(0, obstacleList.Count)];
            while (obstacle.activeSelf)
            {

                obstacle = obstacleList[Random.Range(0, obstacleList.Count)];

            }
            obstacle.SetActive(true);
            obstacle.transform.position = tileTransformDeneme1234.position;

            obstaclePool.Enqueue(obstacle);
            if (obstaclePool.Count >= 3)
            {
                obstacle = obstaclePool.Dequeue();
                obstacle.SetActive(false);
                obstacle = null;
            }


        }
       
        
     






    }
}








