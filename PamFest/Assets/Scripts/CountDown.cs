using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public bool canStart = false;
    public static CountDown instance;
    public float animSpeed;

    public void Awake()
    {
        instance = this;
        StartCoroutine(killAll());
    }
    public IEnumerator killAll()
    {
        yield return new WaitForSeconds(animSpeed);
        canStart = true;
        gameObject.SetActive(false);
    }

}
