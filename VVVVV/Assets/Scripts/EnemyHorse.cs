using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorse : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject enemyRaycast;
    private float direction = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyRaycast = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        detectLimit();
    }

    public void Move()
    {
        rb.velocity = new Vector2(4 * direction, rb.velocity.y);


    }
    public void detectLimit()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemyRaycast.transform.position, Vector2.down * direction, 0.5f);
        if (hit.collider == null)
        {
            direction *= -1;
        }
    }
}
