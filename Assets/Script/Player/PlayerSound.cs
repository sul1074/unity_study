using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip move;
    public AudioClip jump;
    public AudioClip teleport;
    public AudioClip buy;
    public AudioClip noMoney;
    public AudioClip attack;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveSound()
    {
        if(!audio.isPlaying)
        {
            audio.clip = move;
            audio.PlayOneShot(move);
        }
    }
    public void jumpSound()
    {
        audio.clip = jump;
        audio.Play();
    }

    public void teleportSound()
    {
        audio.clip = teleport;
        audio.Play();
    }

    public void buySound()
    {
        audio.clip = buy;
        audio.Play();
    }

    public void noMoneySound()
    {
        audio.clip = noMoney;
        audio.Play();
    }

    public void attackSound()
    {
        audio.clip = attack;
        audio.Play();
    }
}
