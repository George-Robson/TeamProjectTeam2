using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour {
    public string fixedCondition;
    public string placedCondition;
    private GameManager gm;

    private void Start() {
        gm = GameObject.Find("Game Manager").transform.GetComponent<GameManager>();
    }

    public void ObjectFixed() {
        gm.SetGameState(fixedCondition, true);
    }
    
    public void ObjectPlaced() {
        gm.SetGameState(placedCondition, true);
    }
}
