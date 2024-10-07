using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangerHorizontal : MonoBehaviour
{
    public Transform room1CameraPosition;  
    public Transform room2CameraPosition;
    public Transform PlayerSpawnPosition1;
    public Transform PlayerSpawnPosition2;
    public Transform PlayerSpawnPosition1Inverted;
    public Transform PlayerSpawnPosition2Inverted;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           Debug.Log("Player entered the trigger");
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            
            if (playerRb.velocity.x > 0)  
            {
               
                Camera.main.transform.position = new Vector3(
                    room2CameraPosition.position.x,
                    room2CameraPosition.position.y,
                    Camera.main.transform.position.z  
                );
                
                if (playerRb.gravityScale > 0)
                {
                    other.transform.position = PlayerSpawnPosition2.position;
                    GameManager.instance.playerSpawnPoint = PlayerSpawnPosition2;
                }
                else
                {
                    other.transform.position = PlayerSpawnPosition2Inverted.position;
                }
            }
            else if (playerRb.velocity.x < 0)  
            {
               
                Camera.main.transform.position = new Vector3(
                    room1CameraPosition.position.x,
                    room1CameraPosition.position.y,
                    Camera.main.transform.position.z
                );
                if (playerRb.gravityScale > 0)
                {
                    other.transform.position = PlayerSpawnPosition1.position;
                    GameManager.instance.playerSpawnPoint = PlayerSpawnPosition1;
                }
                else
                {
                    other.transform.position = PlayerSpawnPosition1Inverted.position;
                }
            }
        }
    }
}
