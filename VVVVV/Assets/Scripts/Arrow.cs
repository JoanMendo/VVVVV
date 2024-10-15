using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    public bool died = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.right * 14 * Time.deltaTime, Space.World);
        Debug.DrawRay(transform.position, transform.right * 2, Color.red);  // Flecha roja para mostrar transform.right (local)
        Debug.DrawRay(transform.position, Vector3.right * 2, Color.blue);   // Flecha azul para mostrar Vector3.right (global)
    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!died)
        {
            
            if (collision.gameObject.tag == "Ground")
            {
                died = !died;
                GetComponentInChildren<ParticleSystem>().Play();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.enabled = false;
            }

            if (collision.gameObject.tag == "Player")
            {
                died = !died;
                GetComponentInChildren<ParticleSystem>().Play();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.enabled = false;
                collision.gameObject.GetComponent<CharacterMovement>().Die();
            }
        }
        
    }
}


