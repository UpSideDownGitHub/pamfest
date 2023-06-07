using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
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

    [Header("More Advanced AI")]
    public Vector3 center;
    public float minDistanceToDemandFollow;
    public float speedIncreaseTime;
    public float baseSpeed;
    public float baseAccel;
    public float speedMultiplier;
    public float accelMultiplier;

    [Header("SPeed Increase Over Time")]
    public float speedIncrease;

    [Header("Animations baby")]
    public AnimatorController animController;
    public List<Animator> enemyAnims = new List<Animator>();


    private void Start()
    {
        enemyAgents[0].updateRotation = false;
        enemyAgents[0].updateUpAxis = false;
        baseSpeed = enemyAgents[0].speed;
        baseAccel = enemyAgents[0].acceleration;
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
                enemyAgents[i].SetDestination(center);
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
                StartCoroutine(increaseSpeed(i));
                timers[i] = Time.time;
                playerToFollowID[i] = Random.Range(0, players.Count);
            }
        }
    }

    public void LateUpdate()
    {
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            if (enemyAgents[i].velocity.x > 0)
            {
                enemyAnims[i].SetBool("runningRight", true);
                enemyAnims[i].SetBool("runningLeft", false);
                enemyAnims[i].SetBool("idle", false);
            }
            else if (enemyAgents[i].velocity.x < 0)
            {
                enemyAnims[i].SetBool("runningRight", false);
                enemyAnims[i].SetBool("runningLeft", true);
                enemyAnims[i].SetBool("idle", false);
            }
            else
            {
                enemyAnims[i].SetBool("runningRight", false);
                enemyAnims[i].SetBool("runningLeft", false);
                enemyAnims[i].SetBool("idle", true);
            }
        }
    }

    public int findClosestPlayer()
    {
        GameObject closestPlayer = null;
        float closestDistance = 999;
        foreach (var item in players)
        {
            var dist = Vector3.Distance(item.transform.position, transform.position);
            if (dist < minDistanceToDemandFollow && dist < closestDistance)
            {
                closestPlayer = item;
                closestDistance = dist;
            }
        }
        if (closestPlayer != null)
        {
            // target this player
            return players.IndexOf(closestPlayer);
        }
        return -1;
    }

    public void updateFollowTargets()
    {
        for (int i = 0; i < enemyAgents.Count; i++)
        {
            var toFollow = findClosestPlayer();
            if (toFollow > -1)
            {
                // mover to this player rather than finding a player at random
                StartCoroutine(increaseSpeed(i));
                timers[i] = Time.time;
                playerToFollowID[i] = toFollow;
            }
            if (Time.time > timers[i] + timeToAbandonHunt)
            {
                StartCoroutine(increaseSpeed(i));
                timers[i] = Time.time;
                playerToFollowID[i] = Random.Range(0, players.Count);
            }
        }
    }

    public void addNewEnemy(GameObject enemyToAdd)
    {
        enemyToAdd.GetComponent<BoxCollider2D>().isTrigger = true;
        var anim = enemyToAdd.GetComponentInChildren<Animator>();
        anim.runtimeAnimatorController = animController;
        anim.gameObject.transform.localScale = new Vector3(1,1,1);
        anim.gameObject.transform.position = new Vector3(anim.gameObject.transform.position.x + 0.05f, anim.gameObject.transform.position.y, anim.gameObject.transform.position.z);
        var agent = enemyToAdd.AddComponent<NavMeshAgent>();

        agent.speed = enemyAgents[0].speed;
        agent.radius = enemyAgents[0].radius;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        enemyAgents.Add(agent);
        playerToFollowID.Add(0);
        timers.Add(0);
        enemyAnims.Add(anim);

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

    public void increaseSpeedPerminant()
    {
        baseSpeed += speedIncrease;
        foreach (var enemy in enemyAgents)
        {
            enemy.speed = baseSpeed;
        }
    }

    IEnumerator increaseSpeed(int ID)
    {
        enemyAgents[ID].speed = baseSpeed * speedMultiplier;
        enemyAgents[ID].acceleration = baseAccel * accelMultiplier;
        yield return new WaitForSeconds(speedIncreaseTime);
        enemyAgents[ID].speed = baseSpeed;
        enemyAgents[ID].acceleration = baseAccel;
    }
}
