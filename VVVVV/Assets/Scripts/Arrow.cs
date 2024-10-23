using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool died = false;

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
            
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
            {
                died = true;
                gameObject.SetActive(false); // Lo desactiva para reusarlo después


            }

            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<CharacterMovement>().Die(); // Hace que el jugador muera

            }
        }
        
    }
}


