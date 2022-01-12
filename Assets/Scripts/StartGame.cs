using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public GameObject gameManager;

    private void Start()
    {
        if (GameObject.Find("Game Manager") == null)
        {
            Instantiate(player, transform);
            Instantiate(canvas, transform);
            Instantiate(gameManager, transform);
            player.transform.name = "Player";
            canvas.transform.name = "UI Canvas";
            gameManager.transform.name = "Game Manager";
            transform.DetachChildren();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 2, 1));
        Gizmos.DrawWireCube(transform.position + transform.forward/2 + Vector3.up*0.7f, new Vector3(.5f, .5f, .5f));
    }
}
