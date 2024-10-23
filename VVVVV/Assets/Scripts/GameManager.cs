using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;  // Prefab del jugador
    public Vector3 initialSpawnpoint;
    public Vector3 initialCameraPosition;
    public Vector3 cameraPosition;
    public static GameManager instance;
    public Vector3 playerSpawnPoint;  // Punto de spawn del jugador
    public static int currentScene = 0;
    public int direction = 1;

    void Awake()  //Lo hago singleton
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  
        }


    }

    public void ChangeScene(Vector3 cameraPosition, Vector3 characterPosition)
    {
        currentScene += direction;

        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = characterPosition;
        playerSpawnPoint = characterPosition;
        initialSpawnpoint = characterPosition;
        initialCameraPosition = cameraPosition;
        this.cameraPosition = cameraPosition;
        SceneManager.LoadScene(currentScene);



    }


   
   







}
