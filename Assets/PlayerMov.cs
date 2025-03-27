using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float MovementSpeed;
    [SerializeField] private bool isAi;
    [SerializeField] private GameObject Ball;

    private Rigidbody2D rb;
    private Vector2 PlayerMove;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isAi)
        {
            AiControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        PlayerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AiControl()
    {
        if (Ball.transform.position.y > transform.position.y + 0.5f)
        {
            PlayerMove = new Vector2(0, 1);
        }
        else if (Ball.transform.position.y < transform.position.y - 0.5f)
        { 
            PlayerMove = new Vector2(0, -1);
        }
        else
        {
            PlayerMove = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = PlayerMove * MovementSpeed;
    }

}

