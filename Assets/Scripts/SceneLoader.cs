using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string defaultScene;
    private SceneController sceneController = new SceneController();

    private void Start() {
        sceneController.LoadScene(defaultScene != "" ? defaultScene : "TestScene");
    }
}
