using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] levelSelectionButtons;

    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionsButton;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var levelID = i + 1;
            levelSelectionButtons[i].onClick.AddListener(delegate { PlayLevel(levelID);});
        }
        
        quitButton.onClick.AddListener(Quit);
        optionsButton.onClick.AddListener(OpenOptions);
    }

    public void PlayLevel(int levelID)
    {
        var scene = "Level0" + levelID;
        PlayerPrefs.SetString("PreviousScene", "MainMenu");
        SceneManager.LoadScene(scene);
    }
    public void OpenOptions()
    {
        PlayerPrefs.SetString("PreviousScene", "MainMenu");
        SceneManager.LoadScene("Options");
    }
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
