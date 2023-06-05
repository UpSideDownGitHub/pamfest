using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Vector3[] spawnPositions;
    public GameObject playerPrefab;
    public string[] names;
    public Color[] colors;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var temp = PlayerInput.Instantiate(playerPrefab, 0, null, -1, pairWithDevice: Gamepad.all[i]);
            temp.gameObject.transform.position = spawnPositions[i];
            GameManager.instance.players.Add(temp.gameObject.GetComponent<Player>());
            temp.gameObject.name = names[i];
            temp.gameObject.GetComponent<SpriteRenderer>().color = colors[i];
        }
        

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            print(Gamepad.all[i].displayName);
        }

        /*
        var p2 = PlayerInput.Instantiate(playerPrefab, 1, null, -1, pairWithDevice: Gamepad.all[1]);
        p2.gameObject.transform.position = spawnPositions[1];
        GameManager.instance.players.Add(p2.gameObject.GetComponent<Player>());
        p2.gameObject.name = "P2";
        p2.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        var p3 = PlayerInput.Instantiate(playerPrefab, 2, null, -1, pairWithDevice: Gamepad.all[2]);
        p3.gameObject.transform.position = spawnPositions[2];
        GameManager.instance.players.Add(p3.gameObject.GetComponent<Player>());
        p3.gameObject.name = "P3";
        p3.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        var p4 = PlayerInput.Instantiate(playerPrefab, 3, null, -1, pairWithDevice: Gamepad.all[3]);
        p4.gameObject.transform.position = spawnPositions[3];
        GameManager.instance.players.Add(p4.gameObject.GetComponent<Player>());
        p4.gameObject.name = "P4";
        p4.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
    */
        }
}
