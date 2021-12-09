using System.Collections.Generic;
using UnityEngine;

namespace FlapyBird
{
    public class LevelConfig : MonoBehaviour
    {
        [SerializeField] private ObstacleController obstacleControllerPrefab;
        [SerializeField, Range(0, 1)] private float obstacleRate = .5f;
        [SerializeField] private float startX = 5f;
        [SerializeField] private float finishX = -5f;
        [SerializeField] private float offset = -1;
        [SerializeField] private float moveSpeed = 1f;
        
        private List<ObstacleController> obsts = new List<ObstacleController>();

        public void Init()
        {
            ObstacleController spawnedObst = Instantiate(obstacleControllerPrefab);
            spawnedObst.transform.position = new Vector3(startX, Random.Range(-2, 2));
            obsts.Add(spawnedObst);
        }
        
        public void MoveObstacles()
        {
            if (obsts[0].transform.position.x <= finishX)
            {
                Destroy(obsts[0].gameObject);
                obsts.RemoveAt(0);
            }
            
            foreach (var item in obsts)
            {
                item.transform.position = Vector3.Lerp(item.transform.position,
                    new Vector3(item.transform.position.x + offset, item.transform.position.y), 
                    Time.deltaTime * moveSpeed);
            }
            
            Transform obst = obsts[obsts.Count - 1].transform;
            if (obst.position.x < startX - 5 * obstacleRate)
            {
                ObstacleController spawnedObst = Instantiate(obstacleControllerPrefab);
                spawnedObst.transform.position = new Vector3(startX, Random.Range(-2, 2));
                spawnedObst.Setup();
                obsts.Add(spawnedObst);
            }
        }
        
    }
}