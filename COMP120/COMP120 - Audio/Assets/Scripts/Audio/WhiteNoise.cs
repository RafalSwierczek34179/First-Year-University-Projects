using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteNoise : MonoBehaviour
{
    private int sampleRate = 44100;
    private int durationInSeconds = 1;
    private AudioSource source;
    private AudioClip generatedClip;
    private int position = 0;


    private float[] whiteNoise;
    void Start()
    {
        whiteNoise = GenerateWhiteNoise(durationInSeconds, 0);
        int durationInSamples = sampleRate * durationInSeconds;
        generatedClip = AudioClip.Create("GeneratedAudio",
                                        durationInSamples,
                                        1,
                                        sampleRate,
                                        true,
                                        OnAudioRead,
                                        OnAudioSetPosition);
        source = GetComponent<AudioSource>();
        source.clip = generatedClip;
        source.Play();
    }

    /// <summary>
    /// The Callback function that reads the playable sequence.
    /// </summary>
    /// <param name="data">A playable sequence.</param>
    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = whiteNoise[position];
            position++;
            count++;
        }
    }

    /// <summary>
    /// The callback function that sets the position of where it is currently playing.
    /// </summary>
    /// <param name="newPosition">The new position to play.</param>
    void OnAudioSetPosition(int newPosition)
    {
        this.position = newPosition;
    }

    /// <summary>
    /// Generates white noise in a playable format.
    /// </summary>
    /// <param name="seconds">The duration of the sample in seconds.</param>
    /// <param name="volume">The amplitude of the white noise.</param>
    /// <returns>A <see cref="float"/> array containing a playable format.</returns>
    private float[] GenerateWhiteNoise(int seconds, float volume)
    {
        List<float> samples = new List<float>();
        for (int i = 0; i < sampleRate * seconds; i++)
        {
            samples.Add(Random.Range(-1, 1) * volume);
        }
        return samples.ToArray();
    }
    private void Update()
    {
        whiteNoise = GenerateWhiteNoise(durationInSeconds,
                                        CalculateVolumeByDistance(1f, 35f, Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position)));
    }

    /// <summary>
    /// Calculates the volume based on how close an object is within set boundaries.
    /// </summary>
    /// <param name="minDistance">Minimum distance an object needs to be for max volume.</param>
    /// <param name="maxDistance">Maximum distance an object needs to be for least volume.</param>
    /// <param name="distance">The distance between the two objects.</param>
    /// <returns>Returns a <see cref="float"/> between 0.0 and 1.0.</returns>
    private float CalculateVolumeByDistance(float minDistance, float maxDistance, float distance)
    {
        return Mathf.InverseLerp(maxDistance, minDistance, distance);
    }
}
