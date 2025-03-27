using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIscore;
    [SerializeField] public int ScoreToReach;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private Text GameOverText;

    private int hitCounter;
    private Rigidbody2D rb;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.linearVelocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
        hitCounter = 0;
        Invoke("StartBall", 1f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection = transform.position.x > 0 ? -1 : 1;
        float yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if (yDirection == 0)
            yDirection = 0.25f;

        rb.linearVelocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Ai")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
            CheckGameEnd();
            ResetBall();
        }
        else if (transform.position.x < 0)
        {
            AIscore.text = (int.Parse(AIscore.text) + 1).ToString();
            CheckGameEnd();
            ResetBall();
        }
    }

    private void CheckGameEnd()
    {
        int playerScoreValue = int.Parse(playerScore.text);
        int aiScoreValue = int.Parse(AIscore.text);

        if (playerScoreValue >= ScoreToReach)
        {          
            Time.timeScale = 0;
            GameOverScreen.SetActive(true);
            GameOverText.text = "Player Wins!";
        }
        else if (aiScoreValue >= ScoreToReach)
        {
            Time.timeScale = 0; 
            GameOverScreen.SetActive(true);
            GameOverText.text = "Ai Wins!";
        }
    }
}
