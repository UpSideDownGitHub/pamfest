using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementManager : MonoBehaviour
{
    [Header("Navigation")]
    public List<NavMeshAgent> enemyAgents = new List<NavMeshAgent>();
    public List<int> playerToFollowID = new List<int>();
    public List<float> timers = new List<float>();
    public float timeToAbandonHunt;

    // list of the player for the enemies to target
    public List<GameObject> players = new List<GameObject>();

    public bool canEnemy = true;

    private void Start()
    {
        enemyAgents[0].updateRotation = false;
        enemyAgents[0].updateUpAxis = false;
        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            players.Add(GameManager.instance.players[i].gameObject);
        }    
    }

    public void Update()
    {
        if (!canEnemy)
        {
            for (int i = 0; i < enemyAgents.Count; i++)
            {
                enemyAgents[i].isStopped = true;
            }
            return;
        }

        updateFollowTargets();

        if (players.Count == 0)
            return;
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            //print(playerToFollowID[i]);
            try
            {
                enemyAgents[i].SetDestination(players[playerToFollowID[i]].transform.position);
            }
            catch
            {
                timers[i] = Time.time;
                playerToFollowID[i] = Random.Range(0, players.Count);
            }
            
        }
    }

    public void updateFollowTargets()
    {
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            if (Time.time > timers[i] + timeToAbandonHunt)
            {
                timers[i] = Time.time;
                playerToFollowID[i] = Random.Range(0, players.Count);
            }
        }
    }

    public void addNewEnemy(GameObject enemyToAdd)
    {
        enemyToAdd.GetComponent<BoxCollider2D>().isTrigger = true;
        var agent = enemyToAdd.AddComponent<NavMeshAgent>();
        agent.speed = enemyAgents[0].speed;
        agent.radius = enemyAgents[0].radius;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemyAgents.Add(agent);
        playerToFollowID.Add(0);
        timers.Add(0);

        updatePlayers();
    }

    public void updatePlayers()
    {
        players.Clear();
        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            if (GameManager.instance.playersComplete[i] == 0)
            {
                players.Add(GameManager.instance.players[i].gameObject);
            }
        }

        updateFollowTargets();
    }

    public void enableEnemies()
    {
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            enemyAgents[i].isStopped = false;
        }
    }
}
