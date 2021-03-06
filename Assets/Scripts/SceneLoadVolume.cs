using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadVolume : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject playerTeleportPosition;
    private readonly SceneController sceneController = new SceneController();
    private Animator blackScreen;
    public bool disableWireframeGizmos;
    public bool requiresCondition;
    public string condition;
    private GameManager gameManager;

    private void Start() {
        blackScreen = GameObject.Find("Black Screen").GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnDrawGizmos() {
        if (!disableWireframeGizmos) {
            Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(gameObject.transform.position, GetComponent<BoxCollider>().size);
                    Gizmos.DrawLine(gameObject.transform.position, playerTeleportPosition.transform.position);
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(playerTeleportPosition.transform.position, new Vector3(1, 2, 1));
                    Gizmos.DrawWireCube(playerTeleportPosition.transform.position + playerTeleportPosition.transform.forward/2 + Vector3.up*0.7f, new Vector3(.5f, .5f, .5f));
        } 
    }

    private void OnTriggerStay(Collider other) {
        // Debug.Log("Collision with " + other.transform.name);
        if (requiresCondition && !gameManager.FindState(condition))
            return;
        if (other.CompareTag("Player") && other.GetComponent<canChangeLevel>().getCanChangeLevel())
        {
            blackScreen.Play("FadeScreenOut");
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad); 
            other.transform.SetPositionAndRotation(playerTeleportPosition.transform.position, playerTeleportPosition.transform.rotation);
            
        }
    }
}
