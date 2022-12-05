using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameEvent GE; //Evento del sistema de puntuacion
    public int puntos; //Sistema de puntuacion
    [SerializeField] private float Vida;
    [SerializeField] private GameObject efectoMuerte;
    
    public void TomarDaño(float daño)
    {
        
        Vida -= daño;
        if (Vida <= 0)
        {
            Vida = 1000;
            Muerte();
        }
    }

    private void Muerte()
    {
        GE.Raise(this, puntos); // Los puntos que añade al morir el enemigo
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
