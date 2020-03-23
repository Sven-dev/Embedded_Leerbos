using System;
using UnityEditor;
using UnityEngine;

namespace Interval.Editor
{
    [CustomEditor(typeof(ProgressItemFactory))]
    public class ProgressItemFactoryEditor : UnityEditor.Editor
    {
        private bool _showSeq;
        public override void OnInspectorGUI()
        {
            ProgressItemFactory pif = (ProgressItemFactory) target;
            base.OnInspectorGUI();
            var length = pif.Rounds.Rounds.Length;
            if (pif.ProgressItems == null) pif.ProgressItems = new ProgressItem[length];
            if (pif.ProgressItems.Length != length) Array.Resize(ref pif.ProgressItems, length);

            GUILayout.BeginHorizontal();
            _showSeq = EditorGUILayout.Foldout(_showSeq, "ProgressItems");
            EditorGUI.indentLevel++;
            if (_showSeq)
            {
                GUILayout.EndHorizontal();
                for (int i = 0; i < pif.ProgressItems.Length; i++)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Element " + i);
                    EditorGUI.indentLevel--;
                    pif.ProgressItems[i] =
                        (ProgressItem) EditorGUILayout.ObjectField(pif.ProgressItems[i], typeof(ProgressItem), true);
                    GUILayout.EndHorizontal();
                    EditorGUI.indentLevel++;
                }
            }
            else
            {
                GUILayout.EndHorizontal();
            }

            }
        }
    }