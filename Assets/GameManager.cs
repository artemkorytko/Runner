using System;
using System.Collections;
using UnityEngine;

namespace Assets
{
    public class GameManager : MonoBehaviour
    {
        private Level level;
        private UiController uiController;

        

        private void Awake()
        {
            level = GetComponent<Level>();
            uiController = FindObjectOfType<UiController>();
        }

        private void Start()
        {
            level.GenerateLevel();
            uiController.OpenMenu();
        }

        public void StartGame()
        {
            level.StartGame();
            SubscrabePlayerAction();
            uiController.OpenGame();
        }

        public void NextLevel()
        {
            UnSubscrabePlayerAction();
            level.GenerateLevel();
            StartGame();
        }

        public void Restart()
        {
            //TODO: restart logic
            NextLevel();
        }

        private void SubscrabePlayerAction()
        {
            level.Player.OnDied += OnPlayerDeath;
            level.Player.OnFinish += OnPlayerFinish;
        }

        private void UnSubscrabePlayerAction()
        {
            level.Player.OnDied -= OnPlayerDeath;
            level.Player.OnFinish -= OnPlayerFinish;
        }

        private void OnPlayerFinish()
        {
            uiController.OpenWin();
        }

        private void OnPlayerDeath()
        {
            uiController.OpenLost();
        }
    }
}