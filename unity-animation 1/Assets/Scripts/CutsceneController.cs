using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject MainCamera;
    public GameObject TimerCanvas;
    public GameObject CutsceneCamera; // Reference to the CutsceneCamera GameObject
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Disable PlayerController script and TimerCanvas at the start
        player.GetComponent<PlayerController>().enabled = false;
        TimerCanvas.SetActive(false);
    }

    public void OnIntroAnimationFinished()
    {
        // Enable PlayerController script and TimerCanvas
        player.GetComponent<PlayerController>().enabled = true;
        TimerCanvas.SetActive(true);

        // Trigger the Level01 animation to start
        animator.SetTrigger("StartLevel01");

        // Disable this script and MainCamera
        animator.enabled = false;
        MainCamera.SetActive(true);

        // Disable the CutsceneCamera
        CutsceneCamera.SetActive(false);
    }
}

