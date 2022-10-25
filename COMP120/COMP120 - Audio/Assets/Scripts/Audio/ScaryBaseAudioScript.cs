using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryBaseAudioScript : MonoBehaviour
{
    int sampleRate = 44100;
    [SerializeField]
    int durationInSecs = 4;

    AudioSource source;
    AudioClip audioClip;

    int position = 0;

    [SerializeField]
    float volume = 1f;

    [SerializeField]
    float echoDelayInSeconds = 1f;

    float[] frequencies;

    List<float> arrayOfSamplesPostTriangleWave;

    [SerializeField]
    GameObject player;
    bool hasPlayerEnteredZone = false;
    bool clipHasBeenPlayed = false;


    private void Start()
    {

        arrayOfSamplesPostTriangleWave = new List<float>();

        frequencies = new float[] {100f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};

        source = GetComponent<AudioSource>();

        GenerateAudioClip();

        source.clip = audioClip;
        //source.Play();

    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 6f)
        {
            hasPlayerEnteredZone = true;
        }

        if (clipHasBeenPlayed == false && hasPlayerEnteredZone == true)
        {
            source.Play();
            clipHasBeenPlayed = true;
            Invoke("TurnOffClipLoop", 8f);
        }

    }

    void TurnOffClipLoop()
    {
        source.loop = false;
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
            int index = (int)(position / (0.5f * sampleRate));
            data[count] = TriangleWave(frequencies[index], position);

            arrayOfSamplesPostTriangleWave.Add(data[count]);
            position++;
        }

        float[] dataWithEcho = Echo(data.Length);

        for (int count = 0; count < data.Length; count++)
        {
            data[count] = dataWithEcho[count];
        }
    }

    float TriangleWave(float frequency, int wavePosition)
    {
        float t = (float)wavePosition / sampleRate;
        return ((2 * volume) / Mathf.PI) * (Mathf.Asin(Mathf.Sin(2 * Mathf.PI * frequency * t)));
        
    }


    float[] Echo(int dataLength)
    {
        List<float> newData = new List<float>();
        int echoDelayInSamples = (int)(echoDelayInSeconds * sampleRate);

        for (int i = position - dataLength; i < arrayOfSamplesPostTriangleWave.Count; i++)
        {
            float replacementSample = arrayOfSamplesPostTriangleWave[i];

            if (i - echoDelayInSamples * 2 > 0)
            {
                replacementSample += arrayOfSamplesPostTriangleWave[i - echoDelayInSamples * 2] * 0.1f;
            }

            else if (i - echoDelayInSamples > 0)
            {
                replacementSample += arrayOfSamplesPostTriangleWave[i - echoDelayInSamples] * 0.3f;
            }

            newData.Add(replacementSample);
        }

        return newData.ToArray();
    }

}
