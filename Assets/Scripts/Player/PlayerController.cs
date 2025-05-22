using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    private Animator animator;
    private Rigidbody2D rb;
    public GameManager gameManager;

    public float jumpForce = 7f;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Fuerza la posición en el suelo
        transform.position = new Vector3(-5f, -2f, 0); 

        // Opcional: congela el Rigidbody antes de comenzar el juego
        if (!gameManager.isGameStarted)
            rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (!gameManager.isGameStarted)
            return;

        if (rb.bodyType != RigidbodyType2D.Dynamic)
            rb.bodyType = RigidbodyType2D.Dynamic;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpWithDelay());
        }
    }


    void Jump()
    {
        animator.SetBool("jumping", true);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator JumpWithDelay()
    {
        animator.SetBool("jumping", true);
        yield return new WaitForSeconds(0.1f);
        GameAudioController.instance.ExecuteSound(jumpSound);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            animator.SetBool("jumping", false);
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.isGameOver = true;
        }
    }
}