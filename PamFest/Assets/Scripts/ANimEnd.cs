using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANimEnd : MonoBehaviour
{
    public GameObject MenuPOP;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (AnimOver());
    }

    IEnumerator AnimOver()
    {
        yield return new WaitForSeconds(5f);
        MenuPOP.SetActive(true);
    }
    
}
