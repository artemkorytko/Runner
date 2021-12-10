using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        private int gamePoints;

        private LevelConfig levelConfig;

        private bool gameIsActive;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            levelConfig = GetComponent<LevelConfig>();

        }

        private void Update()
        {
            if (!gameIsActive)
                return;

            levelConfig.MoveObstacles();
        }
        public void AddPoints()
        {
            gamePoints++;
            //show in UI
        }

        public void StartGame()
        {
            Player.Instance.SetActive(true);
            gameIsActive = true;
            levelConfig.CreateObstacle();
        }
        public void GameOver()
        {
            Player.Instance.SetActive(false);
            gameIsActive = false;
        }

    }
}
