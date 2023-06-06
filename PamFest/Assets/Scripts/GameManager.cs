using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool movingRight = true;
    public List<PlayerMovement> players = new List<PlayerMovement>();
    public int[] playersComplete = new int[4]; // 0 = Running, 1 = Completed, 2 = Eaten
    public List<PlayerMovement> winners = new List<PlayerMovement>();

    public static GameManager instance;

    [Header("Timer")]
    public Timer timer;
    public bool playerFinished;

    [Header("End Of Round")]
    public float waitTime;
    public GameObject endRunCanvas;
    bool endOfRound = false;

    [Header("End Of Game")]
    public string[] playerPrefsNames;


    private void Awake()
    {
        playerFinished = false;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gamepadID = i;
        }
    }

    public void endRoundTime()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (playersComplete[i] == 0)
            {
                playersComplete[i] = 2;
            }
        }
    }

    public void playerCrossedLine()
    {
        if (!playerFinished)
        {
            timer.startTimer();
            playerFinished = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (endOfRound)
            return;

        // check for the end of the round
        bool finishedRound = true;
        for (int i = 0; i < players.Count; i++)
        {
            if (playersComplete[i] == 2)
            {
                players[i].playerMovementEnabled = false;
                if (!winners.Contains(players[i]))
                    winners.Add(players[i]);
                continue;
            }
            if (playersComplete[i] == 1)
            {
                players[i].playerMovementEnabled = false;
                continue;
            }
            finishedRound = false;
        }

        // CHECK FOR THE END OF THE GAME
        int playersLeft = players.Count;
        for (int i = 0; i < players.Count; i++)
        {
            if (playersComplete[i] == 2)
            {
                playersLeft--;
            }
        }
        if (playersLeft <= 1)
        {
            // end the game
            Time.timeScale = 0f;
            for (int i = 0; i < players.Count; i++)
            {
                if (!winners.Contains(players[i]))
                {
                    // add the last player
                    winners.Add(players[i]);

                    // save the players that where part of the game
                    int w = 0;
                    for (int j = 0; j < winners.Count; j++)
                    {
                        if (w > 2)
                            break;
                        //print(playerPrefsNames[j] + ": " + winners[winners.Count - j - 1].gamepadID);
                        PlayerPrefs.SetInt(playerPrefsNames[j], winners[winners.Count - j - 1].gamepadID);
                        w++;
                    }
                    // RUN END GAME SEQUENCE
                    SceneManager.LoadScene(2);
                    break;
                }
            }
        }

        if (finishedRound)
        {
            movingRight = !movingRight;

            // End of the round
            StartCoroutine(endRound());
        }
    }

    public IEnumerator endRound()
    {
        endOfRound = true;
        endRunCanvas.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        timer.stopTimer();
        playerFinished = false;
        endRunCanvas.SetActive(false);
        for (int i = 0; i < players.Count; i++)
        {
            if (playersComplete[i] < 2)
            {
                playersComplete[i] = 0;
                players[i].playerMovementEnabled = true;
            }
        }
        endOfRound = false;
    }
}
