using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Referencia al men� de pausa
    private bool isPaused = false;  // Estado del juego (pausado o no)
    private static CanvasScript instance;  // Para implementar un singleton del men� de pausa

    void Awake()
    {
        // Si ya existe una instancia del men� de pausa, destruye la nueva
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Evita que el men� de pausa se destruya entre escenas
        }
    }

    void Update()
    {
        // Si se presiona la tecla "Escape", alterna el men� de pausa
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
        Debug.Log("Saliendo del juego...");  // Mensaje en el editor
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

        Debug.Log("Reiniciando el juego...");  // Mensaje en el editor
    }
    public void StartGame()
    {
        Time.timeScale = 1f;           
        isPaused = false;              
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("EscenaInicial");
        Destroy(gameObject);
    }
}
