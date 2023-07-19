using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public float time = 0f;

    public Text FinalTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Counter time, in specific format
        time += Time.deltaTime;
        TimerText.text = time.ToString("0:00.00");
    }

    public void Win()
    {
        // time shown on WinCanvas
        FinalTime.text = TimerText.text;
    }
}