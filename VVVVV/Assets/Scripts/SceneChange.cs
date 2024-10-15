using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int direction;
    public Transform room1CameraPosition;
    public Transform room2CameraPosition;
    public Transform PlayerSpawnPosition1;
    public Transform PlayerSpawnPosition1Inverted;
    public Transform PlayerSpawnPosition2;
    public void ChangeScene()
    {
        GameManager.currentScene+= direction;
        SceneManager.LoadScene(GameManager.currentScene);
    }
}
