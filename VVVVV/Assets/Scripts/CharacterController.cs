using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody2D rb;
    private float InputX;
    public LayerMask groundLayer;
    public PhysicsMaterial2D[] physicMaterials;
    private Animator animator;
    private SpriteRenderer spriteRenderer;





    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectsWithTag("Player") == null)
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        InputX = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(InputX * speed, rb.velocity.y);

        if (InputX == 0)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            DetectTerrain();
                 
        }
        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    public void Die()
    {
        Destroy(gameObject);

    }
    public void DetectTerrain()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.6f, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * 0.6f, Color.red);
         


        if (hitDown.collider != null || hitUp.collider != null)
        {
            rb.gravityScale *= -1;
            gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[1];

        }
        

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.45f, groundLayer);
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.45f, groundLayer);
            Debug.DrawRay(transform.position, Vector2.down * 0.45f, Color.red);
            Debug.DrawRay(transform.position, Vector2.up * 0.45f, Color.red);
            if (hitDown.collider != null || hitUp.collider != null)
            {
                gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[0];
            }
        }
    }
}
