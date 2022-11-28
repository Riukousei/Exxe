using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private float da�oAtaque = 10f;
    [SerializeField] private float velocidadAtaque = 1f;
    private float puedeAtacar;
    private Transform objetivo;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima=0.02f; //Distancia minima para el patrullaje
    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer; //Controla c�mo se ve el personaje
    [SerializeField] private float Vida;
    [SerializeField] private GameObject efectoMuerte;
    private bool mirandoDerecha = true;
    public Animator animatorEnemigo;

    private void Start()
    {
        animatorEnemigo = GetComponent<Animator>();
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
            MirarJugador();
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
        if ((transform.position.x < puntosMovimiento[numeroAleatorio].position.x) && (!mirandoDerecha))
        {
            //spriteRenderer.flipX = true;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else if((transform.position.x > puntosMovimiento[numeroAleatorio].position.x) && (mirandoDerecha))
        {
            //spriteRenderer.flipX = false;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void MirarJugador()
    {
        if ((transform.position.x < objetivo.position.x)&&(!mirandoDerecha))
        {
            //spriteRenderer.flipX = true;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else if ((transform.position.x > objetivo.position.x)&& (mirandoDerecha))
        {
            //spriteRenderer.flipX = false;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (velocidadAtaque <= puedeAtacar)
            {
                animatorEnemigo.SetTrigger("Ataque");
                other.gameObject.GetComponent<SaludPersonaje>().UpdateHealth(da�oAtaque,other.GetContact(0).normal);
                Debug.Log("Haciendo da�o");
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

    public void TomarDa�o(float da�o)
    {
        Vida -= da�o;
        animatorEnemigo.SetTrigger("Golpeado");
        if (Vida <= 0)
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
