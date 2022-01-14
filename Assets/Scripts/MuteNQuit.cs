using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuteNQuit : MonoBehaviour
{
    public bool muted = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(GameObject.Find("UI Canvas"));
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("Game Manager"));
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!muted)
            {
                foreach (var textMeshProUGUI in FindObjectsOfType<TextMeshProUGUI>())
                {
                    Destroy(textMeshProUGUI.gameObject);
                }
                GameObject.Find("UI Canvas").transform.GetChild(0).gameObject.SetActive(false);
            }
            if (muted)
            {
            }
            muted = !muted;
        }
    }
}
