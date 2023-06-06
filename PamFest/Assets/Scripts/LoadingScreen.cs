using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadDone());

            IEnumerator LoadDone()
            {
                yield return new WaitForSeconds(5f);
                SceneManager.LoadScene("MainScene");
            }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
