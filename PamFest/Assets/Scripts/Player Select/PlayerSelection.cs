using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public void Next(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (currentPlayer + 1 > totalPlayers - 1)
                currentPlayer = 0;
            else
                currentPlayer++;
            currentShownPlayer.sprite = players[currentPlayer];
        }
    }
    public void Previous(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (currentPlayer - 1 < 0)
                currentPlayer = totalPlayers - 1;
            else
                currentPlayer--;
            currentShownPlayer.sprite = players[currentPlayer];
        }
    }


    public void confirm()
    {
        PlayerPrefs.SetInt("Player " + currentControllerID.ToString(), currentPlayer);
        PlayerSelectManager.instance.confirmed[currentControllerID] = true;
    }
}