using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float minGroundNormalY = 0.7f;

    Rigidbody2D rb;
    private float moveInput;
    private float baseMoveSpeed;
    private Coroutine speedBoostRoutine;

    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 5;

    //This is here so that if when we build the HUD we use a different script, we can use
    //these public variables to reference score and lives.
    public int Score => score;
    public int Lives => lives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // A/D to move
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        // W to jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        if (moveInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedBoostRoutine != null)
            StopCoroutine(speedBoostRoutine);
        speedBoostRoutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        moveSpeed = baseMoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseMoveSpeed;
        speedBoostRoutine = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y >= minGroundNormalY)
            {
                isGrounded = true;
                break;
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        //We haven't built the HUD yet so we're just printing to debug.
        Debug.Log("Score: " + score);
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        Debug.Log("Lives: " + lives);
        // TODO: Game-over logic goes here (menu-and-game-state section).
        // When lives <= 0, trigger game over — likely loading a Game Over
        // scene or showing a menu. Not implemented yet; out of scope for
        // the collectibles feature.
        if (lives <= 0)
        {
            Debug.Log("GAME OVER (placeholder — not yet implemented)");
        }
    }
}
