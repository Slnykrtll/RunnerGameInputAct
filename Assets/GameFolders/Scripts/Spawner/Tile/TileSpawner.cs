using System.Collections.Generic;
using UnityEngine;

namespace RunnerGameInputAct.Tile.Spawner
{
    public class TileSpawner : MonoBehaviour
    {
        public static TileSpawner instance;

        [SerializeField] int tileStartCount = 5;
        [SerializeField] public GameObject startTile;
        [SerializeField] public List<GameObject> currentTiles;
        private GameObject prevTile;

        private Vector3 currentTileLocation = Vector3.zero;
        private Vector3 currentTileDirections = Vector3.forward;
       

        //[SerializeField] public List<Tile> tilePrefabs;
        //[SerializeField] public Tile[] tilePrefabs;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            currentTiles = new List<GameObject>();
           
            for (int i = 0; i < tileStartCount; i++)
            {
                SpawnTile(startTile.GetComponent<Tile>());
            }
           

        }

        private void SpawnTile(Tile tile)
        {

            prevTile = Instantiate(tile.gameObject, currentTileLocation, Quaternion.identity);
            currentTiles.Add(prevTile);
            currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirections);
            //Debug.Log("oyun baþýnda bir kez çalýþmalý");

        }
        public void AddNewTile()
        {

            for (int i = 0; i < currentTiles.Count; i++)
            {
                GameObject tile = currentTiles[i];
                
                Vector3 newPosition = tile.transform.position + tile.transform.forward * tile.GetComponent<Renderer>().bounds.size.z;
                tile.transform.position = newPosition;

            }
         
        }
       

    }






}



