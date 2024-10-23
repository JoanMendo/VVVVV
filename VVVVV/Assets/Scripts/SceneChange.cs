using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int direction;
    public Vector3 nextPlayerPosition;
    public Vector3 nextCameraPosition;

    public void ChangeScene()
    {
        GameManager.instance.direction = direction;
        GameManager.instance.ChangeScene(nextCameraPosition, nextPlayerPosition);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
