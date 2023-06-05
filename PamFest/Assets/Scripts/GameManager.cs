using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool movingRight = true;
    public List<Player> players = new List<Player>();
    public int[] playersComplete = new int[4]; // 0 = Running, 1 = Completed, 2 = Eaten

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
        bool finishedRound = true;
        for (int i = 0; i < players.Count; i++)
        {
            if (playersComplete[i] > 0)
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
