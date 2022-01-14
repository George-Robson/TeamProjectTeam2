using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadBook : MonoBehaviour
{
    public Sprite book;
    private GameObject bookobject;
    public string condition;
    private GameManager gameManager;
    private bool toggled;
    private GameObject UICanvas;

    private void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        UICanvas = GameObject.Find("UI Canvas");
    }
    private void OnTriggerStay(Collider other) {
        if (gameManager.FindState(condition) && Input.GetKeyDown(KeyCode.R))
        {
            if (bookobject == null)
            {
                bookobject = Instantiate(new GameObject(), UICanvas.transform);
                bookobject.AddComponent<Image>().sprite = book;
                bookobject.GetComponent<RectTransform>().transform.localScale = new Vector3(20, 20, 1);
            }
                
            if (toggled)
            {
                bookobject.SetActive(false);
            }
            if (!toggled)
            {
                bookobject.SetActive(true);
            }
            toggled = !toggled;

        }
    }
    private void OnTriggerExit(Collider other) {
        bookobject.SetActive(false);
    }
}
