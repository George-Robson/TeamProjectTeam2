using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> conditions = new List<string>();
    public List<GameState> gameStateList = new List<GameState>()
    {
        
    };

    public bool FindState(string state) {
        for (int i = 0; i < gameStateList.Count; i++)
        {
            if (state == gameStateList[i].gameStateString)
                return gameStateList[i].gameStateBool;
        }
        Debug.LogError("Cannot find game state " + state);
        return false;
    }

    public void AddGameState(string condition) {
        gameStateList.Add(new GameState(false, condition));
    }
    
    public void RemoveGameState(string condition) {
        foreach (var state in gameStateList)
        {
            if (state.gameStateString == condition)
                gameStateList.Remove(state);
            return;
        }
    }

    public void SetGameState(string condition, bool b) {
        for (int i = 0; i < gameStateList.Count; i++)
        {
            if (gameStateList[i].gameStateString == condition)
                gameStateList[i].gameStateBool = b;
            return;
        }
    }
}

public class GameState
{
    public bool gameStateBool;
    public string gameStateString;

    public GameState(string s) {
        gameStateString = s;
        gameStateBool = false;
    }

    public GameState(bool b, string s) {
        gameStateBool = b;
        gameStateString = s;
    }
}
