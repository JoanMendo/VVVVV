using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar escenas

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    // Metodo para cambiar a una escena por nombre
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
