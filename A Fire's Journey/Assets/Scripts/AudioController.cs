using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    
    public AudioSource voiceOver;
    public AudioSource music;
    public AudioSource[] sfx;
    public AudioSource fireSound;
    public AudioSource fireWhoosh;
    private int sampleStart;
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
        {43f, 47.5f}, //9
        {48.1f, 50f},
        {50.3f, 52.5f},
        {54f, 57f},
        //Section D1 (Burning)
        {60.2f,66.3f}, //13
        {67f,69.3f},
        {69.8f,72.2f},
        {73.3f,78.5f},
        {79f,85.2f},
        {85.8f,87.4f},
        //Section D2 (Light)
        {89.5f,91.8f}, //19
        {92.1f,95.1f},
        {95.72f,100.5f},
        {101.3f,106.3f},
        //Section E (Final)
        {108.6f,111.4f}, //23
        {111.7f,114f},
        {114.5f,117.5f},
        {118.5f,120f},
        };

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
