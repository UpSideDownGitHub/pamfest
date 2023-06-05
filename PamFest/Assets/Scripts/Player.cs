using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 _vec2;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float smoothValue;

    public void Movement(InputAction.CallbackContext ctx) => _vec2 = ctx.ReadValue<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_vec2 != Vector2.zero)
        {
            print(_vec2);
            Vector3 zero = Vector3.zero;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, _vec2 * movementSpeed, ref zero, smoothValue);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


}
