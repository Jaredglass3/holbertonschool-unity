using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiShake : MonoBehaviour
{
    public float shakeAmount = 5f; // The amount to shake
    public float shakeDuration = 1f; // The duration of the shake

    private Vector3 originalPosition;
    private float shakeTimer;

    void Awake()
    {
        // Save the original position of the UI element
        originalPosition = transform.localPosition;
    }

   [ContextMenu("Shake")]
    // Call this method to start shaking
    public void StartShake()
    {
        shakeTimer = shakeDuration;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Randomly move the UI element around its original position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            // Decrease the timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the position when shaking is over
            transform.localPosition = originalPosition;
        }
    }
}