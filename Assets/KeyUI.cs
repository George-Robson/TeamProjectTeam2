using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    private void Update() {
        gameObject.SetActive(GameObject.Find("Game Manager").transform.GetComponent<GameManager>().FindState("HasKey"));
    }
}
