using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Puntuacion : MonoBehaviour
{
    public GameObject puntos;
    private GameObject puntuacion;
    public Canvas canvas;
    public Canvas CG;

    public int puntosActuales=0;

    public void Start()
    {
        if (CG== null)
        {
            CG = canvas;
            DontDestroyOnLoad(CG);
        }
        else
        {
            Destroy(canvas);
        }
    }


    public void SetPoints(int points)
    {
        puntos.GetComponent<TextMeshProUGUI>().text = points.ToString();
    }

    public void UpdatePoints(Component sender, object data)
    {
        if (data is int)
        {
            puntosActuales=puntosActuales+(int) data;
            
            SetPoints(puntosActuales);
        }

    }
}
