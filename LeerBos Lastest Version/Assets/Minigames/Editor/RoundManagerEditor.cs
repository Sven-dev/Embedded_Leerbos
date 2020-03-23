using System;
using Minigames;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Round = Minigames.Round;
using RoundManager = Minigames.RoundManager;

namespace Editor
{
    [CustomEditor(typeof(RoundManager))]
    public class RoundManagerEditor : UnityEditor.Editor
    {
        private int _roundCount;
        public override void OnInspectorGUI()
        {
            RoundManager rm = (RoundManager) target;
            
            base.OnInspectorGUI();
            _answerCount = rm.Interactables != null ? rm.Interactables.Length : 0;
            if (rm.Sampler.Data == null) rm.Sampler.Data = new Object[3];
            
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PrefixLabel("Amount of rounds");
                _roundCount = EditorGUILayout.IntField(_roundCount);
            GUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                Array.Resize(ref rm.Rounds, _roundCount);
                Array.Resize(ref rm.Sampler.Data, rm.Rounds.Length);
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Provide data");
            rm.Style = (DataProvisionStyle)EditorGUILayout.EnumPopup(rm.Style);

            if (rm.Style == DataProvisionStyle.External)
            {
                rm.Provider = (ScriptableObject) EditorGUILayout.ObjectField(rm.Provider, typeof(object), true);
            }
            GUILayout.EndHorizontal();
            
            for (int i = 0; i < rm.Rounds.Length; i++)
            {
                if(rm.Rounds[i] == null) rm.Rounds[i] = new Round();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Round " + (i + 1), EditorStyles.boldLabel);
                if (rm.Style == DataProvisionStyle.Manual)
                {
                    GUILayout.Label("Answer");
                    GUILayout.Label("Data");
                }
                GUILayout.EndHorizontal();
                AddRound(rm, i);
            }
        }
        
        private int _answerCount;
        void AddRound(RoundManager rm, int i)
        {
            Round r = rm.Rounds[i];
            if (r == null) return;
            Array.Resize(ref r.Answers, _answerCount);
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(rm.Sampler.name);
            rm.Sampler.Data[i] = 
                EditorGUILayout.ObjectField(rm.Sampler.Data[i], typeof(object), true);         
            GUILayout.EndHorizontal();
            
            for (int j = 0; j < _answerCount; j++)
            {
                if (!rm.Interactables[j])
                {
                    Array.Resize(ref r.Answers, j);
                    continue;
                }
                GUILayout.BeginHorizontal();
                r.Answers[j].Interactable = rm.Interactables[j];
                EditorGUILayout.LabelField(rm.Interactables[j].name);
                if (rm.Style == DataProvisionStyle.Manual)
                {
                    r.Answers[j].IsCorrect =
                        EditorGUILayout.Toggle(r.Answers[j].IsCorrect);
                    r.Answers[j].Data = 
                        EditorGUILayout.ObjectField(r.Answers[j].Data, typeof(object), true);                    
                } 
                GUILayout.EndHorizontal();
            }
        }
    }
}