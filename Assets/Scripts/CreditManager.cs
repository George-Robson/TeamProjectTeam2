using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public Animator animator;
    public bool goToMenu = false;
    
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("EndCredits");
    }

    private void Update()
    {
        if (goToMenu)
            MainMenu();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
