using TMPro;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Timer playerTimer; // Reference to the Timer component on the player
    public GameObject winCanvas; // Reference to the win canvas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseController.Instance.WinGame(); // Notify the PauseController that the game is won

            winCanvas.SetActive(true);

            playerTimer.Win(); // Display the final time
        }
    }
}