using UnityEngine;

namespace RunnerGameInputAct.Tile.Spawner
{
    public class GroundPooling : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                transform.position += new Vector3(0, 0, transform.GetComponent<Renderer>().bounds.size.z * 2);

            }

        }
    }
}

