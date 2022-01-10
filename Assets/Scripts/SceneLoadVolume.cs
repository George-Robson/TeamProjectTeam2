using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadVolume : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject playerTeleportPosition;
    private SceneController sceneController = new SceneController();
    private Animator blackScreen;
    public float fadeSpeed = 0.0001f;

    private void Start() {
        blackScreen = GameObject.Find("Black Screen").GetComponent<Animator>();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, GetComponent<BoxCollider>().size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerTeleportPosition.transform.position, new Vector3(1, 2, 1));
        Gizmos.DrawWireCube(playerTeleportPosition.transform.position + playerTeleportPosition.transform.forward/2 + Vector3.up*0.7f, new Vector3(.5f, .5f, .5f));
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collision with " + other.transform.name);
        if (other.tag == "Player")
        {
            blackScreen.Play("FadeScreenOut");
            sceneController.LoadScene(sceneToLoad);
            other.transform.SetPositionAndRotation(playerTeleportPosition.transform.position, playerTeleportPosition.transform.rotation);
        }
    }

    //TODO: 
    // private IEnumerator fadeScreenOut() {
    //     while (blackScreen.color.a < 1) {
    //         Color objectColor = blackScreen.color;
    //         float fadeAmount = blackScreen.color.a + (fadeSpeed * Time.fixedDeltaTime);
    //
    //         objectColor = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, fadeAmount);
    //         blackScreen.color = objectColor;
    //         yield return null;
    //     }
    //     bFadeScreenOut = false;
    // }
    //
    // private IEnumerator fadeScreenIn() {
    //     while (blackScreen.color.a > 0) {
    //         Color objectColor = blackScreen.color;
    //         float fadeAmount = blackScreen.color.a - (fadeSpeed * Time.fixedDeltaTime);
    //
    //         objectColor = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, fadeAmount);
    //         blackScreen.color = objectColor;
    //         yield return null;
    //     }
    //     bFadeScreenIn = false;
    // }
}
