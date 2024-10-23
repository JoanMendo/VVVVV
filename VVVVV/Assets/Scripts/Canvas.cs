using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public Canvas canvas;
    public static CanvasScript canvasInstance;
    bool ispaused = false;

    // Update is called once per frame

    private void Awake()
    {
        if (canvasInstance == null)
        {
            canvasInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (ispaused == false)
            {

                Pause();
                ispaused = true;
            }
            else
            {
                Resume();
                ispaused = false;
            }
    }
    

    public void Pause()
    {

        canvas.enabled = true;
        Time.timeScale = 0;

    }
    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = 1;

    }

    public void Restart()
    {
        SceneManager.LoadScene(GameManager.currentScene);
        GameObject player = GameObject.FindWithTag("Player");
        GameManager.instance.playerSpawnPoint = GameManager.instance.initialSpawnpoint;
        player.GetComponent<CharacterMovement>().Die();
        
        Debug.Log("Restart");
        Resume();
    }

    public void Quit()
    {

        Application.Quit();

    }
}
