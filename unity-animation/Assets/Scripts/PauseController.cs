using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;
    
    public Button pauseSelector;

    public GameObject PauseCanvas;

    private bool isPaused;
    private bool hasWon = false;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        PauseCanvas.SetActive(false);
        
        pauseSelector.gameObject.SetActive(true);
        pauseSelector.onClick.AddListener(TriggerPause);

        isPaused = false;
        Time.timeScale = 1;
    }

    public void TriggerPause()
    {
        Pause();
    }
    public void TriggerResume()
    {
        Resume();
    }

    public void Trigger()
    {
        if (hasWon) return;
        
        if (isPaused)
        {
            TriggerResume();
        }
        else
        {
            TriggerPause();
        }
    }
    
    private void Pause()
    {
        PauseCanvas.SetActive(true);
        pauseSelector.gameObject.SetActive(false);
        Time.timeScale = 0;
        Cursor.visible = true;
        isPaused = true;
    }
    private void Resume()
    {
        PauseCanvas.SetActive(false);
        pauseSelector.gameObject.SetActive(true);
        Time.timeScale = 1;
        Cursor.visible = false;
        isPaused = false;
    }

    public void WinGame()
    {
        pauseSelector.gameObject.SetActive(false);
        
        Time.timeScale = 0;
        Cursor.visible = true;
        
        hasWon = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Trigger();
        }
    }
}
