using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadVolume : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject playerTeleportPosition;
    private SceneController sceneController = new SceneController();
    private Image blackScreen;
    public float fadeSpeed = 0.2f;

    private void Start() {
        blackScreen = GameObject.Find("Black Screen").GetComponent<Image>();
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
        if (other.tag == "Player") {
            StartCoroutine(fadeScreenIn());
            sceneController.LoadScene(sceneToLoad);
            other.transform.SetPositionAndRotation(playerTeleportPosition.transform.position, playerTeleportPosition.transform.rotation);
        }
    }

    //TODO: 
    private IEnumerator fadeScreenIn() {
        while (blackScreen.color.a < 1) {
            Debug.Log("hello");
            Color objectColor = blackScreen.color;
            float fadeAmount = blackScreen.color.a + (fadeSpeed * Time.fixedDeltaTime);

            objectColor = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, fadeAmount);
            blackScreen.color = objectColor;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
}
