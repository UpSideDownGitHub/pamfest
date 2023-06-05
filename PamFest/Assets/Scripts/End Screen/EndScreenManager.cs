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
        
        if (pos1 != -1)
            actualPlayerHeads[0].sprite = playerHeads[pos1];
        else
            actualPlayerHeads[0].sprite = null;
        if (pos2 != -1)
            actualPlayerHeads[1].sprite = playerHeads[pos2];
        else
            actualPlayerHeads[1].sprite = null;
        if (pos3 != -1)
            actualPlayerHeads[2].sprite = playerHeads[pos3];
        else
            actualPlayerHeads[2].sprite = null;
    }
}
