using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Tamalero : MonoBehaviour
{
    public GameEvent GE; //Evento del sistema de puntuacion
    public int puntos; //Sistema de puntuacion
    public float speed = 1f;
    [SerializeField] private float da�oAtaque = 10f;
    [SerializeField] private float velocidadAtaque = 1f;
    public bool puedeAtacar = false;
    private Transform objetivo;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima=0.02f; //Distancia minima para el patrullaje
    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer; //Controla c�mo se ve el personaje
    [SerializeField] private float Vida;
    [SerializeField] private GameObject efectoMuerte;
    public bool mirandoDerecha = true;
    private bool Caminando = false;
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
            Debug.Log("El enemigo va hacia el jugador"); //Para mostrar si el enemigo va hacia el jugador
            if (Caminando == false)
            {
                Caminando = true;
                animatorEnemigo.SetBool("Caminar", Caminando);
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, step);
            MirarJugador();
        }
        else
        {
            Debug.Log("El enemigo deambula"); //Para mostrar si el enemigo deambula por el escenario
            if (Caminando == false)
            {
                Caminando = true;
                animatorEnemigo.SetBool("Caminar", Caminando);
            }
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
            Debug.Log("El enemigo rot� y est� viendo del lado correcto"); //Para mostrar si el enemigo debi� rotar y ver al otro lado
        }
        else if((transform.position.x > puntosMovimiento[numeroAleatorio].position.x) && (mirandoDerecha))
        {
            //spriteRenderer.flipX = false;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            Debug.Log("El enemigo rot� y est� viendo del lado correcto"); //Para mostrar si el enemigo debi� rotar y ver al otro lado
        }
    }

    private void MirarJugador()
    {
        if ((transform.position.x < objetivo.position.x)&&(!mirandoDerecha))
        {
            //spriteRenderer.flipX = true;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            Debug.Log("El enemigo rot� y est� viendo al jugador"); //Para mostrar si el enemigo debi� rotar y est� viendo al jugador
        }
        else if ((transform.position.x > objetivo.position.x)&& (mirandoDerecha))
        {
            //spriteRenderer.flipX = false;
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            Debug.Log("El enemigo rot� y est� viendo al jugador"); //Para mostrar si el enemigo debi� rotar y est� viendo al jugador
        }
    }

    /*private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Colision� con algo"); //Para mostrar si colisiona con algo el enemigo
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Fue con el Jugador la colisi�n"); //Para mostrar si colisiona con el jugador
            if (velocidadAtaque <= puedeAtacar)
            {
                animatorEnemigo.SetTrigger("Ataque");
                other.gameObject.GetComponent<SaludPersonaje>().UpdateHealth(da�oAtaque,other.GetContact(0).normal);
                Debug.Log("Haciendo da�o Al jugador"); //Para mostrar si est� haciendo da�o al jugador
                puedeAtacar = 0f;
            }
            else
            {
                Debug.Log("No puede atacar"); //Para mostrar si no puede atacar
                puedeAtacar += Time.deltaTime;
            }
            
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detect� algo"); //Para mostrar si algo entra en el trigger
        if (other.gameObject.tag == "Player")
        {
            objetivo = other.transform;
            Debug.Log("Fue al jugador"); //Para mostrar si detect� al jugador
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Dej� de detectar algo"); //Para mostrar si algo sale del trigger collider
        if (other.gameObject.tag == "Player")
        {
            objetivo = null;
            Debug.Log("El jugador sali�");//Para mostrar si sali� el jugador del trigger
        }
    }

    public void TomarDa�o(float da�o)
    {
        Debug.Log("El enemigo recibi� da�o"); //Para mostrar si el enemigo fue atacado
        Vida -= da�o;
        animatorEnemigo.SetTrigger("Golpeado");
        if (Vida <= 0)
        {
            Debug.Log("El enemigo debe morir"); //Para mostrar si la vida del enemigo es menor o igual a cero
            Muerte();
        }
    }

    private void Muerte()
    {
        GE.Raise(this, puntos); // Los puntos que a�ade al morir el enemigo
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Debug.Log("El enemigo muri� y se destruy�"); //Para mostrar si el enemigo se muri� y destruy�
    }
}
