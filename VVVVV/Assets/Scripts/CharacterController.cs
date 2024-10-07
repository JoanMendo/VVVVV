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





    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectsWithTag("Player") == null)
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
        StartCoroutine(checkFloating());
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
            gameObject.transform.rotation = Quaternion.Euler(180, 180, 0);
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
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -transform.up, 1f, groundLayer);

        Debug.DrawRay(transform.position, -transform.up * 1f, Color.red);


        if (hitDown.collider != null )
        {
            rb.gravityScale *= -1;
            gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[1];
            animator.SetBool("IsFloating", true);
            isFloating = true;

        }
        

    }

    public IEnumerator checkFloating()
    {
        
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -transform.up, 1f, groundLayer);

            Debug.DrawRay(transform.position, -transform.up * 1f, Color.red);

            if ((hitDown.collider != null) && isFloating)
            {
                Debug.Log("Grounded");
                gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[0];
                animator.SetBool("IsFloating", false);
            }
            yield return new WaitForSeconds(0.05f);
            yield return checkFloating();


    }
}
