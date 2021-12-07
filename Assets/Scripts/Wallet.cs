using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private const string SCORE = "score";
    public event Action WalletChanged;
    public int Amount
    {
        get
        {
            return PlayerPrefs.GetInt(SCORE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(SCORE, value);
            PlayerPrefs.Save();
        }
    }

    public void AddCoins()
    {
        Amount++;
        WalletChanged?.Invoke();
    }
    public void ClearWallet()
    {
        Amount = 0;
        WalletChanged?.Invoke();
    }
        
}
