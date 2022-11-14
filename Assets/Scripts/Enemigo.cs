using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private float dañoAtaque = 10f;
    [SerializeField] private float velocidadAtaque = 1f;
    private float puedeAtacar;
    private Transform objetivo;

    private void Update()
    {
        if (objetivo != null)
        {
            float step = speed * Time.deltaTime; ;
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, step);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (velocidadAtaque <= puedeAtacar)
            {
                other.gameObject.GetComponent<SaludPersonaje>().UpdateHealth(-dañoAtaque);
                puedeAtacar = 0f;
            }
            else
            {
                puedeAtacar += Time.deltaTime;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            objetivo = other.transform;
            //Debug.Log(objetivo);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            objetivo = null;
            //Debug.Log(objetivo);
        }
    }
}
