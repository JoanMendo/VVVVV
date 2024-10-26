using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int nextScene;
    public Vector3 nextPlayerPosition;
    public Vector3 nextCameraPosition;

    public void ChangeScene()
    {

        GameManager.instance.ChangeScene(nextCameraPosition, nextPlayerPosition, nextScene); //Las posiciones del jugador y la cámara en la escena
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
