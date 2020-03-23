using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balloons.Scripts
{
    public class BalloonStack : MonoBehaviour
    {
        public Transform StackOrigin;
        public BalloonPositions Positions;
        
        private int _slotsFilled = 0;
        public Transform NextSlot()
        {
            var slot = Positions.Positions[_slotsFilled];
            _slotsFilled++;
            if (_slotsFilled >= Positions.Positions.Length) _slotsFilled = 0;
            return slot;
        }
    }
}