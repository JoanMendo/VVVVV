using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int direction;

    public void ChangeScene()
    {
        GameManager.instance.direction = direction;
        GameManager.instance.ChangeScene();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
