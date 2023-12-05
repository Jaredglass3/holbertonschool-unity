using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ZPong
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 originalPosition;

        private void Start()
        {
            originalPosition = transform.position;
        }

        public void Shake(float intensity, float duration)
        {
            StartCoroutine(ShakeCoroutine(intensity, duration));
        }

        private IEnumerator ShakeCoroutine(float intensity, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float xOffset = Random.Range(-1f, 1f) * intensity;
                float yOffset = Random.Range(-1f, 1f) * intensity;

                transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = originalPosition;
        }
    }
}
