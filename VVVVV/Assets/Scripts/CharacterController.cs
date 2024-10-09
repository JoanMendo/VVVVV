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
    private bool isFloating = false;
    private Transform raycastOrigin;
    private SpriteRenderer spriteRenderer;





    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.Play("Revive");   
        raycastOrigin = GetComponentInChildren<Transform>();
        StartCoroutine(lockMovementOnSpawn());


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

        
        if (Input.GetKeyDown(KeyCode.Space) && !isFloating)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f * rb.gravityScale);
            rb.gravityScale *= -1;
            animator.SetBool("IsFloating", true);

        }
        if (rb.velocity.x < 0 && rb.gravityScale > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rb.velocity.x < 0 && rb.gravityScale < 0)
        {
            
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            
        }
        if (rb.velocity.x >= 0 && rb.gravityScale > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.velocity.x >= 0 && rb.gravityScale < 0)
        {
            
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 180);
            
        }

        checkFloating();


    }

    public void Die()
    {
        Destroy(gameObject);
        GameManager.instance.RespawnPlayer();

    }

    public void checkFloating()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(raycastOrigin.position, -transform.up, 0.3f, groundLayer);

        Debug.DrawRay(transform.position, -transform.up * 0.3f, Color.red);

        if (hitDown.collider != null)
        {
            if (isFloating)
            {
                gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[0];
                animator.SetBool("IsFloating", false);
                isFloating = false;
            }
        }
        else if (!isFloating)
        {
            gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[1];
            
            isFloating = true;

        }
    }
    public IEnumerator lockMovementOnSpawn()
    {

        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(0.6f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
