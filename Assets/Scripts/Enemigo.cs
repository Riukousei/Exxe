using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private float dañoAtaque = 10f;
    [SerializeField] private float velocidadAtaque = 1f;
    private float puedeAtacar;
    private Transform objetivo;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima=0.02f; //Distancia minima para el patrullaje
    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer; //Controla cómo se ve el personaje
    [SerializeField] private float Vida;
    [SerializeField] private GameObject efectoMuerte;

    private void Start()
    {
        numeroAleatorio = Random.Range(0,puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        if (objetivo != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, step);
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, step);
            if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
            {
                numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
                Girar();
            }
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
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
            Debug.Log(objetivo);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            objetivo = null;
            Debug.Log(objetivo);
        }
    }

    public void TomarDaño(float daño)
    {
        Vida -= daño;
        if(Vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
