using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public int xMovement;
    public int yMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision");
        if (collision.CompareTag("Player"))
        {
            cam.transform.position = new Vector2(cam.transform.position.x + xMovement *21.2f, cam.transform.position.y + yMovement *10f);

        }
    }
}
