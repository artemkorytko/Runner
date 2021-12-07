using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Level level;
    private UIcontroller uiCcontroller;
    private GamePanel gamePanel;
    private Wallet wallet;
    private void Awake()
    {
        level = GetComponent<Level>();
        uiCcontroller = FindObjectOfType<UIcontroller>();
    }
    private void Start()
    {
        level.GenerateLevel();
        uiCcontroller.OpenMenu();
    }

    public void StartGame()
    {
        level.StartGame();
        SubscribePlayerAction();
        uiCcontroller.OpenGame();
        gamePanel = FindObjectOfType<GamePanel>();
        wallet = FindObjectOfType<Wallet>();
        wallet.ClearWallet();
        SubscribeWallet();

    }
    public void ContinueGame()
    {
        level.StartGame();
        SubscribePlayerAction();
        uiCcontroller.OpenGame();
        gamePanel = FindObjectOfType<GamePanel>();
        wallet = FindObjectOfType<Wallet>();
        SubscribeWallet();
    }
    public void NextLevel()
    {
        UnSubscribePlayerAction();
        level.GenerateLevel();
        ContinueGame();
    }
    public void RestartLevel()
    {
        UnSubscribePlayerAction();
        level.RestartLevel();
        wallet.ClearWallet();
        StartGame();
       
    }
    private void SubscribePlayerAction()
    {
        level.Player.OnDie += OnPlayerDeath;
        level.Player.OnFinish += OnPlayerFinish;
    }

    private void UnSubscribePlayerAction()
    {
        level.Player.OnDie -= OnPlayerDeath;
        level.Player.OnFinish -= OnPlayerFinish;
    }
    private void OnPlayerFinish()
    {
        uiCcontroller.OpenWin();
    }

    private void OnPlayerDeath()
    {
        uiCcontroller.OpenLost();
    }

    private void SubscribeWallet()
    {
        wallet.WalletChanged += OnWalletChanged;
    }
    private void UnsubscribeWallet()
    {
        wallet.WalletChanged -= OnWalletChanged;
    }
    private void OnWalletChanged()
    {
        gamePanel.UpdateScore();
    }
}