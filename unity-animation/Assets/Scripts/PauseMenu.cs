using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;
    public Button optionsButton;
    public Button resumeButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(MainMenu);
        optionsButton.onClick.AddListener(Options);
        restartButton.onClick.AddListener(Restart);
    }
    
    public void Resume()
    {
        PauseController.Instance.TriggerResume();
    }
    

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


    public void Options()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Options");
    }


}
