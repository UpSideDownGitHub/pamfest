using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDestroy : MonoBehaviour
{
    public float givenTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, givenTime);
    }
}
