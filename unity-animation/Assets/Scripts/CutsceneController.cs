using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject mainCamera;
    public GameObject player;
    public GameObject timerCanvas;

    void Start()
    {
        // Subscribe to the animation event for the Level01 animation
        AnimationClip level01Clip = cutsceneAnimator.runtimeAnimatorController.animationClips[0];
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.time = level01Clip.length;
        animationEvent.functionName = "OnLevel01AnimationFinished";
        level01Clip.AddEvent(animationEvent);
    }

    // This method will be called when the Level01 animation is finished
    void OnLevel01AnimationFinished()
    {
        // Enable Main Camera, PlayerController, and TimerCanvas
        mainCamera.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        timerCanvas.SetActive(true);

        // Disable CutsceneController
        gameObject.SetActive(false);
    }
}
