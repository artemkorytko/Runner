using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject fail;
    [SerializeField] private GameObject pause;

    private void Start()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
        game.SetActive(false);
        win.SetActive(false);
        fail.SetActive(false);
        pause.SetActive(false);
    }
    
    public void ShowGame()
    {
        menu.SetActive(false);
        game.SetActive(true);
        win.SetActive(false);
        fail.SetActive(false);
        pause.SetActive(false);
    }
    public void ShowWin()
    {
        menu.SetActive(false);
        game.SetActive(false);
        win.SetActive(true);
        fail.SetActive(false);
        pause.SetActive(false);
    }
    public void ShowFail()
    {
        menu.SetActive(false);
        game.SetActive(false);
        win.SetActive(false);
        fail.SetActive(true);
        pause.SetActive(false);
    }
    public void ShowPause()
    {
        menu.SetActive(false);
        game.SetActive(false);
        win.SetActive(false);
        fail.SetActive(false);
        pause.SetActive(true);
    }
    /// <summary>
    /// Take coins in string format
    /// </summary>
    /// <param name="coins"></param>
    public void UpdateCoinText(string coins)
    {
        coinsText.text = "Coins: " + coins;
    }
}
