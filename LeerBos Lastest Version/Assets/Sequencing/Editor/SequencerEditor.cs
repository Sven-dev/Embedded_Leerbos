using Sequencing;
using UnityEditor;
using UnityEngine;
using Array = System.Array;
using Sequencer = Sequencing.Sequencer;

namespace Editor
{
    [CustomEditor(typeof(Sequencer))]
    public class SequencerEditor : UnityEditor.Editor
    {
        private bool _showSeq;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Sequencer seq = (Sequencer) target;
            GUILayout.BeginHorizontal();
            _showSeq = EditorGUILayout.Foldout(_showSeq, "Sequencables");
            EditorGUI.indentLevel++;
            if (_showSeq)
            {
                seq.UseDefaultInterval = EditorGUILayout.Toggle("Use default interval", seq.UseDefaultInterval);
                if (seq)
                {
                    EditorGUI.BeginDisabledGroup(!seq.UseDefaultInterval);
                    seq.DefaultInterval = EditorGUILayout.FloatField(seq.DefaultInterval, GUILayout.MaxWidth(100));
                    EditorGUI.EndDisabledGroup();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Size");
                EditorGUI.BeginChangeCheck();
                EditorGUI.indentLevel--;
                int size = EditorGUILayout.IntField(seq.Sequencables.Length);
                EditorGUI.indentLevel++;
                if (EditorGUI.EndChangeCheck())
                {
                    Array.Resize(ref seq.Sequencables, size);
                }

                GUILayout.EndHorizontal();

                for (int i = 0; i < size; i++)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Element " + i);
                    EditorGUI.indentLevel--;
                    seq.Sequencables[i] = (Sequencable) EditorGUILayout.ObjectField(seq.Sequencables[i], typeof(Sequencable), true);
                    if (seq.Sequencables[i] != null)
                    {
                        EditorGUI.BeginDisabledGroup(seq.UseDefaultInterval);
                        seq.Sequencables[i].IntervalAfter 
                            = EditorGUILayout.FloatField(seq.UseDefaultInterval ? seq.DefaultInterval : seq.Sequencables[i].IntervalAfter);   
                        EditorGUI.EndDisabledGroup();
                    }
                    EditorGUI.indentLevel++;
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;
        }
    }
}