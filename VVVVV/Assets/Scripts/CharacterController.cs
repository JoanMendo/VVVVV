using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody2D rb;
    private float InputX;
    private bool Grounded;
    private float time = 0f;
    private Renderer renderer;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        if (GameObject.FindGameObjectsWithTag("Player") == null)
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        InputX = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(InputX * speed, rb.velocity.y);

        
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rb.gravityScale *= -1;
            Grounded = false;
            rb.sharedMaterial = new PhysicsMaterial2D { friction = 0, bounciness = 0 };
        }
     


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;

            rb.sharedMaterial = new PhysicsMaterial2D { friction = 1, bounciness = 0 };
        }
    }
}
