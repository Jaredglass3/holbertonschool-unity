using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject Player;
    public GameObject Camera;
    private Rigidbody rb;
    public bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        rb = Player.GetComponent<Rigidbody>();
        Camera.GetComponent<CameraController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if ESC was pressed before
            pressed = !pressed;
            if (pressed)
                Resume();
            if (!pressed)
                Pause();
        }
    }

    public void Pause()
    {
        // active pause menu and stop timer, movement, camera and falling
        pauseCanvas.SetActive(true);
        Player.GetComponent<Timer>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Camera.GetComponent<CameraController>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Resume()
    {
        // resume game and plays timer, movement, camera and falling
        pauseCanvas.SetActive(false);
        Player.GetComponent<Timer>().enabled = true;
        Player.GetComponent<PlayerController>().enabled = true;
        Camera.GetComponent<CameraController>().enabled = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void Restart()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Load the current scene again
        SceneManager.LoadScene(currentSceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
        GameObject optionsCanvas = GameObject.Find("OptionsCanvas");
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }
}