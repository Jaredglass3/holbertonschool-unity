using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZPong
{

    [RequireComponent(typeof(Paddle))]
    public class Player : MonoBehaviour
    {

        private bool isLeftPaddle;

        public KeyCode upKey;
        public KeyCode downKey;

        public float speed = 5f;

        private Paddle thisPaddle;

        public float movementDelay = 0.5f;  // Delay in seconds
        private bool delayElapsed = false;

        private void Start()
        {
            thisPaddle = GetComponent<Paddle>();

            isLeftPaddle = thisPaddle.isLeftPaddle;
            
            // Retrieve player-specific input keys from PlayerPrefs.
            string upKeyPref = "Player" + (isLeftPaddle ? "One" : "Two") + "UpInput";
            string downKeyPref = "Player" + (isLeftPaddle ? "One" : "Two") + "DownInput";

            if (PlayerPrefs.HasKey(upKeyPref) && PlayerPrefs.HasKey(downKeyPref))
            {
                upKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(upKeyPref));
                downKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(downKeyPref));
            }
            
            if (PlayerPrefs.HasKey("PaddleSpeed"))
            {
                speed = PlayerPrefs.GetFloat("PaddleSpeed");
            }
        }

         private void Update()
        {
            if (Input.GetKeyDown(upKey) || Input.GetKeyDown(downKey))
            {
                StartCoroutine(DelayedMovementStart());
            }
            if ((Input.GetKey(upKey) && delayElapsed) || (Input.GetKey(downKey) && delayElapsed))
            {
                float direction = Input.GetKey(upKey) ? 1 : -1;
                thisPaddle.Move(direction * speed * Time.deltaTime);
            }
            if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey))
            {
                delayElapsed = false;  // Reset delay flag
            }
        }
        private IEnumerator DelayedMovementStart()
        {
            yield return new WaitForSeconds(movementDelay);
            delayElapsed = true;
        }
    }
}