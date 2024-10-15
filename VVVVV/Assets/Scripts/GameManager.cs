using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;  // Prefab del jugador
    public static GameManager instance;
    public Transform playerSpawnPoint;  // Punto de spawn del jugador
    public Scene[] scenes;  // Array de escenas
    public static int currentScene = 0;

    void Awake()
    {
        // Si ya existe una instancia del GameManager, destruir esta para evitar duplicados
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Evitar que este objeto sea destruido al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject);  // Destruir duplicado si ya existe una instancia
        }

    }
    public void RespawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);  // Instanciar jugador en el punto de spawn
        
    }







}
