using System.Collections;
using UnityEngine;

namespace ZPong
{
    [RequireComponent(typeof(Collider2D))]
    public class Paddle : MonoBehaviour
    {
        public bool isLeftPaddle = true;
        public float slideInDuration = 1.0f; // Adjust this value based on the desired slide-in duration

        private float halfPlayerHeight;
        public float screenTop { get; private set; }
        public float screenBottom { get; private set; }

        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            if (PlayerPrefs.HasKey("PaddleSize"))
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, PlayerPrefs.GetFloat("PaddleSize"));
                this.GetComponent<BoxCollider2D>().size = rectTransform.sizeDelta;
            }

            halfPlayerHeight = rectTransform.sizeDelta.y / 2f;

            var height = UIScaler.Instance.GetUIHeight();

            screenTop = height / 2;
            screenBottom = -1 * height / 2;

            Vector2 offscreenPosition = new Vector2(rectTransform.anchoredPosition.x, screenTop + halfPlayerHeight + 10f);
            rectTransform.anchoredPosition = offscreenPosition;

            StartCoroutine(SlideInAnimation());
        }

        public void Move(float movement)
        {
            Vector2 newPosition = rectTransform.anchoredPosition;
            newPosition.y += movement;
            newPosition.y = Mathf.Clamp(newPosition.y, screenBottom + halfPlayerHeight, screenTop - halfPlayerHeight);
            rectTransform.anchoredPosition = newPosition;
        }

        public float GetHalfHeight()
        {
            return halfPlayerHeight;
        }

        public Vector2 AnchorPos()
        {
            return rectTransform.anchoredPosition;
        }

        private IEnumerator SlideInAnimation()
        {
            float elapsedTime = 0f;
            Vector2 offscreenPosition = new Vector2(rectTransform.anchoredPosition.x, screenTop + halfPlayerHeight + 10f);
            Vector2 targetPosition = new Vector2(rectTransform.anchoredPosition.x, screenTop - halfPlayerHeight);

            while (elapsedTime < slideInDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(offscreenPosition, targetPosition, elapsedTime / slideInDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = targetPosition;
        }
    }
}