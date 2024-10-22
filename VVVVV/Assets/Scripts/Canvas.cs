using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public Canvas canvas;
    bool ispaused = false;

    // Update is called once per frame
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
        DontDestroyOnLoad(gameObject);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameManager.currentScene);
        Resume();
    }
    public void Respawn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterMovement>().Die();
        Resume();
    }
    public void Quit()
    {

        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
