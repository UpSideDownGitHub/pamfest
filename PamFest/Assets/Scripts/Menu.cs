using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public Animator anim;

    public void LoadScene(string sceneName)
    {
        if (sceneName == "Loading")
        {
            // check to see if there are more than 1 controller connected
            if (Gamepad.all.Count < 2)
            {
                anim.Play("New State 0");
                // cant play the game as not enough controllers
                // need to show a popup that says please connect another controller
                return;
            }
        }
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
