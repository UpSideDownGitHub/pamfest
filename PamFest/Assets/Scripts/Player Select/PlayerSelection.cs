using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public Sprite[] players;
    public Image currentShownPlayer;
    public int totalPlayers;
    public int currentPlayer = 0;
    public int currentControllerID;

    public void Awake()
    {
        totalPlayers = players.Length;
        currentShownPlayer.sprite = players[currentPlayer];
    }

    public void next()
    {
        if (currentPlayer + 1 > totalPlayers - 1)
            currentPlayer = 0;
        else
            currentPlayer++;
        currentShownPlayer.sprite = players[currentPlayer];
    }

    public void confirm()
    {
        PlayerPrefs.SetInt("Player " + currentControllerID.ToString(), currentPlayer);
    }
}
