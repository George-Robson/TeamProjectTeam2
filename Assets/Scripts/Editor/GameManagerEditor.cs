using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI() {
        serializedObject.Update();
        GameManager gameManager = (GameManager) target;

        GUILayout.BeginVertical();
        
        if (GUILayout.Button("Refresh"))
        {
            gameManager.gameStateList.Clear();
            for (int i = 0; i < gameManager.conditions.Count; i++)
            {
                gameManager.AddGameState(gameManager.conditions[i]);
            }
        }
        
        GUILayout.Label("Current Game States:");
        for (int i = 0; i < gameManager.gameStateList.Count; i++)
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label(gameManager.gameStateList[i].gameStateString);
            GUILayout.Toggle(gameManager.gameStateList[i].gameStateBool, "");
            
            GUILayout.EndHorizontal();
            
        }
        base.OnInspectorGUI();
        GUILayout.EndVertical();
    }
}
