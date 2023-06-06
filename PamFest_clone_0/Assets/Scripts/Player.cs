using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : NetworkBehaviour
{
    public Vector2 _vec2;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float smoothValue;
    public bool playerMovementEnabled;

    [Header("Enemyness")]
    public EnemyMovementManager enemyMovementManager;

    [Header("Gamepad Support")]
    public int gamepadID;

    public override void OnNetworkSpawn()
    {
        //if (!IsOwner) Destroy(this);
    }

    public void Movement(InputAction.CallbackContext ctx) => _vec2 = ctx.ReadValue<Vector2>();

    private void Start()
    {
        enemyMovementManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyMovementManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovementEnabled)
        {
            if (_vec2 != Vector2.zero)
            {
                Vector3 zero = Vector3.zero;
                rb.velocity = Vector3.SmoothDamp(rb.velocity, _vec2 * movementSpeed, ref zero, smoothValue);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerMovementEnabled)
        {
            if (collision.CompareTag("Enemy"))
            {
                // turn into an enemy
                GameManager.instance.playersComplete[gamepadID] = 2;
                rb.velocity = Vector2.zero;
                GetComponent<SpriteRenderer>().color = Color.green;
                enemyMovementManager.addNewEnemy(gameObject);
                gameObject.tag = "Enemy";
            }
            if (collision.CompareTag("Left"))
            {
                // if moving to this then this will mean that this player is finished
                if (!GameManager.instance.movingRight)
                {
                    GameManager.instance.playersComplete[gamepadID] = 1;
                    rb.velocity = Vector2.zero;
                    enemyMovementManager.updatePlayers();
                }
            }
            else if (collision.CompareTag("Right"))
            {
                if (GameManager.instance.movingRight)
                {
                    print("ID: " + gamepadID);
                    GameManager.instance.playersComplete[gamepadID] = 1;
                    rb.velocity = Vector2.zero;
                    enemyMovementManager.updatePlayers();
                }
            }
        }
    }


}
