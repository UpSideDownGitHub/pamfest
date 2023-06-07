using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Vector2 _vec2;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float smoothValue;
    public bool playerMovementEnabled;
    public bool playerMovementDisabled;

    [Header("Cone Death")]
    public float disableTime;
    private float timeSinceDisable;
    public GameObject particleSystems;

    [Header("Enemyness")]
    public EnemyMovementManager enemyMovementManager;

    [Header("Gamepad Support")]
    public int gamepadID;

    [Header("Player Ring")]
    public SpriteRenderer ringSprite;
    public Color[] colors;

    public Animator cameraAnim;
    public Animator playerAnim;

    public GameObject hitEffect;

    public void Movement(InputAction.CallbackContext ctx)
    {

        _vec2 = ctx.ReadValue<Vector2>();
        if (_vec2.x > 0)
        {
            playerAnim.SetBool("isRunningRight", true);
            playerAnim.SetBool("isRunningLeft", false);
            playerAnim.SetBool("isIdle", false);
        }
        else if (_vec2.x < 0)
        {
            playerAnim.SetBool("isRunningRight", false);
            playerAnim.SetBool("isRunningLeft", true);
            playerAnim.SetBool("isIdle", false);
        }
        else
        {
            playerAnim.SetBool("isRunningRight", false);
            playerAnim.SetBool("isRunningLeft", false);
            playerAnim.SetBool("isIdle", true);
        }
    }

    private void Start()
    {
        enemyMovementManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyMovementManager>();
        cameraAnim = Camera.main.gameObject.GetComponent<Animator>();
        ringSprite.color = colors[gamepadID];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovementDisabled && Time.time > disableTime + timeSinceDisable)
        {
            playerMovementDisabled = false;
        }
        if (playerMovementEnabled && !playerMovementDisabled)
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitEffect, collision.collider.ClosestPoint(transform.position), Quaternion.identity);
        if (collision.gameObject.CompareTag("cone"))
        {
            playerMovementDisabled = true;
            rb.velocity = Vector2.zero;
            timeSinceDisable = Time.time;
            Instantiate(particleSystems, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerMovementEnabled)
        {
            Instantiate(hitEffect, collision.ClosestPoint(transform.position), Quaternion.identity);
            if (collision.CompareTag("Enemy"))
            {
                // turn into an enemy
                
                cameraAnim.Play("CameraShakeAnim");
                print("work");
                                    
                GameManager.instance.playersComplete[gamepadID] = 2;
                rb.velocity = Vector2.zero;
                enemyMovementManager.addNewEnemy(gameObject);
                gameObject.tag = "Enemy";
                ringSprite.enabled = false;

                //Play anim
                //cameraAnim.SetBool("isShaking", true);
            }
            if (collision.CompareTag("Left"))
            {
                // if moving to this then this will mean that this player is finished
                if (!GameManager.instance.movingRight)
                {
                    GameManager.instance.playersComplete[gamepadID] = 1;
                    GameManager.instance.playerCrossedLine(gamepadID);
                    rb.velocity = Vector2.zero;
                    enemyMovementManager.updatePlayers();
                }
            }
            else if (collision.CompareTag("Right"))
            {
                if (GameManager.instance.movingRight)
                {
                    GameManager.instance.playersComplete[gamepadID] = 1;
                    GameManager.instance.playerCrossedLine(gamepadID);
                    rb.velocity = Vector2.zero;
                    enemyMovementManager.updatePlayers();
                }
            }
        }
    }


}
