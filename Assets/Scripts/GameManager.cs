using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputHandler inputHandler;

    private int _coins = 0;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        NewGame();
        uiManager.UpdateCoinText(_coins.ToString());
        level.player.StartCoins(_coins);
    }

    private void Update()
    {
        if (!inputHandler.IsHold && level.player.IsActive)
        {
           Time.timeScale = .3f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
    }

    public void NewGame()
    {
        level.GenerateLevel();
        UnSubPlayer();
    }

    public void StartGame()
    {
        level.StartGame();
        uiManager.UpdateCoinText(_coins.ToString());
        level.player.OnDied += OnPlayerDead;
        level.player.OnFinish += OnPlayerFinish;
        level.player.OnTakeCoin += OnPlayerTakeCoin;
    }

    private void UnSubPlayer()
    {
        level.player.OnDied -= OnPlayerDead;
        level.player.OnFinish -= OnPlayerFinish;
        level.player.OnTakeCoin -= OnPlayerTakeCoin;
    }

    public void RestartGame()
    {
        NewGame();
    }

    private void OnPlayerTakeCoin()
    {
        uiManager.UpdateCoinText((_coins + level.player.GetCoins).ToString());
    }

    private void OnPlayerFinish()
    {
        _coins += level.player.GetCoins;
        uiManager.UpdateCoinText(_coins.ToString());
        uiManager.ShowWin();
        Time.timeScale = 1;
    }

    private void OnPlayerDead()
    {
        uiManager.ShowFail();
    }
}
