using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnnouncements : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPlayerSelect(int playerNumber)
    {
        audioSource.PlayOneShot(audioClips[playerNumber]);
        audioSource.PlayOneShot(audioClips[4]);
    }
}
