using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public TextMeshProUGUI interactUI;
    public string givesCondition;
    private GameManager gameManager;
    private Camera cam;
    private RaycastHit hit;
    private bool interactable = false;

    private void Start() {
        
        gameManager = GameObject.Find("Game Manager").transform.GetComponent<GameManager>();
        if (gameManager.FindState(givesCondition))
            Destroy(gameObject);
        cam = GameObject.Find("Player").transform.GetChild(0).GetComponent<Camera>();
        interactUI.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player"))
        {
            interactUI.gameObject.SetActive(true);
            interactable = true;
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("interact");
                gameManager.SetGameState(givesCondition, true);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            interactUI.gameObject.SetActive(false);
            interactable = false;
        }
    }

    private void Update() {
        if (interactable == true)
        {
            interactUI.gameObject.transform.parent.transform.LookAt(cam.transform);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("interact");
                gameManager.SetGameState(givesCondition, true);
                Destroy(gameObject);
            }
        }
    }
}
