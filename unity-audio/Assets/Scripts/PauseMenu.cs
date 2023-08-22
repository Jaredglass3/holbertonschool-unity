using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject Player;
    public GameObject Camera;
    private Rigidbody rb;
    public bool pressed = false;

    [SerializeField] private AudioMixer masterMixer; // Reference to the MasterMixer
    [SerializeField] private AudioMixerSnapshot pausedSnapshot; // Reference to the PausedSnapshot
    [SerializeField] private AudioMixerSnapshot unpausedSnapshot; // Reference to the UnpausedSnapshot
    public float transitionTime = 0.2f; // Transition time for Audio Mixer Snapshots

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        Camera.GetComponent<CameraController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pressed = !pressed;
            if (pressed)
                Resume();
            if (!pressed)
                Pause();
        }
    }

    public void Pause()
    {
        // Transition to the paused snapshot with a smooth transition time
        masterMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { pausedSnapshot }, new float[] { 1.0f }, transitionTime);

        pauseCanvas.SetActive(true);
        Player.GetComponent<Timer>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Camera.GetComponent<CameraController>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Resume()
    {
        // Transition back to the unpaused snapshot with a smooth transition time
        masterMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { unpausedSnapshot }, new float[] { 1.0f }, transitionTime);

        pauseCanvas.SetActive(false);
        Player.GetComponent<Timer>().enabled = true;
        Player.GetComponent<PlayerController>().enabled = true;
        Camera.GetComponent<CameraController>().enabled = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
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
