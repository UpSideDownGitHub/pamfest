using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool movingRight = true;
    public List<Player> players = new List<Player>();
    public int[] playersComplete = new int[4]; // 0 = Running, 1 = Completed, 2 = Eaten
    public List<Player> winners = new List<Player>();

    public static GameManager instance;

    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
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
            print("Game Ended");
            Time.timeScale = 0f;
            for (int i = 0; i < players.Count; i++)
            {
                if (!winners.Contains(players[i]))
                {
                    winners.Add(players[i]);
                    print(winners[winners.Count - 1]);
                    print(winners[winners.Count - 2]);
                    print(winners[winners.Count - 3]);
                    PlayerPrefs.SetInt("First Place", winners[winners.Count - 1].gamepadID);
                    PlayerPrefs.SetInt("Second Place", winners[winners.Count - 2].gamepadID);
                    PlayerPrefs.SetInt("Third Place", winners[winners.Count - 3].gamepadID);

                    // load the end scene
                    SceneManager.LoadScene(1);
                    break;
                }
            }
        }



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
        
        if (finishedRound)
        {
            movingRight = !movingRight;
            // do end of round things

            // re-enable the player movement if they can move
            for (int i = 0; i < players.Count; i++)
            {
                if (playersComplete[i] < 2)
                {
                    playersComplete[i] = 0;
                    players[i].playerMovementEnabled = true;
                }
            }
        }
    }
}
