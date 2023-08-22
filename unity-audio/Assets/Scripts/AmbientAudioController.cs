using UnityEngine;

public class AmbientAudioController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public AudioSource ambientAudio; // Reference to the AudioSource component

    public float maxDistance = 10.0f; // Maximum distance for full volume

    void Update()
    {
        // Calculate the distance between player and GameObject
        float distance = Vector3.Distance(player.position, transform.position);

        // Calculate the volume based on distance
        float volume = 1.0f - Mathf.Clamp01(distance / maxDistance);

        // Set the volume of the ambient audio
        ambientAudio.volume = volume;
    }
}
