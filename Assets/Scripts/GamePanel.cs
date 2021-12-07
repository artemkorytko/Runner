using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public TextMeshProUGUI score;
    private Wallet wallet;

  
    public void UpdateScore()
    {
        wallet = FindObjectOfType<Wallet>();
        score.text = $"Score: {wallet.Amount}";
    }
    
}
