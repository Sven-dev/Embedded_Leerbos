 using System;
 using UnityEditor;
 using UnityEngine;
 using UnityEngine.Events;

namespace Minigames
 {
     [System.Serializable]
     public class Round
     {
         public Answer[] Answers = new Answer[0];
         private RoundManager _rm;       
         
         public void Start(RoundManager rm)
         {
             _rm = rm;
         }

         public void Answer(Answer answer)
         {
             if (answer.IsCorrect)
             {
                 _rm.CompleteCurrentRound();
             }
             else Restart();
         }

         void Restart()
         {
         }
     }
 }