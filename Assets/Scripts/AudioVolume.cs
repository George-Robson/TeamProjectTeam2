using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    void Start() {
        audioSource = GameObject.Find("Player").transform.GetChild(0).GetComponent<Camera>().GetComponent<AudioSource>();
    } 
    
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, GetComponent<BoxCollider>().size);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        } else Debug.LogError("No audio clip assigned to play");
    }
}
