using UnityEngine;

public class ACoinPosition : MonoBehaviour
{
    public static ACoinPosition instance;
    [SerializeField]public Collider coinCollider;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public Vector3 GetRandomPointInCollider()
    {
        Vector3 point = new Vector3(
            Random.Range(coinCollider.bounds.min.x, coinCollider.bounds.max.x),
            1,
            Random.Range(coinCollider.bounds.min.z, coinCollider.bounds.max.z)
            );
        //if (point != coinCollider.ClosestPoint(point)) //Belirlenen noktaya en yakýn nokta  
        //{
        //    point = GetRandomPointInCollider();
        //}

        //point.y = 1;
        return point;
    }

}
