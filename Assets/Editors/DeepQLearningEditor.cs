using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;

[CustomEditor(typeof(DeepQLearning))]
public class DeepQLearningEditor : Editor
{
    // Start is called before the first frame update
    DeepQLearning deepQLearning;
    private void OnEnable()
    {
        deepQLearning = (DeepQLearning)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Start DeepQLearning", GUILayout.Height(35)))
        {
            string path = Application.dataPath + "/python/log_names.py";
            PythonRunner.RunFile(path);
        }
    }

}
