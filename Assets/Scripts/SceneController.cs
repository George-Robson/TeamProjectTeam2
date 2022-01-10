using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public string level1, level2, level3, level4;
    public GameObject blackScreen;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        LoadScene(level1);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadScenes(List<string> scenes) {
        for (int i = 0; i < scenes.Count; i++) {
            SceneManager.LoadScene(scenes[i], LoadSceneMode.Additive);
        }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void UnloadScene(string scene) {
        SceneManager.UnloadSceneAsync(scene);
    }

    public void UnloadScenes(List<string> scenes) {
        for (int i = 0; i < scenes.Count; i++) {
            SceneManager.UnloadSceneAsync(scenes[i]);
        }
    }
}
