using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;  // Prefab del jugador
    public Vector3 cameraPosition;
    public static GameManager instance;
    public Vector3 playerSpawnPoint;  // Punto de spawn del jugador
    public static int currentScene = 0;
    public int direction = 1;

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
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public void ChangeScene()
    {
        currentScene += direction;  // Cambiar la escena actual
       
        // Cargar la nueva escena
        
        SceneManager.LoadScene(currentScene);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (direction < 1)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");  // Buscar la cámara
            camera.transform.position = cameraPosition;  // Mover la cámara a la nueva posición
            GameObject player = GameObject.FindGameObjectWithTag("Player");  // Buscar al jugador
            player.transform.position = playerSpawnPoint;  // Mover al jugador al nuevo punto de spawn
        }
    }
   
   







}
