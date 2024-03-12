using RunnerGameInputAct.Tile;
using System.Collections.Generic;
using UnityEngine;
public class ACoinSpawner : MonoBehaviour
{
    public static ACoinSpawner instance;
    public GameObject coinPrefab;
    ACoinPosition coinPosition;
    private void Start()
    {
        coinPosition = ACoinPosition.instance;
        if (instance == null)
            {
                instance = this;
            }
        SpawnCoins(coinPosition);

    }
    public void SpawnCoins(ACoinPosition coinPosition)
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            
            GameObject temp = Instantiate(coinPrefab);
            temp.transform.position = coinPosition.GetRandomPointInCollider();
            //coinList.Add(temp);
            //temp.SetActive(false);
        }
    }



}




