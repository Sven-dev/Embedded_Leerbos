using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sequencing
{
    public class Sequencer : MonoBehaviour
    {
        [HideInInspector] public Sequencable[] Sequencables;
        public float InitialInterval;
        public float IntervalAfterInterrupt;
        [HideInInspector] public bool UseDefaultInterval;
        [HideInInspector] public float DefaultInterval;

        private Sequencable _currentlySequencing;
        private bool _interrupted;

        private int _counter;
        
        void Start()
        {
            StartCoroutine(Execute());
        }

        public IEnumerator Execute()
        {
            yield return Wait(InitialInterval);
            while (true)
            {
                if (_interrupted) yield return null;
                else
                {
                    _currentlySequencing = Sequencables[_counter % Sequencables.Length];
                    _counter++;
                    yield return _currentlySequencing.GiveTurn();
                }
            }


        }

        public void Interrupt(Sequencable input = null)
        {
            StartCoroutine(InterruptSequencer(input));
        }

        IEnumerator InterruptSequencer(Sequencable input)
        {
            _interrupted = true;
            if(_currentlySequencing) _currentlySequencing.Interrupt();
            if (input) yield return input.GiveTurn();
            _counter = 0;
            yield return Wait(IntervalAfterInterrupt);
            _interrupted = false;
        }

        IEnumerator Wait(float f)
        {
            yield return new WaitForSeconds(f);
        }

    }
}