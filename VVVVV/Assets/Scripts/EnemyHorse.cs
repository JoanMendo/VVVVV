using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorse : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask playerLayer;   
    private GameObject enemyRaycast;
    private float direction = 1;
    private float lastTime = -1;
    public float moveSpeed = 2f;
    private Coroutine coroutine;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemyRaycast = transform.GetChild(0).gameObject;
        gameObject.GetComponent<Animator>().SetBool("isMoving", true);
        coroutine = StartCoroutine(pauseOrMove());
    }

    // Update is called once per frame
    void Update()
    {
        detectLimit();
        Move();
        
    }

    public void Move()
    {
        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }   

    }
    public void detectLimit()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(enemyRaycast.transform.position, Vector2.down, 1f, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(enemyRaycast.transform.position, Vector2.right * direction, 1f, groundLayer);
        RaycastHit2D detectPlayerRight = Physics2D.Raycast(enemyRaycast.transform.position, Vector2.right * direction, 35f, playerLayer | groundLayer);
        RaycastHit2D detectPlayerLeft = Physics2D.Raycast(enemyRaycast.transform.position, Vector2.left * direction, 35f, playerLayer | groundLayer);

        Debug.DrawRay(enemyRaycast.transform.position, Vector2.down, Color.red);
        Debug.DrawRay(enemyRaycast.transform.position, Vector2.right * direction, Color.red);
        


        if ((hitDown.collider == null || hitRight.collider != null) && lastTime+1 < Time.time)
        {
            direction *= -1;
            lastTime = Time.time;
        }
        if ((detectPlayerRight.collider != null && detectPlayerRight.collider.CompareTag("Player")) || (detectPlayerLeft.collider != null && detectPlayerLeft.collider.CompareTag("Player")))
        {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(pauseOrMove());
            gameObject.GetComponent<Animator>().SetBool("isAtacking", true);
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);
            moveSpeed = 5.5f;
        }  


    }

    public IEnumerator pauseOrMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            gameObject.GetComponent<Animator>().SetBool("isMoving", false);
            gameObject.GetComponent<Animator>().SetBool("isAtacking", false);
            moveSpeed = 0;
            yield return new WaitForSeconds(2);
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);
            float random = Random.Range(0, 2);
            if (random < 1)
            {
                direction *= -1;
            }
            moveSpeed = 2.5f;
        }
    }

      public void OnCollisionEnter2D(Collision2D collision)
      {
          if (collision.gameObject.tag == "Player")
          {
              collision.gameObject.GetComponent<CharacterMovement>().Die();

          }
      }
}
