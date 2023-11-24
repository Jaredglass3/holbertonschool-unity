using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCanvasController : MonoBehaviour
{
    public Button mainMenu;
    public Button nextLevel;

    private int sceneNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.onClick.AddListener(MainMenu);
        nextLevel.onClick.AddListener(NextLevel);
        
        var scene = SceneManager.GetActiveScene().name;
        if (scene.Length == 7)
        {
            Debug.Log("Scene indexed " + scene[6]);
            sceneNumber = scene[6] - '0';
            Debug.Log("Scene " + sceneNumber);

        }
        else
        {
            Debug.LogError("Scene is not numeral: " + scene);
        }
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if (sceneNumber == 3)
        {
            MainMenu();
        }
        else
        {
            Debug.Log("Scene " + sceneNumber);
            sceneNumber++;
            Debug.Log("Scene " + sceneNumber);
            SceneManager.LoadScene("Level0" + sceneNumber);
        }
    }
}
