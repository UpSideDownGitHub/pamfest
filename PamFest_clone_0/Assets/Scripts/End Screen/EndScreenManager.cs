using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public Sprite[] playerHeads;
    public Image[] actualPlayerHeads;
    
    // Start is called before the first frame update
    void Start()
    {
        int pos1 = PlayerPrefs.GetInt("First Place", -1);
        int pos2 = PlayerPrefs.GetInt("Second Place", -1);
        int pos3 = PlayerPrefs.GetInt("Third Place", -1);
        print(pos1);
        print(pos2);
        print(pos3);
        actualPlayerHeads[0].sprite = playerHeads[pos1];
        actualPlayerHeads[1].sprite = playerHeads[pos2];
        actualPlayerHeads[2].sprite = playerHeads[pos3];
    }
}
