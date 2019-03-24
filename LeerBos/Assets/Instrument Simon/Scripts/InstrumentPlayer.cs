using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentPlayer : MonoBehaviour
{
    public VictoryScript VictoryScript;

    [HideInInspector]
    public bool Throwable;
    [HideInInspector]
    public bool Free;
    public int Rounds;
    public int Length;

    private Instrument[] Instruments;
    private List<Instrument> Sequence;
    private List<Instrument> Execution;

    private AudioSource CorrectAudio;

	// Use this for initialization
	void Start ()
    {
        CorrectAudio = GetComponent<AudioSource>();

        Throwable = false;
        Free = false;
        Instruments = GetComponentsInChildren<Instrument>();
        Sequence = new List<Instrument>();
        Execution = new List<Instrument>();

        GenerateSequence();
    }

    //Generate a new sequence, or add a new value
    private void GenerateSequence()
    {
        //While the sequence isn't complete
        while (Sequence.Count < Length)
        {
            //Get one of the instruments
            int rnd = Random.Range(0, Instruments.Length);
            Instrument next = Instruments[rnd];

            //Add it to the sequence
            print(next);
            Sequence.Add(next);       
        }

        //Play the sequence
        PlaySequence();
    }

    //play the sequence
    private void PlaySequence()
    {
        StartCoroutine(_PlaySequence());
    }

    private IEnumerator _PlaySequence()
    {
        Throwable = false;
        int index = 0;
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < Sequence.Count; i++)
        {
            Sequence[i].PlayConsonant(i);
            index++;
            while (Sequence[i].Audio.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.25f);
        }

        Throwable = true;
    }

    private void Correct()
    {
        StartCoroutine(_Correct());
    }

    private IEnumerator _Correct()
    {
        Throwable = false;
        Execution.Clear();

        yield return new WaitForSeconds(0.5f);
        CorrectAudio.Play();
        while (CorrectAudio.isPlaying)
        {
            yield return null;
        }

        if (Rounds > 0)
        {
            Length++;
            GenerateSequence();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Free = true;
            Throwable = true;
            VictoryScript.Enable();
        }
    }

    //Checks if the hit instrument is correct
    public void CheckInstrument(Instrument i)
    {
        int index = Execution.Count;
        if (Sequence[index] == i)
        {
            Execution.Add(i);
            i.PlayConsonant(index);
            if (Execution.Count == Sequence.Count)
            {
                Rounds--;
                Correct();
            }
        }
        else
        {
            Execution.Clear();
            i.PlayDissonant();
            PlaySequence();
        }
    }
}