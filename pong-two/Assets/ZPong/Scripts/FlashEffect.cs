using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Color flashColor = Color.yellow;
    public float flashDuration = 2f;

    private Image flashImage;

    private void Start()
    {
        flashImage = GetComponent<Image>();
        flashImage.color = new Color(1f, 1f, 1f, 0f); // Initial transparency
    }

    public void TriggerFlash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        flashImage.color = flashColor; // Set the flash color

        // Wait for one frame
        yield return null;

        flashImage.color = new Color(1f, 1f, 1f, 0f); // Set back to transparent
    }
}
