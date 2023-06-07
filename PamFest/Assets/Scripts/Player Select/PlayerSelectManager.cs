using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSelectManager : MonoBehaviour
{
    public GameObject[] playerSelectPanels;
    public string[] names;

    public List<bool> confirmed = new List<bool>();

    public static PlayerSelectManager instance;

    public GameObject countDownObjects;
    public float countDownTime;
    public bool called = false;

    void Awake()
    {
        instance = this;
        confirmed.Clear();
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (i > 3)
                return;
            var temp = PlayerInput.Instantiate(playerSelectPanels[i], 0, null, -1, pairWithDevice: Gamepad.all[i]);
            temp.gameObject.name = names[i];
            confirmed.Add(false);
    
        }
    }

    public void Update()
    {
        for (int i = 0; i < confirmed.Count; i++)
        {
            if (!confirmed[i])
                return;
        }
        if (!called)
            StartCoroutine (AfterSelect());
    }
    public IEnumerator AfterSelect()
    {
        // play7e rthe animation]
        // wait for anim to finish the load the next scene
        countDownObjects.SetActive(true);
        yield return new WaitForSeconds(1f + countDownTime);
        SceneManager.LoadScene("MainScene");
    }
}
