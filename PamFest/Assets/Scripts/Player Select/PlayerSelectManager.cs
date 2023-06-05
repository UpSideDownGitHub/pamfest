using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectManager : MonoBehaviour
{
    public GameObject[] playerSelectPanels;
    public string[] names;


    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (i > 3)
                return;
            var temp = PlayerInput.Instantiate(playerSelectPanels[i], 0, null, -1, pairWithDevice: Gamepad.all[i]);
            temp.gameObject.name = names[i];
        }
    }
}
