using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    public void UpdateCoinText(string coins)
    {
        coinsText.text = "Coins: " + coins;
    }
}
