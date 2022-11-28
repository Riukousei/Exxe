using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadDeMovimiento = 5f;
    private float desplazamientoX, desplazamientoY;
    private Vector2 direccionDeMovimiento;
    private Rigidbody2D rbPersonaje;
    public Animator playerAnimator;
    private bool mirandoDerecha = true;
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadRebote;
    // Es la primera en llamarse cuando el juego se ejecuta
    void Awake()
    {
        rbPersonaje = this.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Es llamada 1 vez por frame
    void Update()
    {
        CapturaDePulsaciones();
        //playerAnimator.SetFloat("Horizontal", Mathf.Abs(desplazamientoX));
        playerAnimator.SetFloat("Magnitude", direccionDeMovimiento.sqrMagnitude);
        FlipPersonaje();

    }

    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            MovimientoDelPersonaje();
        }
    }
    public void Rebote(Vector2 puntoGolpe)
    {
        rbPersonaje.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, 0); //verificar si velocidadRebote.y no se tiene que igualar a 0
    }

    void CapturaDePulsaciones()
    {
        desplazamientoX = Input.GetAxisRaw("Horizontal");
        desplazamientoY = Input.GetAxisRaw("Vertical");
        direccionDeMovimiento = new Vector2(desplazamientoX, desplazamientoY).normalized;
    }

    void FlipPersonaje()
    {

        if(desplazamientoX>0 && !mirandoDerecha)
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else if(desplazamientoX<0 && mirandoDerecha)
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

        }
    }
    void MovimientoDelPersonaje()
    {
        rbPersonaje.MovePosition(rbPersonaje.position + direccionDeMovimiento * velocidadDeMovimiento * Time.fixedDeltaTime);
    }
}
