using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int score = 0;
    public int health = 5;
    public Rigidbody m_Rigidbody;
    public float speed = 700;
    public Text scoreText;
    public Text healthText;
    public Image WinLoseImage;
    public Text WinLoseText;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        SetScoreText();
        SetHealthText();
    }

    void Update()
    {
        if (health == 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        m_Rigidbody.velocity = movement * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }

        if (other.gameObject.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            WinLoseText.text = "You Win!";
            WinLoseText.color = Color.black;
            WinLoseImage.color = Color.green;
            WinLoseImage.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    void GameOver()
    {
        WinLoseText.text = "Game Over!";
        WinLoseText.color = Color.white;
        WinLoseImage.color = Color.red;
        WinLoseImage.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3));
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
