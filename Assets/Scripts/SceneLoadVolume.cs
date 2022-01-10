using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadVolume : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject playerTeleportPosition;
    private SceneController sceneController = new SceneController();

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, GetComponent<BoxCollider>().size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerTeleportPosition.transform.position, new Vector3(1, 2, 1));
        Gizmos.DrawWireCube(playerTeleportPosition.transform.position + playerTeleportPosition.transform.forward/2 + Vector3.up*0.7f, new Vector3(.5f, .5f, .5f));
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collision with " + other.transform.name);
        if (other.tag == "Player") {
            sceneController.LoadScene(sceneToLoad);
            other.transform.SetPositionAndRotation(playerTeleportPosition.transform.position, playerTeleportPosition.transform.rotation);
        }
    }
}
