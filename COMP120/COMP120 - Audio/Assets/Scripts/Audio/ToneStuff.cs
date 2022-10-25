using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public int sampleRate = 44100;
    public int durationInSeconds = 1;
    public int position = 0;
    public int frequency = 440;

    private AudioSource source;
    private AudioClip generatedClip;

    // Start is called before the first frame update
    void Start()
    {
        int durationInSamples = sampleRate * durationInSeconds;
        generatedClip = AudioClip.Create("GeneratedAudio",
                durationInSamples, 1, sampleRate, true,
                OnAudioRead, OnAudioSetPosition);
        source = GetComponent<AudioSource>();
    }

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = SineWave(frequency, position, 10);
            position++;
            count++;
        }
    }

    void OnAudioSetPosition(int newPosition)
    {
        this.position = newPosition;
    }
    private float SineWave(float frequency, int wavePosition, int volume)
    {
        float t = (float)wavePosition / sampleRate;
        return volume * Mathf.Sin(2 * Mathf.PI * frequency * t);
    }

    void playAudio()
    {
        source.clip = generatedClip;
        source.Play();
    }

    void OnGUI()
    {
        Rect bounds = new Rect(10, 10, 150, 100);
        if (GUI.Button(bounds, "Play"))
        {
            playAudio();
        }
    }
}
