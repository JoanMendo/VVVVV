using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Referencia al menú de pausa
    private bool isPaused = false;  // Estado del juego (pausado o no)
    private static CanvasScript instance;  // Para implementar un singleton del menú de pausa


    

    void Update()
    {
        // Si se presiona la tecla "Escape", alterna el menú de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;           // Restablece el tiempo del juego
        isPaused = false;              // Cambia el estado a "no pausado"
    }

    void Pause()
    {
        pauseMenuUI.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0f;           // Congela el tiempo del juego
        isPaused = true;               // Cambia el estado a "pausado"
    }

    public void QuitGame()
    {
        Application.Quit();            // Cierra el juego (en la build)

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;           // Restablece el tiempo del juego
        isPaused = false;              // Cambia el estado a "no pausado"
        pauseMenuUI.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(GameManager.currentScene);
        GameObject player = GameObject.FindWithTag("Player");
        GameManager.instance.playerSpawnPoint = GameManager.instance.initialSpawnpoint;
        player.GetComponent<CharacterMovement>().Die();

    }
    public void StartGame()
    {
        Time.timeScale = 1f;           
        isPaused = false;              
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("EscenaInicial");
      
        if (GameManager.instance != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            GameManager.instance.playerSpawnPoint = new Vector3(-2f, -3.9f, 0f);
            GameManager.instance.initialCameraPosition = new Vector3(-0.23f, 0, -10f);
            GameManager.currentScene = 1;
            player.GetComponent<CharacterMovement>().Die();
        }
    

    }
}
