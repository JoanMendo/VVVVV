using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.instance.initialCameraPosition; // Posici�n de la c�mara al recargar una escena
    }

    
}
