using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludPersonaje : MonoBehaviour
{
    private float vida = 0f;
    [SerializeField] private float vidaMaxima = 100f;

    private void Start()
    {
        vida = vidaMaxima;
    }

    public void UpdateHealth(float mod)
    {
        vida += mod;

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
        else if(vida <= 0f)
        {
            vida = 0f;
            Debug.Log("Game Over");
        }
    }
}
