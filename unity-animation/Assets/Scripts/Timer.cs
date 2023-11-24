using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerText; // Reference to the TextMeshPro component displaying the timer
    private float elapsedTime = 0f; // Elapsed game time
    public TMP_Text FinalTime; // Reference to the TextMeshPro component displaying the final time

    private bool isCounting;

    private float targetAlpha;

    private void Start()
    {
        isCounting = false;
        targetAlpha = TimerText.alpha;
        TimerText.alpha = targetAlpha / 2;
    }

    public void TriggerTimer()
    {
        isCounting = true;
        elapsedTime = 0;
        TimerText.alpha = targetAlpha;
    }

    // Update the displayed timer
    void Update()
    {
        if (isCounting)
        {
            elapsedTime += Time.deltaTime;
            float seconds = elapsedTime % 60;
            float minutes = Mathf.Floor(elapsedTime / 60);

            TimerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
        }
    }

    // Display the final time
    public void Win()
    {
        isCounting = false;

        TimerText.gameObject.SetActive(false);
        
        float seconds = elapsedTime % 60;
        float minutes = Mathf.Floor(elapsedTime / 60);
        
        FinalTime.color = Color.green;
        FinalTime.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
}