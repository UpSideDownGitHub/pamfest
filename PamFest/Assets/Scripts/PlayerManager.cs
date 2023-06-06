using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Vector3[] spawnPositions;
    public GameObject playerPrefab;
    public string[] names;
    public Sprite[] players;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var temp = PlayerInput.Instantiate(playerPrefab, 0, null, -1, pairWithDevice: Gamepad.all[i]);
            temp.gameObject.transform.position = spawnPositions[i];
            GameManager.instance.players.Add(temp.gameObject.GetComponent<PlayerMovement>());
            temp.gameObject.name = names[i];

            // set the sprite to the selected sprite
            temp.gameObject.GetComponent<SpriteRenderer>().sprite = players[PlayerPrefs.GetInt("Player " + i, 0)];
        }

        /*
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            print(Gamepad.all[i].displayName);
        }
        */
    }
}
