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

        #if UNITY_EDITOR
        if (GUILayout.Button("Refresh (Do not refresh when testing in playmode)"))
        {
            gameManager.gameStateList.Clear();
            for (int i = 0; i < gameManager.conditions.Count; i++)
            {
                gameManager.AddGameState(gameManager.conditions[i]);
            }
        }
        #endif
        
        GUILayout.Label("Current Game States:");
        foreach (var gameState in gameManager.gameStateList)
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label(gameState.gameStateString);
            GUILayout.Toggle(gameState.gameStateBool, "");
            if (GUILayout.Button("Toggle"))
            {
                gameManager.SetGameState(gameState.gameStateString, !gameState.gameStateBool);
            }
            
            GUILayout.EndHorizontal();
        }
        base.OnInspectorGUI();
        GUILayout.EndVertical();
    }
}
