using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPlayer : MonoBehaviour
{
    private float _volume = 0.5f;
    [SerializeField] private int _sampleRate = 44100;

    // Format of each note in the sequence is //* Note Octave / Duration
    private string[] _playSequence = new string[] { "B2/1", "A#2/1", "G2/1", "A#2/1", "E2/1", "D2/1", "C2/1", "D2/1" };
    private Note[] _sequenceInNotes;
    private float[] _playableSequence;

    private AudioSource _source;
    private AudioClip _generatedClip;
    private int _position = 0;

    private static string[] _pitchFromA = new string[] { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
    private class Note
    {
        public float frequency { get; set; }
        public float duration { get; set; }
        public Note(float frequency, float duration)
        {
            this.frequency = frequency;
            this.duration = duration;
        }
    }

    /// <summary>
    /// Gets the duration of a sample.
    /// </summary>
    /// <param name="noteSequence">An Array of <see cref="Note"/> containing a sequence.</param>
    /// <returns>A <see cref="float"/> containing the duration of the sample.</returns>
    private float GetSampleLengthFromNoteSequence(Note[] noteSequence)
    {
        float lengthInSeconds = 0;
        foreach (Note note in noteSequence)
        {
            lengthInSeconds += note.duration;
        }
        return lengthInSeconds;
    }

    /// <summary>
    /// Converts a Note Sequence into a playable format.
    /// </summary>
    /// <param name="noteSequence">An Array of <see cref="Note"/> containing a sequence.</param>
    /// <returns>A <see cref="float"/> array containing a playable format.</returns>
    private float[] NoteSequenceToPlayableSequence(Note[] noteSequence)
    {
        float[] playableSequenceBuilder = new float[0];
        foreach (Note note in noteSequence)
        {
            playableSequenceBuilder = SpliceAudio(playableSequenceBuilder, ConvertToADSREnvelop(SineWaveNote(note), 1f, 0.1f, 0.2f, 1f, 0.5f, 0.2f));
        }
        return playableSequenceBuilder;
    }

    /// <summary>
    /// Converts a readable/programmable sequence into a note sequence.
    /// </summary>
    /// <param name="sequence">Programmed Sequence.</param>
    /// <returns>A <see cref="Note"/> array containing a sequence.</returns>
    private Note[] ConvertSequenceToNoteSequence(string[] sequence)
    {
        List<Note> notes = new List<Note>();
        foreach (string note in sequence)
        {
            string[] splitNote = note.Split('/');
            notes.Add(new Note(GetFrequencyFromLetter(splitNote[0]), float.Parse(splitNote[1])));
        }
        return notes.ToArray();
    }

    /// <summary>
    /// Generates a mono SineWave based on a note.
    /// </summary>
    /// <param name="note">Note to generate.</param>
    /// <returns>A <see cref="float"/> array containing the mono SineWave frequency.</returns>
    private float[] SineWaveNote(Note note)
    {
        List<float> sineWaveNote = new List<float>();
        for (int i = 0; i < _sampleRate * note.duration; i++)
        {
            float wavePosition = (float)i / _sampleRate;
            sineWaveNote.Add(_volume * Mathf.Sin(2 * Mathf.PI * note.frequency * wavePosition));
        }
        return sineWaveNote.ToArray();
    }

    /// <summary>
    /// Splices two samples together.
    /// </summary>
    /// <param name="firstSample">An array containing a playable sequence.</param>
    /// <param name="secondSample">An array containing a playable sequence.</param>
    /// <returns>A <see cref="float"/> array containing a playable format.</returns>
    private float[] SpliceAudio(float[] firstSample, float[] secondSample)
    {
        List<float> spliceAudio = new List<float>();
        for (int i = 0; i < firstSample.Length; i++)
        {
            spliceAudio.Add(firstSample[i]);
        }
        for (int i = 0; i < secondSample.Length; i++)
        {
            spliceAudio.Add(secondSample[i]);
        }
        return spliceAudio.ToArray();
    }

    /// <summary>
    /// Converts a Sample into a ADSR Envelop.
    /// </summary>
    /// <param name="sample">The sample to be converted.</param>
    /// <param name="attackLevel">The attack level peak.</param>
    /// <param name="attackTime">The duration of the attack.</param>
    /// <param name="decayTime">The duration of the decay.</param>
    /// <param name="sustainLevel">The level at which it sustains.</param>
    /// <param name="sustainTime">The duration of sustain.</param>
    /// <param name="releaseTime">The duration of release.</param>
    /// <returns>A <see cref="float"/> array containing a playable format.</returns>
    private float[] ConvertToADSREnvelop(float[] sample, float attackLevel, float attackTime, float decayTime, float sustainLevel, float sustainTime, float releaseTime)
    {
        List<float> adsr = new List<float>();
        int numOfSamples = sample.Length;
        int attackLength = (int)(numOfSamples * attackTime);
        int decayLength = (int)(numOfSamples * decayTime);
        int sustainLength = (int)(numOfSamples * sustainTime);
        int releaseLength = (int)(numOfSamples * releaseTime);
        int progress = 0;
        for (int i = 0; i < attackLength; i++)
        {
            adsr.Add(sample[i] * Mathf.Lerp(0, attackLevel, progress / attackLength));
            progress++;
        }
        for (int i = attackLength; i < attackLength + decayLength; i++)
        {
            adsr.Add(sample[i] * Mathf.Lerp(attackLength, sustainLength, progress / decayLength));
            progress++;
        }
        for (int i = attackLength + decayLength; i < attackLength + decayLength + sustainLength; i++)
        {
            adsr.Add(sample[i] * sustainLevel);
            progress++;
        }
        for (int i = attackLength + decayLength + sustainLength; i < attackLength + decayLength + sustainLength + releaseLength; i++)
        {
            adsr.Add(sample[i] * Mathf.Lerp(sustainLevel, 0, progress / numOfSamples));
            progress++;
        }
        for (int i = attackLength + decayLength + sustainLength + releaseLength; i < numOfSamples; i++)
        {
            adsr.Add(sample[i] * 0);
        }
        return adsr.ToArray();
    }

    /// <summary>
    /// Scales the volume of a sample.
    /// </summary>
    /// <param name="sample">The sample to be scaled.</param>
    /// <param name="factor">The scale amount.</param>
    /// <returns>A <see cref="float"/> array containing a playable format.</returns>
    private float[] ScaleAmplitude(float[] sample, float factor)
    {
        List<float> scaledSample = new List<float>();

        for (int i = 0; i < sample.Length; i++)
        {
            float amplitude = sample[i] * factor;
            amplitude = Mathf.Clamp(amplitude, -1, 1);
            scaledSample.Add(amplitude);
        }
        return scaledSample.ToArray();
    }

    /// <summary>
    /// Gets the note frequency from the letter and octave.
    /// </summary>
    /// <param name="letterWithOctave">The letter and octave of the note.</param>
    /// <returns>A <see cref="float"> of the note frequency.</returns>
    private float GetFrequencyFromLetter(string letterWithOctave)
    {
        float baseNote = 440.0f;
        float octave, key;
        octave = (letterWithOctave.Length == 3) ? (float)Char.GetNumericValue(letterWithOctave[2]) : (float)Char.GetNumericValue(letterWithOctave[1]); 
        key = Array.IndexOf(_pitchFromA, letterWithOctave.Substring(0, letterWithOctave.Length - 1)); //Finds the position of the pitch in the scale
        key = (key < 3) ? key + 12 + ((octave - 1) * 12) + 1 : key + ((octave - 1) * 12) + 1; //Checks to see if the note is sharp if it is increase the semitone by 1
        return baseNote * Mathf.Pow(2.0f, (key - 49) / 12);
    }

    private void Start()
    {
        _playableSequence = ScaleAmplitude(NoteSequenceToPlayableSequence(ConvertSequenceToNoteSequence(_playSequence)), 0.3f);
        int durationInSamples = (int)(_sampleRate * GetSampleLengthFromNoteSequence(ConvertSequenceToNoteSequence(_playSequence)));
        _generatedClip = AudioClip.Create("GeneratedAudio",
                                        durationInSamples,
                                        1,
                                        _sampleRate,
                                        true,
                                        OnAudioRead,
                                        OnAudioSetPosition);
        _source = GetComponent<AudioSource>();
        _source.clip = _generatedClip;
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
            data[count] = _playableSequence[_position];
            _position++;
            count++;
        }
    }

    /// <summary>
    /// The callback function that sets the position of where it is currently playing.
    /// </summary>
    /// <param name="newPosition">The new position to play.</param>
    void OnAudioSetPosition(int newPosition)
    {
        this._position = newPosition;
    }
}
