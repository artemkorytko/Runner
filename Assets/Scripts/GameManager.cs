using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Level level;
    private UIcontroller uiCcontroller;

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
    }
    public void NextLevel()
    {
        UnSubscribePlayerAction();
        level.GenerateLevel();
        StartGame();
    }
    public void Restart()
    {
        //homework
        //TODO : restart logic
        NextLevel();
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
        //win ui
    }

    private void OnPlayerDeath()
    {
        uiCcontroller.OpenLost();
        //lost ui
    }
}