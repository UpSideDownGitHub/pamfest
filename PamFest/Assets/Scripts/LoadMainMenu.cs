using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(EndDone());




        IEnumerator EndDone()
        {
            yield return new WaitForSeconds(7.5f);
            SceneManager.LoadScene("MainMenu");
        }

    }
     
}
