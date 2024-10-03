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
        if (rb.velocity.x < 0 && rb.gravityScale > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb.velocity.x < 0 && rb.gravityScale < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(180, 180, 00);
        }
        if (rb.velocity.x >= 0 && rb.gravityScale > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.velocity.x >= 0 && rb.gravityScale < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
        }

    }

    public void Die()
    {
        Destroy(gameObject);

    }
    public void DetectTerrain()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.8f, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * 0.8f, Color.red);
         


        if (hitDown.collider != null || hitUp.collider != null)
        {
            rb.gravityScale *= -1;
            gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[1];
            animator.SetBool("IsFloating", true);

        }
        

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer);
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.8f, groundLayer);
            Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.red);
            Debug.DrawRay(transform.position, Vector2.up * 0.8f, Color.red);
            if (hitDown.collider != null || hitUp.collider != null)
            {
                gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[0];
                animator.SetBool("IsFloating", false);
            }
        }
    }
}
