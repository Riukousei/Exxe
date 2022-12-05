using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Moto : MonoBehaviour
{
    public GameEvent GE; //Evento del sistema de puntuacion
    public int puntos; //Sistema de puntuacion
    public float speed = 1f;
    [SerializeField] private float dañoAtaque = 10f;
    [SerializeField] private float velocidadAtaque = 1f;
    private float puedeAtacar;
    private Transform objetivo;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima = 0.02f; //Distancia minima para el patrullaje
    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer; //Controla cómo se ve el personaje
    [SerializeField] private float Vida;
    [SerializeField] private GameObject efectoMuerte;
    private bool mirandoDerecha = true;
    public Animator animatorEnemigo;

    private void Start()
    {
        animatorEnemigo = GetComponent<Animator>();
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        Debug.Log("El enemigo deambula"); //Para mostrar si el enemigo deambula por el escenario
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, step);
        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
            Girar();
        }
    }

    private void Girar()
    {
        if ((transform.position.x < puntosMovimiento[numeroAleatorio].position.x) && (!mirandoDerecha))
        {
            //spriteRenderer.flipX = true;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            Debug.Log("El enemigo rotó y está viendo del lado correcto"); //Para mostrar si el enemigo debió rotar y ver al otro lado
        }
        else if ((transform.position.x > puntosMovimiento[numeroAleatorio].position.x) && (mirandoDerecha))
        {
            //spriteRenderer.flipX = false;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            Debug.Log("El enemigo rotó y está viendo del lado correcto"); //Para mostrar si el enemigo debió rotar y ver al otro lado
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colisionó con algo"); //Para mostrar si colisiona con algo el enemigo
        if (other.gameObject.tag == "Player")
        {
            //animatorEnemigo.SetTrigger("Ataque");
            other.gameObject.GetComponent<SaludPersonaje>().UpdateHealth(dañoAtaque, other.GetContact(0).normal);
            Debug.Log("Haciendo daño Al jugador"); //Para mostrar si está haciendo daño al jugador
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detectó algo"); //Para mostrar si algo entra en el trigger
        if (other.gameObject.tag == "Player")
        {
            objetivo = other.transform;
            Debug.Log("Fue al jugador"); //Para mostrar si detectó al jugador
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Dejó de detectar algo"); //Para mostrar si algo sale del trigger collider
        if (other.gameObject.tag == "Player")
        {
            objetivo = null;
            Debug.Log("El jugador salió");//Para mostrar si salió el jugador del trigger
        }
    }

    public void TomarDaño(float daño)
    {
        Debug.Log("El enemigo recibió daño"); //Para mostrar si el enemigo fue atacado
        Vida -= daño;
        //animatorEnemigo.SetTrigger("Golpeado");
        if (Vida <= 0)
        {
            Vida = 1000;
            Debug.Log("El enemigo debe morir"); //Para mostrar si la vida del enemigo es menor o igual a cero
            Muerte();
        }
    }

    private void Muerte()
    {
        GE.Raise(this, puntos); // Los puntos que añade al morir el enemigo
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Debug.Log("El enemigo murió y se destruyó"); //Para mostrar si el enemigo se murió y destruyó
    }
}
