using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    public void LoadScene(string sceneName)
    {

        StartCoroutine(SFXComp());

        IEnumerator SFXComp()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneName);
        }
    }

        public void QuitDaGame()
    {
        Application.Quit();
    }
}
