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
        int pos1Head = PlayerPrefs.GetInt("Player " + pos1.ToString(), -1);
        int pos2Head = PlayerPrefs.GetInt("Player " + pos2.ToString(), -1);
        int pos3Head = PlayerPrefs.GetInt("Player " + pos3.ToString(), -1);
        
        if (pos1Head != -1)
            actualPlayerHeads[0].sprite = playerHeads[pos1Head];
        else
            actualPlayerHeads[0].sprite = null;
        if (pos2Head != -1)
            actualPlayerHeads[1].sprite = playerHeads[pos2Head];
        else
            actualPlayerHeads[1].sprite = null;
        if (pos3Head != -1)
            actualPlayerHeads[2].sprite = playerHeads[pos3Head];
        else
            actualPlayerHeads[2].sprite = null;
    }
}
