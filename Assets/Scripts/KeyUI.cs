using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    private GameManager gameManager;
    
    private void Update() {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("Game Manager").transform.GetComponent<GameManager>();
        }
        
        if (gameManager.FindState("HasKey")) {
            gameObject.SetActive(true);
        } else {gameObject.SetActive(false);}
    }
}
