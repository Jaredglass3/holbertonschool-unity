using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // This variable will store the name of the previous scene
    private string previousScene;

    // Start is called before the first frame update
    void Start()
    {
        // Get the name of the previous scene from PlayerPrefs if available
        previousScene = PlayerPrefs.GetString("PreviousScene", "MainMenu");
    }

    // Method to handle the Back button's click event
    public void Back()
    {
        // Load the previous scene using its name
        SceneManager.LoadScene(previousScene);
    }
}