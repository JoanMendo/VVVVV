using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    public static Cronometro instance;
    public TextMeshProUGUI cronometroText; 
    public  float tiempoTranscurrido = 0f;
    public bool enMarcha = true;

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
    void Start()
    {
        tiempoTranscurrido = 0f;
    }

    void Update()
    {
        // Solo cuenta el tiempo si está en marcha
        if (enMarcha)
        {
            // Suma el tiempo transcurrido en cada frame
            tiempoTranscurrido += Time.deltaTime;
            ActualizarCronometroText();
        }
    }

    void ActualizarCronometroText()
    {
        // Convierte el tiempo en minutos y segundos
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);

        // Muestra el tiempo en formato mm:ss
        cronometroText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

}
