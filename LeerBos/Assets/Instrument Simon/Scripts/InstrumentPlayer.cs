using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentPlayer : MonoBehaviour
{
    public VictoryScript VictoryScript;

    public bool Throwable;
    public bool Free;
    public int Rounds;
    public int SequenceLength;
    public float Pitch;

    private List<Instrument> Sequence;
    private List<Instrument> Execution;

    private AudioSource CorrectAudio;

	// Use this for initialization
	void Start ()
    {
        CorrectAudio = GetComponent<AudioSource>();

        Throwable = false;
        Free = false;
        Sequence = new List<Instrument>();
        Execution = new List<Instrument>();

        GenerateSequence();
    }

    private void GenerateSequence()
    {
        Execution.Clear();
        while (Sequence.Count < SequenceLength)
        {
            int rnd = Random.Range(0, transform.childCount-1);
            Sequence.Add(transform.GetChild(rnd).GetComponent<Instrument>());
        }

        PlaySequence();
    }

    private void PlaySequence()
    {
        StartCoroutine(_PlaySequence());
    }

    private IEnumerator _PlaySequence()
    {
        Throwable = false;
        Pitch = 1;
        yield return new WaitForSeconds(1f);
        foreach (Instrument i in Sequence)
        {
            i.PlayConsonant(Pitch);
            while (i.Audio.isPlaying)
            {
                yield return null;
            }

            Pitch += 0.1f;
            yield return new WaitForSeconds(0.5f);
        }

        Pitch = 1;
        Throwable = true;
    }

    private void Correct()
    {
        StartCoroutine(_Correct());
    }

    private IEnumerator _Correct()
    {
        Throwable = false;
        Pitch = 1;

        yield return new WaitForSeconds(0.75f);
        CorrectAudio.Play();
        while (CorrectAudio.isPlaying)
        {
            yield return null;
        }

        if (Rounds > 0)
        {
            SequenceLength++;
            GenerateSequence();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Free = true;
            VictoryScript.Enable();
        }
    }

    //Checks if the hit instrument is correct
    public void CheckInstrument(Instrument i)
    {
        //If you 
        if (Throwable)
        {
            int index = Execution.Count;
            if (Sequence[index] == i)
            {
                Pitch += 0.1f;
                i.PlayConsonant(Pitch);
                Execution.Add(i);
                if (Execution.Count == Sequence.Count)
                {
                    Rounds--;
                    Correct();
                }
            }
            else
            {
                Pitch = 1;
                Execution.Clear();
                i.PlayDissonant();
                PlaySequence();
            }
        }
        else if (Free)
        {
            i.PlayConsonant();
        }
    }
}