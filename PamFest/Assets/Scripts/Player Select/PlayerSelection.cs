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

    public Animator playerBody;
    public Animator playerHead;
    public GameObject ready;
    public GameObject confirmButton;

    public Color playerColour;

    public GameObject audioManager;
    private PlayerAnnouncements playerAnnouncements;

    public void Awake()
    {
        totalPlayers = players.Length;
        currentShownPlayer.sprite = players[currentPlayer];

        playerAnnouncements = audioManager.GetComponent<PlayerAnnouncements>();
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

        playerAnnouncements.OnPlayerSelect(currentPlayer);

        playerBody.SetTrigger("Selected");
        playerHead.SetTrigger("Selected");

        ready.SetActive(true);
        confirmButton.SetActive(false);

        ready.GetComponent<Image>().color = playerColour;
    }
}
