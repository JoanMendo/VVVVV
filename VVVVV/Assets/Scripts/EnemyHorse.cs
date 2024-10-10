using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorse : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    private GameObject enemyRaycast;
    private float direction = 1;
    private float lastTime = -2;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemyRaycast = transform.GetChild(0).gameObject;
        gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        detectLimit();
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(2 * direction, rb.velocity.y);
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

        Debug.DrawRay(enemyRaycast.transform.position, Vector2.down, Color.red);
        Debug.DrawRay(enemyRaycast.transform.position, Vector2.right * direction, Color.red);


        if ((hitDown.collider == null || hitRight.collider != null) && lastTime+2 < Time.time)
        {
            direction *= -1;
            lastTime = Time.time;
        }
        
    }

}
