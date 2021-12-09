using UnityEngine;

namespace FlapyBird
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private GameObject upObstacle;
        [SerializeField] private GameObject downObstacle;

        public void Setup()
        {
            upObstacle.transform.position = new Vector3(upObstacle.transform.position.x,
                                            upObstacle.transform.position.y + Random.Range(-.5f, .5f));
            downObstacle.transform.position = new Vector3(downObstacle.transform.position.x,
                downObstacle.transform.position.y + Random.Range(-.5f, .5f));
        }
    }
}