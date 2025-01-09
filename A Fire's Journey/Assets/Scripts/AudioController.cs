using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    
    public AudioSource voiceOver;
    public AudioSource music;
    public AudioSource[] sfx;
    public float[,] voiceOverTimeStamps = {//based on index, 1st index is row, two floats are times
        {0,0},
        {1.7f, 4}, //1
        {5, 9}, 
        {9.8f, 12.75f},
        {13.4f, 18},
        //section B (Grow fire)
        {20.5f, 25.5f}, //5
        {25.9f, 31.5f},
        {32.2f, 34.2f},
        {34.7f, 37.8f},
        //Section C (Fan Flames)
        {2.5f, 7},
        {2.5f, 7},
        {2.5f, 7},
        {2.5f, 7},
        } 
    
    
    ;

    public event Action OnAudioFinished;

    public void PlayVoiceOverAudio(int index)
    {
        float startT = voiceOverTimeStamps[index, 0];
        float endT = voiceOverTimeStamps[index, 1]; 
        voiceOver.time = startT;  // Set the start time for playback
        voiceOver.Play();            // Start playing the audio
        StartCoroutine(StopAudioAfterTime(endT - startT));
    }

    private IEnumerator StopAudioAfterTime(float duration)
    {
        // Wait for the duration of the clip segment to finish
        yield return new WaitForSeconds(duration);
        voiceOver.Stop();  // Stop the audio when the segment is over
        
        OnAudioFinished?.Invoke();
    }


}
