using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ZPong
{
    public class Ball : MonoBehaviour
    {
        public float speed = 5f;
        public float squishScale = 0.5f;
        public float squishDuration = 0.2f;
        private bool isSquishing = false;

        private float screenTop = 527;
        private float screenBottom = -527;

        private Vector2 direction;
        private Vector2 defaultDirection;

        private bool ballActive;

        protected RectTransform rectTransform;

        private AudioSource bounceSFX;

        public ParticleSystem impactParticlePrefab;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            Vector2 initialPosition = new Vector2(0f, -1000f);
            rectTransform.anchoredPosition = initialPosition;

            StartCoroutine(SlideInAnimation());

            bounceSFX = this.GetComponent<AudioSource>();
        }

        IEnumerator SlideInAnimation()
        {
            float startTime = Time.time;
            Vector2 initialPosition = rectTransform.anchoredPosition;
            Vector2 targetPosition = new Vector2(0f, 0f);

            while (Time.time - startTime < 2f)
            {
                float t = (Time.time - startTime) / 2f;
                rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);
                yield return null;
            }

            rectTransform.anchoredPosition = targetPosition;

            if (PlayerPrefs.HasKey("BallSpeed"))
            {
                speed = PlayerPrefs.GetFloat("BallSpeed");
            }

            if (PlayerPrefs.HasKey("BallSize"))
            {
                var value = PlayerPrefs.GetFloat("BallSize");
                rectTransform.sizeDelta = new Vector2(value, value);
            }

            if (PlayerPrefs.HasKey("PitchDirection"))
            {
                string pitchDirectionValue = PlayerPrefs.GetString("PitchDirection");

                if (pitchDirectionValue == "Random")
                {
                    float randomX = Random.Range(-1f, 1f);
                    direction = new Vector2(randomX, 0f).normalized;
                }
                else if (pitchDirectionValue == "Right")
                {
                    direction = new Vector2(1f, 0f);
                }
                else
                {
                    direction = new Vector2(-1f, 0f);
                }
            }
            else
            {
                direction = new Vector2(-1f, 0f);
            }

            defaultDirection = direction;

            SetHeightBounds();
        }

        private void Update()
        {
            if (ballActive)
            {
                Vector2 newPosition = rectTransform.anchoredPosition + (direction * speed * Time.deltaTime);
                rectTransform.anchoredPosition = newPosition;

                if (rectTransform.anchoredPosition.y >= screenTop || rectTransform.anchoredPosition.y <= screenBottom)
                {
                    direction.y *= -1f;
                    PlayBounceSound();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Paddle"))
            {
                Paddle paddle = collision.gameObject.GetComponent<Paddle>();

                float y = BallHitPaddleWhere(GetPosition(), paddle.AnchorPos(),
                    paddle.GetComponent<RectTransform>().sizeDelta.y / 2f);
                Vector2 newDirection = new Vector2(paddle.isLeftPaddle ? 1f : -1f, y);
                ParticleSystem impactParticles = Instantiate(impactParticlePrefab, transform.position, Quaternion.identity);
        impactParticles.Play();

                Reflect(newDirection);
                PlayBounceSound();
                StartCoroutine(SquishAnimation());
            }
            else if (collision.gameObject.CompareTag("Goal"))
            {
                if (this.rectTransform.anchoredPosition.x < -1)
                {
                    ScoreManager.Instance.ScorePointPlayer2();
                }
                else
                {
                    ScoreManager.Instance.ScorePointPlayer1();
                }
            }
        }

        IEnumerator SquishAnimation()
        {
            if (!isSquishing)
            {
                isSquishing = true;

                Vector3 originalScale = transform.localScale;

                transform.localScale = new Vector3(originalScale.x, originalScale.y * squishScale, originalScale.z);
                yield return new WaitForSeconds(squishDuration / 2);

                transform.localScale = originalScale;
                yield return new WaitForSeconds(squishDuration / 2);

                isSquishing = false;
            }
        }

        public void Reflect(Vector2 newDirection)
        {
            direction = newDirection.normalized;
        }

        public void SetBallActive(bool value)
        {
            ballActive = value;
            direction = defaultDirection;
        }

        public Vector2 GetPosition()
        {
            return rectTransform.anchoredPosition;
        }

        public void SetHeightBounds()
        {
            var height = UIScaler.Instance.GetUIHeightPadded();
            screenTop = height / 2;
            screenBottom = -1 * height / 2;
        }

        protected float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
        {
            return (ball.y - paddle.y) / paddleHeight;
        }

        void PlayBounceSound()
        {
            bounceSFX.pitch = Random.Range(.8f, 1.2f);
            bounceSFX.Play();
        }

        public void DisableBall()
        {
            ballActive = false;
        }
    }
}