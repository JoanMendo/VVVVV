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
    private static CharacterMovement instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Hace que el jugador persista entre escenas
            
        }
        else
        {
            Destroy(gameObject); // Destruye la instancia duplicada
        }
    }


    void Start()
    {
        GameManager.instance.initialSpawnpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        raycastOrigin = GetComponentInChildren<Transform>(); // Para poder modificar mejor el punto de origen de los raycast

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

        
        if (Input.GetKeyDown(KeyCode.Space) && !isFloating) //Solo se puede cambiar la gravedad si estas en el suelo
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

    public void Die() //Resetea la posición del jugador
    {

        animator.Play("Revive");
        transform.position = GameManager.instance.playerSpawnPoint;
        if (rb.gravityScale < 0)
        {
            rb.gravityScale *= -1;
            rb.velocity = new Vector2(0, 0);
        }
        StartCoroutine(lockMovementOnSpawn());

    }

    public void checkFloating()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(raycastOrigin.position, -transform.up, 0.3f, groundLayer);

        Debug.DrawRay(transform.position, -transform.up * 0.3f, Color.red);

        if (hitDown.collider != null)
        {
            if (isFloating)
            {
                gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[0]; //Le añade friccion
                animator.SetBool("IsFloating", false);
                isFloating = false;
            }
        }
        else if (!isFloating)
        {
            gameObject.GetComponent<Collider2D>().sharedMaterial = physicMaterials[1]; //Le quita friccion

            isFloating = true;

        }
    }
    public IEnumerator lockMovementOnSpawn() //Para que no se mueva durante la animación de respawn
    {

        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSeconds(0.6f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
