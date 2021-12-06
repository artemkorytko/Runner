using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private Level level;
        private UI_Controller uiController;
        private void Awake()
        {
            level = GetComponent<Level>();
            uiController = FindObjectOfType<UI_Controller>();
        }

        private void Start()
        {
            level.GenerateLevel();
            uiController.OpenMenu();
        }

        public void StartGame()
        {
            level.StartGame();
            SubPlayerAction();
            uiController.OpenGame();
        }

        private void SubPlayerAction()
        {
            level.Player.OnDied += OnPlayerDeath;
            level.Player.OnFinish += OnPlayerFinish;
        }
        private void UnSubPlayerAction()
        {
            level.Player.OnDied -= OnPlayerDeath;
            level.Player.OnFinish -= OnPlayerFinish;
        }

        public void RestartLevel()
        {
            NextLevel(); //TODO: restart
        }
        public void NextLevel()
        {
            UnSubPlayerAction();
            level.GenerateLevel();
            StartGame();
        }
        private void OnPlayerFinish()
        {
            uiController.OpenWin();
        }

        private void OnPlayerDeath()
        {
            uiController.OpenLose();
        }
    }
}