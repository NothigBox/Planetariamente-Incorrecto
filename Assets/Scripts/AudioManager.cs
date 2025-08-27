using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource final;
    [SerializeField] AudioSource textLoop;
    [SerializeField] AudioSource buttonClick;

    [Header("Audio Clips")]
    [SerializeField] AudioClip reRollClick;
    [SerializeField] AudioClip[] buttonClicks;
    [SerializeField] AudioClip[] finals;

    private void Awake()
    {
        TextEffect.OnTextWritting = (isActive) => SetTextLoop(isActive);
    }

    public void PlayButtonClick() 
    {
        buttonClick.clip = buttonClicks[Random.Range(0, buttonClicks.Length)];
        buttonClick.Play();   
    }

    public void PlayReRollClick() 
    {
        buttonClick.clip = reRollClick;
        buttonClick.Play();   
    }

    public void SetTextLoop(bool isPlaying) 
    {
        if (isPlaying) textLoop.Play();
        else textLoop.Stop();
    }

    public void PlayGoodFinal() 
    {
        final.clip = finals[0];
        final.Play();
    }

    public void PlayBadFinal()
    {
        final.clip = finals[1];
        final.Play();
    }
}
