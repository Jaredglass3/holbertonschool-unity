using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;
    public GameObject TimerCanvas;
    public GameObject WinCanvas;
    public GameObject PauseMenu;
    public BackgroundMusicController backgroundMusicController; // Reference to your BackgroundMusicController script
<<<<<<< HEAD
    public AudioSource victorySound;
=======

>>>>>>> 708b849b6652b39b8bbe05d5bcd51da4480c2f3b
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
<<<<<<< HEAD
              Debug.Log("Trigger entered");
           // Stop the background music
=======
            // Stop the background music
>>>>>>> 708b849b6652b39b8bbe05d5bcd51da4480c2f3b
            if (backgroundMusicController != null)
            {
                backgroundMusicController.GetComponent<AudioSource>().Stop();
            }

<<<<<<< HEAD

             // Play the victory sound
            if (victorySound != null)
            {
                victorySound.Play();
            }

=======
>>>>>>> 708b849b6652b39b8bbe05d5bcd51da4480c2f3b
            // Unable show pause menu press esc key
            PauseMenu.SetActive(false);

            // show win display
            WinCanvas.SetActive(true);

            // get Timer script and call win method
            Timer timer = Player.GetComponent<Timer>();
            timer.Win();

            // stop timer cont and hide timer on top
            timer.enabled = false;
            TimerCanvas.SetActive(false);

            // disable player and camera movement 
            Player.GetComponent<PlayerController>().enabled = false;
            Camera.GetComponent<CameraController>().enabled = false;
        }
    }
}