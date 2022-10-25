using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceAudioScript : MonoBehaviour
{
    int sampleRate = 44100;
    [SerializeField]
    int durationInSecs = 10;

    AudioSource source;
    AudioClip audioClip;

    int position = 0;

    [SerializeField]
    float volume = 0.5f;

    [SerializeField]
    float frequency = 100f;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        GenerateAudioClip();

        source.clip = audioClip;
        source.Play();
    }
    void GenerateAudioClip()
    {
        int totalSamples = sampleRate * durationInSecs;
        audioClip = AudioClip.Create("AudioClip", totalSamples, 1, sampleRate, false, OnAudioRead, OnAudioSetPosition);
    }

    void OnAudioSetPosition(int newPosition)
    {
        this.position = newPosition;
    }

    void OnAudioRead(float[] data)
    {


        for (int count = 0; count < data.Length; count++)
        {
            data[count] = Mathf.Clamp(Mathf.Sin(CosineWave(2*frequency,position)*SineWave(frequency, position) + SineWave(0.5f * frequency, position) * CosineWave(5f * frequency, position)), -1f, 1f);
            this.position++;
        }
    }


    float SineWave(float frequency, int wavePosition)
    {
        float t = (float)wavePosition / sampleRate;
        return volume * Mathf.Sin(2 * Mathf.PI * frequency * t);
    }

    float CosineWave(float frequency, int wavePosition)
    {
        float t = (float)wavePosition / sampleRate;
        return volume * Mathf.Cos(2 * Mathf.PI * frequency * t);
    }
}
