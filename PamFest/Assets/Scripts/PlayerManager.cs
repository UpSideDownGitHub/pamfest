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

        /*
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            print(Gamepad.all[i].displayName);
        }
        */
    }
}
