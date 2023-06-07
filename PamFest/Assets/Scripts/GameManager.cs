using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool movingRight = true;
    public List<Player> players = new List<Player>();
    public int[] playersComplete = new int[4]; // 0 = Running, 1 = Completed, 2 = Eaten
    public List<Player> winners = new List<Player>();

    public static GameManager instance;
    public EnemyMovementManager enemyMovementManager;
    public ConeManager coneManager;

    [Header("Timer")]
    public Timer timer;
    public bool playerFinished;

    [Header("End Of Round")]
    public float waitTime;
    public GameObject endRunCanvas;
    bool endOfRound = false;

    [Header("End Of Game")]
    public string[] playerPrefsNames;

    [Header("Score")]
    public GameObject[] scoreObjects;
    public TMP_Text[] scoreTexts;
    public int[] scores = new int[4];
    public ParticleSystem[] crossedLineParticles;
    public AudioSource source;

    [Header("Wave Related Systems")]
    public int currentWave = 0;
    public float speedIncrease;
    public float minMusicSpeed = 0.85f;
    public float maxMusicSpeed = 1.3f;
    public int increaseRate;
    public AudioSource BGMusicSource;
    public GameObject leftArrow;
    public GameObject rightArrow;

    // thunder effect
    public float thunderRound;
    public GameObject thunderObject;

    [Header("End Sequence")]
    public bool gameEnded;
    public float minCameraValue;
    public Vector3 playerPosition;
    public float gameEndTime;
    private float _gameSinceGameEnd;
    private Camera cam;
    public float lerpTime;

    public AudioSource finalSax;


    private void Awake()
    {
        gameEnded = false;
        currentWave = 0;
        playerFinished = false;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        BGMusicSource.pitch = minMusicSpeed;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gamepadID = i;
            scores[i] = 0;
            scoreTexts[i].text = "0";
        }
    }

    public void endRoundTime()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (playersComplete[i] == 0)
            {
                playersComplete[i] = 2;
                players[i].rb.velocity = Vector2.zero;
                players[i].ringSprite.enabled = false;
                enemyMovementManager.addNewEnemy(players[i].gameObject);
                players[i].gameObject.tag = "Enemy";
            }
        }
    }

    public void playerCrossedLine(int ID)
    {
        scoreTexts[ID].text = (++scores[ID]).ToString();

        if (movingRight)
            crossedLineParticles[0].Play();
        else
            crossedLineParticles[1].Play();

        if (!playerFinished)
        {
            timer.startTimer();
            playerFinished = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            Time.timeScale = 1;
            cam.transform.position = Vector3.Lerp(cam.transform.position, playerPosition, lerpTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, minCameraValue, lerpTime);
            
            if (Time.time > _gameSinceGameEnd)
                SceneManager.LoadScene("EndScreen");
            return;
        }
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
                }
            }
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
            gameEnded = true;
            winners[winners.Count - 1].rb.velocity = Vector2.zero;
            playerPosition = winners[winners.Count - 1].transform.position;
            playerPosition = new Vector3(playerPosition.x, playerPosition.y, cam.transform.position.z);
            _gameSinceGameEnd = Time.time + gameEndTime;
            BGMusicSource.Stop();
            finalSax.Play();
            timer.objects[0].SetActive(false);
            timer.objects[1].SetActive(false);
            return;
        }

        if (finishedRound)
        {
            movingRight = !movingRight;

            // remove old cones
            coneManager.removeOldCones();
            // End of the round
            StartCoroutine(endRound());
        }
    }

    public IEnumerator endRound()
    {
        // increase the current wave
        currentWave++;
        if (currentWave % increaseRate == 0 && BGMusicSource.pitch < maxMusicSpeed)
        {
            // increase the music speed
            BGMusicSource.pitch += speedIncrease;
        }
        // increase the enemy speed
        enemyMovementManager.increaseSpeedPerminant();

        if (currentWave == thunderRound)
            thunderObject.SetActive(true);

        if (movingRight)
            rightArrow.SetActive(true);
        else
            leftArrow.SetActive(true);


        endOfRound = true;
        endRunCanvas.SetActive(true);
        enemyMovementManager.canEnemy = false;
	    source.Play();
        timer.stopTimer();

        for (int i = 0; i < players.Count; i++)
        {
            scoreObjects[i].SetActive(true);
        }


        yield return new WaitForSeconds(waitTime);
        enemyMovementManager.canEnemy = true;
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
        // update the enemy to target the new players
        enemyMovementManager.updatePlayers();

        // add new cones
        coneManager.increaseConeAmmount();
        coneManager.spawnCones();

        // turn of the arrows
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);

        // turn off score
        for (int i = 0; i < players.Count; i++)
        {
            scoreObjects[i].SetActive(false);
        }

        endOfRound = false;
    }
}
