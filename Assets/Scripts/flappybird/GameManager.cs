using System;
using UnityEngine;

namespace FlapyBird
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private int _gamePoints = 0;
        private bool _isActive;
        private LevelConfig _levelConfig;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            _levelConfig = GetComponent<LevelConfig>();
        }

        public void StartGame()
        {
            Player.Instance.SetActive(true);
            _isActive = true;
            _levelConfig.Init();
        }

        private void FixedUpdate()
        {
            if (_isActive)
                _levelConfig.MoveObstacles();
        }

        public void GameOver()
        {
            _isActive = false;
            Player.Instance.SetActive(false);
        }

        public void AddPoint()
        {
            _gamePoints++;
            Debug.Log(_gamePoints);
        }
    }
}