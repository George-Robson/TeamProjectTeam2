using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController
{
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadScenes(List<string> scenes) {
        for (int i = 0; i < scenes.Count; i++) {
            SceneManager.LoadSceneAsync(scenes[i], LoadSceneMode.Additive);
        }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void UnloadScene(string scene) {
        scenesLoading.Add(SceneManager.UnloadSceneAsync(scene));
    }

    public void UnloadScenes(List<string> scenes) {
        for (int i = 0; i < scenes.Count; i++) {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(scenes[i]));
        }
    }
}
