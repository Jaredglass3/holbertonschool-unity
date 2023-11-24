using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Timer playerTimer;
    public GameObject triggerBox;

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerTimer.TriggerTimer();
            Destroy(triggerBox);
        }
    }
}
